using Microsoft.Data.Sqlite;
using System.Collections;
using System.Xml.Linq;

namespace BillSplitter
{
    internal class BillStore : IDisposable
    {
        private readonly SqliteConnection _conn;

        internal BillStore(string file)
        {
            _conn = new($"Data Source={file};Foreign Keys=True");
            _conn.Open();
        }

        internal long AddBill(string starter, string name)
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Bill (Starter, Name, LastEdited) VALUES ($starter, $name, {TimeNow()})";
            cmd.Parameters.AddWithValue("$starter", starter);
            cmd.Parameters.AddWithValue("$name", name);
            cmd.ExecuteNonQuery();
            return GetLastInsertRowId();
        }

        internal IEnumerable<(long id, string name)> GetBillsByStarter(string starter)
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = $"SELECT Id, Name FROM Bill WHERE Starter = $starter";
            cmd.Parameters.AddWithValue("$starter", starter);

            var reader = cmd.ExecuteReader();
            List<(long, string)> result = [];
            Console.WriteLine(result.GetType().Name);

            while (reader.Read())
            {
                result.Add((reader.GetInt64(0), reader.GetString(1)));
            }
            return result;
        }

        internal long AddParticipant(long billId, string participant)
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Participant VALUES ($billId, $participant)";
            cmd.Parameters.AddWithValue("$billId", billId);
            cmd.Parameters.AddWithValue("$participant", participant);
            cmd.ExecuteNonQuery();
            return GetLastInsertRowId();
        }

        private long GetLastInsertRowId()
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = "SELECT last_insert_rowid()";
            return (long)cmd.ExecuteScalar()!;
        }

        private static long TimeNow() => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}

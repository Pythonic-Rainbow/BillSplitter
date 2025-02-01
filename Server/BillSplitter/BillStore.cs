using Microsoft.Data.Sqlite;

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
            cmd.CommandText = "INSERT INTO Bill VALUES ($starter, $name)";
            cmd.Parameters.AddWithValue("$starter", starter);
            cmd.Parameters.AddWithValue("$name", name);
            cmd.ExecuteNonQuery();
            return GetLastInsertRowId();
        }

        private long GetLastInsertRowId()
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = "SELECT last_insert_rowid()";
            return (long)cmd.ExecuteScalar()!;
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}

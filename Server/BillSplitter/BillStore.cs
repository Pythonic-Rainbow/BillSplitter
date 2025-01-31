using Microsoft.Data.Sqlite;

namespace BillSplitter
{
    internal class BillStore : IDisposable
    {
        private readonly SqliteConnection conn;

        internal BillStore(string file)
        {
            conn = new($"Data Source={file};Foreign Keys=True");
            conn.Open();
        }

        internal int AddBill(string starter, string name)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Bill VALUES ($starter, $name)";
            cmd.Parameters.AddWithValue("$starter", starter);
            cmd.Parameters.AddWithValue("$name", name);
            return cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}

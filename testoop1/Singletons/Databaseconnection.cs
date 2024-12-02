using MySql.Data.MySqlClient;

namespace Singletons
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private static readonly object _lock = new object();
        private MySqlConnection _connection;
        private static string _connectionString;

        private DatabaseConnection()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        public static void Initialize(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static DatabaseConnection GetInstance()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string has not been initialized.");
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnection();
                }
            }
            return _instance;
        }

        public MySqlConnection GetConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }
    }
}


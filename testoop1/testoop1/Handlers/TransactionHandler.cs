using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testoop1.Interfaces;

namespace testoop1.Handlers
{
    public class TransactionHandler
    {
        private string connectionString="";

        public TransactionHandler(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveTransaction(ISaveable transaction)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                transaction.Save(connection);
            }
        }
    }
}

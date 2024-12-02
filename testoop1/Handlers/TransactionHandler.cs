using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testoop1.Interfaces;
using Singletons;

namespace testoop1.Handlers
{
    

    public class TransactionHandler
    {
        public void SaveTransaction(ISaveable transaction)
        {
            var connection = DatabaseConnection.GetInstance().GetConnection();
            transaction.Save(connection);
        }
    }

}

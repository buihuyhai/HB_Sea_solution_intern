using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Abstracts
{
    public abstract class Transaction
    {
        public decimal Total_Amount { get; set; }
        public Transaction(decimal total_amount)
        {
            Total_Amount = total_amount;
        }
        public abstract void Save(MySqlConnection connection);
    }
}

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
        public decimal TotalAmount { get; set; }
        public Transaction(decimal totalamount)
        {
            TotalAmount = totalamount;
        }
        public abstract void Save(MySqlConnection connection);
    }
}

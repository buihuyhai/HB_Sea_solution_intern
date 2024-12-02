using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Factories
{
    using testoop1.Abstracts;
    using testoop1.Models;

    public class TransactionFactory
    {
        public static Transaction CreateTransaction(string type, params object[] args)
        {
            switch (type.ToLower())
            {
                case "cart":
                    return new Cart((string)args[0], (decimal)args[1], (string)args[2]);
                case "order":
                    return new Order((string)args[0], (decimal)args[1], (string)args[2], (string)args[3],
                                      (string)args[4], (string)args[5], (DateTime)args[6], (string)args[7]);
                default:
                    throw new ArgumentException("Invalid transaction type.");
            }
        }
    }

}

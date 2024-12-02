using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testoop1.Abstracts;
using testoop1.Handlers;
using testoop1.Interfaces;

namespace testoop1.Facades
{
    
    public class TransactionFacade
    {
        private readonly TransactionHandler _handler;

        public TransactionFacade()
        {
            _handler = new TransactionHandler();
        }

        public void SaveTransaction(Transaction transaction)
        {
            try
            {
                _handler.SaveTransaction((ISaveable)transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Transaction save failed: " + ex.Message);
            }
        }
    }

}

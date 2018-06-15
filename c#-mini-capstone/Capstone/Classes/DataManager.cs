using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    interface IDataManager
    {
        // Called by UserInterface to load items to build VendingMachine with
        List<VendingMachineItem> LoadItems(string filename);

        // Called by UserInterface after every Transaction built
        void WriteTransaction(VendingMachineTransaction transaction, decimal currentBalance);

        // Called by UserInterface when sales report requested
        void GenerateSalesReport(List<VendingMachineTransaction> transactions);
    }

    public class DataManager : IDataManager
    {
        private readonly string logFile;

        public DataManager(string logFile)
        {
            this.logFile = logFile;
            // Print out vending machine starting up in log.
        }

        private string LogFile
        {
            get { return logFile; }
        }

        public List<VendingMachineItem> LoadItems(string filename)
        {
            throw new NotImplementedException();
        }

        public void WriteTransaction(VendingMachineTransaction transaction, decimal currentBalance)
        {
            using (StreamWriter sw = new StreamWriter(LogFile, false))
            {
                   sw.WriteLine($"{transaction.Timestamp} {transaction.Type} {transaction.Amount.ToString("C")} {currentBalance.ToString("C")}");
            };
        }

        public void GenerateSalesReport(List<VendingMachineTransaction> transactions)
        {
            throw new NotImplementedException();
        }
    }
}

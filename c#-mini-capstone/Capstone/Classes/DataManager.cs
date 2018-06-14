﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class DataManager //: IDataManager
    {
        private string LogFile { get; }
        public DataManager(string logFile)
        {
            LogFile = logFile;
            // Print out vending machine starting up in log.
        }
    }
}
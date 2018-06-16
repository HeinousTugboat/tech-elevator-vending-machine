using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string stockFilename = "vendingmachine.csv";
            string logFilename = "Log.txt";

            // Check if we're passed a commandline argument, and if we are, use that for our stockfile
            // HACK: Extension idea: add more commandline arguments and implement switches.
            if (args.Length > 0)
            {
                stockFilename = args[0];
            }

            // First we need to set up our file IO
            IDataManager dataManager = new DataManager(logFilename);

            // Then we need to load up our stock items
            List<VendingMachineItem> items = dataManager.LoadItems(stockFilename);

            // Here we initialize the vending machine with its stock and our UI manager
            VendingMachine vendingMachine = new VendingMachine(items);
            IUIManager uiManager = new ConsoleManager();
            
            // And we're off to the races!
            UserInterface userInterface = new UserInterface(vendingMachine, dataManager, uiManager);
            userInterface.RunInterface();



        }
    }
}

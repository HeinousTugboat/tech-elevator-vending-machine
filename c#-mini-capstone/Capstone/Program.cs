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
            IDataManager dataManager = new DataManager("Log.txt");
            List<VendingMachineItem> items = dataManager.LoadItems("vendingmachine.csv");
            VendingMachine vendingMachine = new VendingMachine(items);
            IUIManager uiManager = new ConsoleManager();
            
            UserInterface userInterface = new UserInterface(vendingMachine, dataManager, uiManager);
            userInterface.RunInterface();



        }
    }
}

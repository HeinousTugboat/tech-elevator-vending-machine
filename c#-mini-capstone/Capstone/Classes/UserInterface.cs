using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    enum UIAction
    {
        FeedMoney, // User wants to add money
        DisplayItems, // User wants to see product selection grid
        SelectProduct, // User has selected a product on the UI
        PurchaseItem, // User wants to purchase currently selected product
        FinishTransaction, // User is done, wants out, screw you
        Exit // BYE NOW.
    }

    public class UserInterface
    {
        private VendingMachine vendingMachine;

        public UserInterface(VendingMachine vendingMachine)
        {

        }

        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("This is the UserInterface");
                Console.ReadLine();
            }

        }

    }
}

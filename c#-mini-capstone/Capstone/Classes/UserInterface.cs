using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public enum UIAction
    {
        MainMenu, // User viewing the main menu
        FeedMoney, // User wants to add money
        DisplayItems, // User wants to see product selection grid
        SelectProduct, // User has selected a product on the UI
        PurchaseItem, // User wants to purchase currently selected product
        FinishTransaction, // User is done, wants out, screw you
        Exit // BYE NOW.
    }

    interface IUIManager
    {
        decimal CurrentBalance { get; set; }
        VendingMachineItem CurrentSelection { get; }

        UIAction PrintMainMenu();
        UIAction PrintPurchasingMenu();
        UIAction PrintProductSelectionMenu(Dictionary<ItemType, VendingMachineItem[]> items);
    }

    public class UserInterface
    {
        private VendingMachine vendingMachine;
        private IUIManager uiManager;

        public UserInterface(VendingMachine vendingMachine)
        {
            uiManager = new ConsoleManager();
        }

        public void RunInterface()
        {
            bool done = false;
            UIAction action = UIAction.MainMenu;

            while (!done)
            {
                uiManager.CurrentBalance += 0.01M;

                switch (action)
                {
                    case UIAction.MainMenu:
                        action = uiManager.PrintMainMenu();
                        break;
                    case UIAction.Exit:
                        done = true;
                        break;
                    case UIAction.PurchaseItem:
                        action = uiManager.PrintPurchasingMenu();
                        break;
                    case UIAction.DisplayItems:
                        action = uiManager.PrintProductSelectionMenu(new Dictionary<ItemType, VendingMachineItem[]> { { ItemType.Candy, new VendingMachineItem[10] { null, null, null, null, null, null, null, null, null, null } } });
                        break;
                    default:
                        action = UIAction.MainMenu;
                        break;
                }
            }
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
        }
    }
}

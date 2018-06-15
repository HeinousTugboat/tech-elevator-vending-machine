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
        void PrintPurchaseConfirmation(VendingMachineItem item);
        void PrintChangeConfirmation(decimal changeDispensed);

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
                        Dictionary<ItemType, VendingMachineItem[]> sampleItems = new Dictionary<ItemType, VendingMachineItem[]> {
                            { ItemType.Chip, new VendingMachineItem[10]
                                {
                                    new VendingMachineItem("Potato Crisps", 3.05M, ItemType.Chip, 1),
                                    new VendingMachineItem("Stackers", 1.45M, ItemType.Chip, 2),
                                    new VendingMachineItem("Grain Waves", 2.75M, ItemType.Chip, 3),
                                    new VendingMachineItem("Cloud Popcorn", 3.65M, ItemType.Chip, 4),
                                    null, null, null, null, null, null
                            } },
                            { ItemType.Candy, new VendingMachineItem[10]
                                {
                                    new VendingMachineItem("Moonpie", 1.80M, ItemType.Candy, 1),
                                    new VendingMachineItem("Cowtales", 1.50M, ItemType.Candy, 2),
                                    new VendingMachineItem("Wonka Bars", 1.50M, ItemType.Candy, 3),
                                    new VendingMachineItem("Crunchie", 1.75M, ItemType.Candy, 4),
                                    null, null, null, null, null, null
                            } },
                            { ItemType.Drink, new VendingMachineItem[10]
                                {
                                    new VendingMachineItem("Cola", 1.25M, ItemType.Drink, 1),
                                    new VendingMachineItem("Dr. Salt", 1.50M, ItemType.Drink, 2),
                                    new VendingMachineItem("Mountain Melter", 1.50M, ItemType.Drink, 3),
                                    new VendingMachineItem("Heavy", 1.50M, ItemType.Drink, 4),
                                    null, null, null, null, null, null
                            } },
                            { ItemType.Gum, new VendingMachineItem[10]
                                {
                                    new VendingMachineItem("U-Chews", 0.85M, ItemType.Gum, 1),
                                    new VendingMachineItem("Little League Chew", 0.95M, ItemType.Gum, 2),
                                    new VendingMachineItem("Chiclets", 0.75M, ItemType.Gum, 3),
                                    new VendingMachineItem("Triplemint", 0.75M, ItemType.Gum, 4),
                                    null, null, null, null, null, null
                            } }
                        };
                        action = uiManager.PrintProductSelectionMenu(sampleItems);
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

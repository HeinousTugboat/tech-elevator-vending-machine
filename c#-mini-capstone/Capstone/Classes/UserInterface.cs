using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public enum UIAction
    {
        DisplayMainMenu, // User viewing the main menu
        FeedMoney, // User wants to add money
        ReviewItems, // User wants to see items before purchasing
        DisplayItems, // User wants to see product selection grid
        DisplayPurchasing, // User wants to see purchasing menu
        CheckItem, // User has selected a product on the UI
        PurchaseItem, // User wants to purchase & dispense currently selected product
        FinishTransaction, // User is done, wants out, screw you
        Exit, // BYE NOW.
        SalesReport
    }

    public interface IUIManager
    {
        decimal CurrentBalance { get; set; }
        VendingMachineItem CurrentSelection { get; }

        UIAction PrintMainMenu();
        UIAction PrintPurchasingMenu();
        UIAction PrintProductSelectionMenu(Dictionary<ItemType, VendingMachineItem[]> items, UIAction actionToTake);
        int FeedMoneyRequest();
        void PrintPurchaseConfirmation(VendingMachineItem item);
        void PrintChangeConfirmation(decimal changeDispensed);

    }

    public class UserInterface
    {
        private VendingMachine vendingMachine;
        private IUIManager uiManager;
        private IDataManager dataManager;

        public UserInterface(VendingMachine vendingMachine, IDataManager dataManager, IUIManager uiManager)
        {
            this.vendingMachine = vendingMachine;
            this.dataManager = dataManager;
            this.uiManager = uiManager;

        }

        public void RunInterface()
        {
            bool done = false;
            UIAction action = UIAction.DisplayMainMenu;

            while (!done)
            {
                uiManager.CurrentBalance += 0.01M;
                VendingMachineItem selection = uiManager.CurrentSelection;
                try
                {
                    switch (action)
                    {
                        case UIAction.DisplayMainMenu:
                            action = uiManager.PrintMainMenu();
                            break;
                        case UIAction.DisplayPurchasing:
                            action = uiManager.PrintPurchasingMenu();
                            break;
                        case UIAction.PurchaseItem:
                            selection = uiManager.CurrentSelection;
                            if (selection != null)
                            {
                                vendingMachine.PurchaseItem(selection.Type, selection.Slot);
                            }
                            action = UIAction.DisplayPurchasing;
                            break;
                        case UIAction.CheckItem:
                            selection = uiManager.CurrentSelection;
                            if (selection != null)
                            {
                                vendingMachine.CheckItem(selection.Type, selection.Slot);
                            }
                            action = UIAction.DisplayMainMenu;
                            break;
                        case UIAction.ReviewItems:
                            action = uiManager.PrintProductSelectionMenu(vendingMachine.GetAllItems(), UIAction.CheckItem);
                            break;
                        case UIAction.DisplayItems:
                            action = uiManager.PrintProductSelectionMenu(vendingMachine.GetAllItems(), UIAction.PurchaseItem);
                            break;
                        case UIAction.FeedMoney:
                            int amount = uiManager.FeedMoneyRequest();
                            vendingMachine.FeedMoney(amount);
                            action = UIAction.DisplayPurchasing;
                            break;
                        case UIAction.FinishTransaction:
                            VendingMachineTransaction transaction = vendingMachine.FinishTransaction();
                            uiManager.PrintChangeConfirmation(transaction.Amount);
                            break;
                        case UIAction.SalesReport:
                            action = UIAction.DisplayMainMenu;
                            // I can't decide between this Query syntax or that Lambda syntax..
                            //var items = from itemArray in vendingMachine.GetAllItems().Values
                            //            from item in itemArray
                            //            where item != null
                            //            select item;
                            IEnumerable<VendingMachineItem> items = vendingMachine.GetAllItems().Values
                                .SelectMany(x => x).Where(x => x != null);
                            dataManager.GenerateSalesReport(items.ToList());
                            break;
                        case UIAction.Exit:
                            done = true;
                            break;
                        default:
                            action = UIAction.DisplayMainMenu;
                            break;
                    }
                }
                catch (NotImplementedException e)
                {
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("FUNCTIONALITY NOT IMPLEMENTED!");
                    Console.WriteLine(e.Message);
                    action = UIAction.DisplayMainMenu;
                    Console.ReadLine();
                }
            }
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
        }
    }
}

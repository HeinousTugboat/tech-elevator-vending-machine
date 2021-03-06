﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    /**
     * UIActions represent any possible action we can expect back from our UI 
     * Manager. This should be every possible way the user can actually interact 
     * with our model.
     **/
    public enum UIAction
    {
        DisplayMainMenu,    // Display the main menu
        DisplayPurchasing,  // Display purchasing menu
        FeedMoney,          // Display Feed Money interface, expect amount back
        ReviewItems,        // Display Item list, check selection
        DisplayItems,       // Display Item list, purchase selection
        CheckItem,          // Check currently selected item
        PurchaseItem,       // Purchase and Dispense currently selected item
        FinishTransaction,  // End transaction, provide change, display change dispensed
        Exit,               // Exit program
        SalesReport         // Generate current sales report
    }

    /**
     * IUIManager's the interface for our UI Manager we're writing to. Ideally 
     * this is abstract enough that we can plop in an HtmlManager or UWPManager 
     * or whatever kind of manager. We shouldn't care what's driving us.
     **/
    public interface IUIManager
    {
        decimal CurrentBalance { get; set; }
        ItemType CurrentType { get; }
        int CurrentSlot { get; }

        UIAction PrintMainMenu();
        UIAction PrintPurchasingMenu();
        UIAction PrintProductSelectionMenu(Dictionary<ItemType, VendingMachineItem[]> items, UIAction defaultAction);
        int FeedMoneyRequest();
        void PrintItemCheck(VendingMachineItem item);
        void PrintTransaction(VendingMachineTransaction transaction);
        void PrintException(Exception e);
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
            dataManager.WriteTransaction(new VendingMachineTransaction(TransactionType.MachineStart), vendingMachine.CurrentBalance);
        }

        public void RunInterface()
        {
            bool done = false;
            UIAction action = UIAction.DisplayMainMenu;

            while (!done)
            {
                uiManager.CurrentBalance = vendingMachine.CurrentBalance;
                VendingMachineTransaction transaction;
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
                        case UIAction.FeedMoney:
                            int amount = uiManager.FeedMoneyRequest();
                            transaction = vendingMachine.FeedMoney(amount);
                            dataManager.WriteTransaction(transaction, vendingMachine.CurrentBalance);
                            uiManager.CurrentBalance = vendingMachine.CurrentBalance;
                            uiManager.PrintTransaction(transaction);
                            action = UIAction.DisplayPurchasing;
                            break;
                        case UIAction.ReviewItems:
                            action = uiManager.PrintProductSelectionMenu(vendingMachine.GetAllItems(), UIAction.CheckItem);
                            break;
                        case UIAction.DisplayItems:
                            action = uiManager.PrintProductSelectionMenu(vendingMachine.GetAllItems(), UIAction.PurchaseItem);
                            break;
                        case UIAction.CheckItem:
                            VendingMachineItem item = null;
                            item = vendingMachine.CheckItem(uiManager.CurrentType, uiManager.CurrentSlot);
                            uiManager.PrintItemCheck(item);
                            action = UIAction.DisplayMainMenu;
                            break;
                        case UIAction.PurchaseItem:
                            transaction = vendingMachine.PurchaseItem(uiManager.CurrentType, uiManager.CurrentSlot);
                            dataManager.WriteTransaction(transaction, vendingMachine.CurrentBalance);
                            uiManager.CurrentBalance = vendingMachine.CurrentBalance;
                            uiManager.PrintTransaction(transaction);
                            action = UIAction.DisplayPurchasing;
                            break;
                        case UIAction.FinishTransaction:
                            transaction = vendingMachine.FinishTransaction();
                            dataManager.WriteTransaction(transaction, vendingMachine.CurrentBalance);
                            uiManager.PrintTransaction(transaction);
                            action = UIAction.DisplayMainMenu;
                            break;
                        case UIAction.Exit:
                            done = true;
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
                            uiManager.PrintTransaction(new VendingMachineTransaction(TransactionType.GenerateSalesReport));
                            break;
                        default:
                            action = UIAction.DisplayMainMenu;
                            break;
                    }
                }
                catch (Exception e)
                {
                    uiManager.PrintException(e);
                    action = UIAction.DisplayMainMenu;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
        }
    }
}

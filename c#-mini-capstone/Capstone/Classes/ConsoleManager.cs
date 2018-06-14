using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.ConsoleColor;

namespace Capstone.Classes
{
    interface IUIManager
    {
        decimal CurrentBalance { get; set; }
        VendingMachineItem CurrentSelection { get; }

        UIAction PrintMainMenu();
        UIAction PrintPurchasingMenu();
        UIAction PrintProductSelectionMenu(Dictionary<ItemType, VendingMachineItem> items);
    }

    public class ConsoleManager : IUIManager
    {
        // Used to pass in the balance from the UI class.
        public decimal CurrentBalance { get; set; }

        // Used to pass whatever's currently selected *out* to the UI.
        public VendingMachineItem CurrentSelection { get; private set; }

        // Default window colors, can be whatever. Normally White on Black.
        private ConsoleColor baseFG = White;
        private ConsoleColor baseBG = Black;
        private ConsoleColor hiliteFG = Green;
        private ConsoleColor hiliteBG = Black;

        public ConsoleManager()
        {
            // Setting our console colors to our default ones.
            BackgroundColor = baseBG;
            ForegroundColor = baseFG;

            // Resetting the buffer and window so that we 
            //   a.) don't have any scroll bar and 
            //   b.) don't have any excess space
            BufferWidth = WindowWidth;
            BufferHeight = WindowHeight;
            SetWindowSize(BufferWidth, BufferHeight);

            // Hide that ugly little cursor. We'll do it ourselves.
            CursorVisible = false;
            Title = "Vend-o-matic 600";

            // Reset the console window so our formatting will take effect across the whole thing
            Clear();
            SetCursorPosition(0, 0);
            PrintBalance();
        }

        private void PrintBalance()
        {
            CursorTop = WindowHeight - 1;
            string balance = "Currently Available: " + CurrentBalance.ToString("C");
            CursorLeft = WindowWidth - balance.Length - 1;
            Write(balance);
            SetCursorPosition(0, 0);
        }

        // Sets current color based on BaseBG/FG.
        private void SetColor(bool hilite = false)
        {
            if (hilite)
            {
                ForegroundColor = hiliteFG;
                BackgroundColor = hiliteBG;
            }
            else
            {
                ForegroundColor = baseFG;
                BackgroundColor = baseBG;
            }
        }

        private void MainMenuOutput(int selectedOption = 0)
        {
            SetCursorPosition(2, 2);
            SetColor(selectedOption == 0);
            Write("(1) Display Vending Machine Items ");

            SetCursorPosition(2, 3);
            SetColor(selectedOption == 1);
            Write("(2) Purchase ");

            SetCursorPosition(2, 4);
            SetColor(selectedOption == 2);
            Write("(3) Exit ");

            SetColor();
        }

        public UIAction PrintMainMenu()
        {
            int selectedOption = 0;
            bool isCurrentlyInMenu = true;
            ConsoleKeyInfo keyPress;
            PrintBalance();

            while (isCurrentlyInMenu)
            {
                MainMenuOutput(selectedOption);
                keyPress = ReadKey(true);

                switch (keyPress.Key)
                {
                    // Navigate menu using up/down arrow, select with enter/escape/space
                    case ConsoleKey.UpArrow:
                        selectedOption--;
                        if (selectedOption < 0)
                        {
                            selectedOption = 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption++;
                        if (selectedOption > 2)
                        {
                            selectedOption = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Escape:
                    case ConsoleKey.Spacebar:
                        isCurrentlyInMenu = false;
                        break;

                    // Select specific menu choice with 1/2/3
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        selectedOption = 0;
                        isCurrentlyInMenu = false;
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        selectedOption = 1;
                        isCurrentlyInMenu = false;
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        selectedOption = 2;
                        isCurrentlyInMenu = false;
                        break;
                }
            }

            if (selectedOption == 0)
            {
                return UIAction.DisplayItems;
            }
            if (selectedOption == 1)
            {
                return UIAction.PurchaseItem;
            }
            return UIAction.Exit;
        }

        public UIAction PrintPurchasingMenu()
        {
            return UIAction.Exit;
        }

        public UIAction PrintProductSelectionMenu(Dictionary<ItemType, VendingMachineItem> items)
        {
            return UIAction.Exit;
        }

        // WHAT DOES YOU MANAGE?
        // Console. Duh.
        // Print Main Menu
        //   Display Items
        //   Purchase Item
        //   Exit
        // Print Purchasing Menu
        //   Feed Money
        //   Select Product
        //   Finish Transaction
        // Print Product Selection Menu
        //   Print all slots?

        // Print Messages such as 
        //   Out of Stock 
        //   Shit's Broked
        //   Dispensary Commentary ("Crunch Crunch, Yum!")..
        //   Product Code doesn't exist.. if we use menu, not necessary? Just don't show unavailable stuff..

        // Current Balance!

    }
}

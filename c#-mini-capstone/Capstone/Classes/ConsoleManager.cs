﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.ConsoleColor;

namespace Capstone.Classes
{

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

        // Added for diagnostic purposes.
        private UIAction LastAction;

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

            // Add UTF8 encoding so we can draw with pipes!
            OutputEncoding = Encoding.UTF8;

            // Reset the console window so our formatting will take effect across the whole thing
            Clear();
            SetCursorPosition(0, 0);
            PrintBalance();
        }

        private void PrintBalance()
        {
            Clear();

            SetCursorPosition(1, WindowHeight - 1);
            Write("Current Action: " + LastAction + "\t\t");

            string balance = "Currently Available: " + CurrentBalance.ToString("C");
            SetCursorPosition(WindowWidth - balance.Length - 1, WindowHeight - 1);
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
            PrintBalance();
            int selectedOption = 0;
            bool isCurrentlyInMenu = true;
            ConsoleKeyInfo keyPress;

            while (isCurrentlyInMenu)
            {
                MainMenuOutput(selectedOption);
                keyPress = ReadKey(true);

                switch (keyPress.Key)
                {
                    // Navigate menu using up/down arrow, select with enter/space
                    case ConsoleKey.UpArrow:
                        if (--selectedOption < 0)
                        {
                            selectedOption = 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (++selectedOption > 2)
                        {
                            selectedOption = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        isCurrentlyInMenu = false;
                        break;

                    // Select specific menu choice with 1/2/3/escape
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
                    case ConsoleKey.Escape:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        selectedOption = 2;
                        isCurrentlyInMenu = false;
                        break;
                }
            }

            if (selectedOption == 0)
            {
                LastAction = UIAction.DisplayItems;
            }
            if (selectedOption == 1)
            {
                LastAction = UIAction.PurchaseItem;
            }
            if (selectedOption == 2)
            {
                LastAction = UIAction.Exit;
            }
            return LastAction;
        }

        private void PurchasingMenuOutput(int selectedOption = 0)
        {
            SetCursorPosition(2, 2);
            SetColor(selectedOption == 0);
            Write("(1) Feed Money ");

            SetCursorPosition(2, 3);
            SetColor(selectedOption == 1);
            Write("(2) Select Product ");

            SetCursorPosition(2, 4);
            SetColor(selectedOption == 2);
            Write("(3) Finish Transaction ");

            SetColor();
        }

        public UIAction PrintPurchasingMenu()
        {
            PrintBalance();
            int selectedOption = 0;
            bool isCurrentlyInMenu = true;
            ConsoleKeyInfo keyPress;

            while (isCurrentlyInMenu)
            {
                PurchasingMenuOutput(selectedOption);
                keyPress = ReadKey(true);

                switch (keyPress.Key)
                {
                    // Navigate menu using up/down arrow, select with enter/escape/space
                    case ConsoleKey.UpArrow:
                        if (--selectedOption < 0)
                        {
                            selectedOption = 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (++selectedOption > 2)
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
                LastAction = UIAction.FeedMoney;
            }
            if (selectedOption == 1)
            {
                LastAction = UIAction.SelectProduct;
            }
            if (selectedOption == 2)
            {
                LastAction = UIAction.MainMenu;
            }
            return LastAction;
        }

        //{
        //    SetCursorPosition(2, 2);
        //    SetColor(selectedOption == 0);
        //    Write("(1) Feed Money ");

        //    SetCursorPosition(2, 3);
        //    SetColor(selectedOption == 1);
        //    Write("(2) Select Product ");

        //    SetCursorPosition(2, 4);
        //    SetColor(selectedOption == 2);
        //    Write("(3) Finish Transaction ");

        //    SetColor();
        //}

        private void ProductBoxOutput(int row, int col, string name, decimal price)
        {
            string border = new string('\u2500', name.Length + 2);
            string spaces = new string(' ', name.Length + 2);
            string priceString = price.ToString("C");
            int priceOffset = (name.Length + 1) / 2 - priceString.Length / 2;
            int priceSpace = name.Length + 2 - priceString.Length - priceOffset;

            SetCursorPosition(col, row);
            Write("\u250C" + border + "\u2510");
            SetCursorPosition(col, row + 1);
            Write("\u2502" + spaces + "\u2502");
            SetCursorPosition(col, row + 2);
            Write("\u2502 " + name + " \u2502");
            SetCursorPosition(col, row + 3);
            Write("\u2502" + new string(' ', priceOffset) + priceString + new string(' ', priceSpace) + "\u2502");
            SetCursorPosition(col, row + 4);
            Write("\u2502" + spaces + "\u2502");
            SetCursorPosition(col, row + 5);
            Write("\u2514" + border + "\u2518");
        }

        public UIAction PrintProductSelectionMenu(Dictionary<ItemType, VendingMachineItem[]> items)
        {
            PrintBalance();
            int selectedRow = 0;
            int selectedColumn = 0;

            // Testing ideas for UI.
            SetColor(true);
            int top = 1;
            int left = 2;
            ProductBoxOutput(top, left, "Potato Crisps", 3.05m);
            SetColor();
            left += "Potato Crisps".Length + 7;
            ProductBoxOutput(top, left, "Stackers", 1.45m);
            left += "Stackers".Length + 7;
            ProductBoxOutput(top, left, "Grain Waves", 2.75m);
            left += "Grain Waves".Length + 7;
            ProductBoxOutput(top, left, "Cloud Popcorn", 3.65m);
            left = 2;
            top += 6;
            ProductBoxOutput(top, left, "Moonpie", 1.80m);
            left += "Moonpie".Length + 7;
            ProductBoxOutput(top, left, "Cowtales", 1.50m);
            left += "Cowtales".Length + 7;
            ProductBoxOutput(top, left, "Wonka Bar", 1.50m);
            left += "Wonka Bar".Length + 7;
            ProductBoxOutput(top, left, "Crunchie", 1.75m);

            LastAction = UIAction.MainMenu;
            // End idea test.

            bool isCurrentlyInMenu = true;
            ConsoleKeyInfo keyPress;

            while (isCurrentlyInMenu)
            {
                keyPress = ReadKey(true);
                switch (keyPress.Key)
                {
                    // Navigate menu using up/down arrow, select with enter/escape/space
                    case ConsoleKey.UpArrow:
                        if (--selectedRow < 0)
                        {
                            selectedRow = 3;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (++selectedRow > 3)
                        {
                            selectedRow = 0;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (--selectedColumn < 0)
                        {
                            selectedColumn = 9;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (++selectedColumn > 9)
                        {
                            selectedColumn = 0;
                        }
                        break;
                    case ConsoleKey.Q:
                        LastAction = UIAction.Exit;
                        isCurrentlyInMenu = false;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Escape:
                    case ConsoleKey.Spacebar:
                        isCurrentlyInMenu = false;
                        break;
                }
            }
            
            return LastAction;
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

using System;
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
        // Check out my totally awesome logo!
        public string[] Logo = {
            @" __   __           _      ___      __  __      _   _       __  __   __  ",
            @" \ \ / /__ _ _  __| |___ / _ \ ___|  \/  |__ _| |_(_)__   / / /  \ /  \ ",
            @"  \ V / -_) ' \/ _` |___| (_) |___| |\/| / _` |  _| / _| / _ \ () | () |",
            @"   \_/\___|_||_\__,_|    \___/    |_|  |_\__,_|\__|_\__| \___/\__/ \__/ "
        };

        // Used to pass in the balance from the UI class.
        public decimal CurrentBalance { get; set; }

        // Used to pass whatever's currently selected *out* to the UI.
        public VendingMachineItem CurrentSelection { get; private set; }

        // Default window colors, can be whatever. Normally White on Black.
        private readonly ConsoleColor BaseFG = White;
        private readonly ConsoleColor BaseBG = Black;
        private readonly ConsoleColor HiliteFG = Green;
        private readonly ConsoleColor HiliteBG = Black;

        // Added for diagnostic purposes.
        private UIAction LastAction;

        public ConsoleManager()
        {
            // Setting our console colors to our default ones.
            BackgroundColor = BaseBG;
            ForegroundColor = BaseFG;

            // Resetting the buffer and window so that we 
            //   a.) don't have any scroll bar and 
            //   b.) don't have any excess space
            BufferWidth = WindowWidth;
            BufferHeight = WindowHeight;
            SetWindowSize(BufferWidth, BufferHeight);

            // Hide that ugly little cursor. We'll do it ourselves.
            CursorVisible = false;
            Title = "Vend-O-Matic 600";

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
            SetColor(Gray);
            Write("Action: ");
            SetColor(White);
            Write(LastAction);

            string balance = "   Available: ";
            string balanceAmount = CurrentBalance.ToString("C");
            string selection = "";
            string selectionName = "";
            if (CurrentSelection != null)
            {
                selection = "Selection: ";
                selectionName = CurrentSelection.Name;
            }
            SetCursorPosition(WindowWidth - (balance.Length + 1 + balanceAmount.Length + selection.Length + selectionName.Length), WindowHeight - 1);
            SetColor(Gray);
            Write(selection);
            SetColor(White);
            Write(selectionName);
            SetColor(Gray);
            Write(balance);
            SetColor(White);
            Write(balanceAmount);

            SetColor(DarkGreen);

            SetCursorPosition(WindowWidth - 74, WindowHeight - 6);
            Write(Logo[0]);
            SetCursorPosition(WindowWidth - 74, WindowHeight - 5);
            Write(Logo[1]);
            SetCursorPosition(WindowWidth - 74, WindowHeight - 4);
            Write(Logo[2]);
            SetCursorPosition(WindowWidth - 74, WindowHeight - 3);
            Write(Logo[3]);

            SetColor();
            SetCursorPosition(0, 0);
        }

        // Sets current color based on BaseBG/FG.
        private void SetColor(bool hilite = false)
        {
            if (hilite)
            {
                SetColor(HiliteFG, HiliteBG);
            }
            else
            {
                SetColor(BaseFG, BaseBG);
            }
        }

        // Sets arbitrary colors, defaults background to Black.
        private void SetColor(ConsoleColor fg, ConsoleColor bg = Black)
        {
            ForegroundColor = fg;
            BackgroundColor = bg;
        }

        // Actually Prints the main menu.
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

        // Method called by UserInterface to get action from main menu.
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

        // Actually prints the purchasing menu.
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

        // Method called by UserInterface to get action from purchasing menu.
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

        // Method called by UserInterface to get action from selection menu.
        public UIAction PrintProductSelectionMenu(Dictionary<ItemType, VendingMachineItem[]> items)
        {
            PrintBalance();
            int selectedRow = 0;
            int selectedColumn = 0;
            int totalRows = 19; // 20 rows, indexed at 0.
            int totalColumns = 1; // 2 columns, indexed at 0.
            int tablePadding = 6;
            int currentRow = 0;
            VendingMachineItem selectedItem = null;

            bool isCurrentlyInMenu = true;
            ConsoleKeyInfo keyPress;

            while (isCurrentlyInMenu)
            {
                foreach (KeyValuePair<ItemType, VendingMachineItem[]> itemList in items)
                {
                    bool activeColumn = false;
                    bool activeRow = false;
                    switch (itemList.Key)
                    {
                        case ItemType.Candy:
                        case ItemType.Chip:
                            currentRow = -1;
                            CursorTop = 1;
                            break;
                        case ItemType.Drink:
                        case ItemType.Gum:
                            currentRow = 9;
                            CursorTop = 13;
                            break;
                    }

                    foreach (VendingMachineItem item in itemList.Value)
                    {
                        activeRow = ++currentRow == selectedRow;
                        switch (itemList.Key)
                        {
                            case ItemType.Chip:
                            case ItemType.Drink:
                                CursorLeft = tablePadding;
                                activeColumn = selectedColumn == 0;
                                break;
                            case ItemType.Candy:
                            case ItemType.Gum:
                                CursorLeft = (WindowWidth - 2 * tablePadding) / 2;
                                activeColumn = selectedColumn == 1;
                                break;
                        }

                        CursorTop++;

                        if (activeColumn && activeRow)
                        {
                            selectedItem = item;
                        }

                        if (item != null)
                        {

                            SetColor(activeColumn && activeRow);
                            Write(item);
                        }
                        else
                        {
                            if (activeColumn && activeRow)
                            {
                                SetColor(DarkGreen);
                            }
                            else
                            {
                                SetColor(DarkGray);
                            }
                            Write("  -- no item --");
                        }
                        SetColor();
                    }
                }
                keyPress = ReadKey(true);
                switch (keyPress.Key)
                {
                    // Navigate menu using up/down arrow, select with enter/space, bail to main menu with escape, select column/row with letter/number
                    case ConsoleKey.UpArrow:
                        if (--selectedRow < 0)
                        {
                            selectedRow = totalRows;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (++selectedRow > totalRows)
                        {
                            selectedRow = 0;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (--selectedColumn < 0)
                        {
                            selectedColumn = totalColumns;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (++selectedColumn > totalColumns)
                        {
                            selectedColumn = 0;
                        }
                        break;
                    case ConsoleKey.A:
                        selectedColumn = 0;
                        selectedRow = 0;
                        //if (selectedRow >= 10)
                        //{
                        //    selectedRow -= 10;
                        //}
                        break;
                    case ConsoleKey.B:
                        selectedColumn = 1;
                        selectedRow = 0;
                        //if (selectedRow >= 10)
                        //{
                        //    selectedRow -= 10;
                        //}
                        break;
                    case ConsoleKey.C:
                        selectedColumn = 0;
                        selectedRow = 10;
                        //if (selectedRow < 10)
                        //{
                        //    selectedRow += 10;
                        //}
                        break;
                    case ConsoleKey.D:
                        selectedColumn = 1;
                        selectedRow = 10;
                        //if (selectedRow < 10)
                        //{
                        //    selectedRow += 10;
                        //}
                        break;
                    case ConsoleKey.D1:
                        selectedRow = selectedRow < 10 ? 0 : 10;
                        break;
                    case ConsoleKey.D2:
                        selectedRow = selectedRow < 10 ? 1 : 11;
                        break;
                    case ConsoleKey.D3:
                        selectedRow = selectedRow < 10 ? 2 : 12;
                        break;
                    case ConsoleKey.D4:
                        selectedRow = selectedRow < 10 ? 3 : 13;
                        break;
                    case ConsoleKey.D5:
                        selectedRow = selectedRow < 10 ? 4 : 14;
                        break;
                    case ConsoleKey.D6:
                        selectedRow = selectedRow < 10 ? 5 : 15;
                        break;
                    case ConsoleKey.D7:
                        selectedRow = selectedRow < 10 ? 6 : 16;
                        break;
                    case ConsoleKey.D8:
                        selectedRow = selectedRow < 10 ? 7 : 17;
                        break;
                    case ConsoleKey.D9:
                        selectedRow = selectedRow < 10 ? 8 : 18;
                        break;
                    case ConsoleKey.D0:
                        selectedRow = selectedRow < 10 ? 9 : 19;
                        break;


                    case ConsoleKey.Q:
                        LastAction = UIAction.Exit;
                        isCurrentlyInMenu = false;
                        break;
                    case ConsoleKey.Escape:
                        LastAction = UIAction.MainMenu;
                        isCurrentlyInMenu = false;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        CurrentSelection = selectedItem;
                        LastAction = UIAction.SelectProduct;
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

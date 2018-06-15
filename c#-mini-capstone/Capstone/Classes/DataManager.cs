using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public interface IDataManager
    {
        // Called by UserInterface to load items to build VendingMachine with
        List<VendingMachineItem> LoadItems(string filename);

        // Called by UserInterface after every Transaction built
        void WriteTransaction(VendingMachineTransaction transaction, decimal currentBalance);

        // Called by UserInterface when sales report requested
        void GenerateSalesReport(List<VendingMachineItem> items);
    }

    public class DataManager : IDataManager
    {
        private string LogFile { get; }

        public DataManager(string logFile)
        {
            LogFile = logFile;
            // Print out vending machine starting up in log.
        }

        public List<VendingMachineItem> LoadItems(string filename)
        {
            List<VendingMachineItem> items = new List<VendingMachineItem>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split('|');
                        ItemType type = (ItemType)line[0][0];
                        int slot = int.Parse(line[0].Substring(1));
                        string name = line[1];
                        decimal price = decimal.Parse(line[2]);

                        VendingMachineItem item = new VendingMachineItem(name, price, type, slot);
                        items.Add(item);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Unable to load file: {e.Message}");
            }
            return items;
        }

        public void WriteTransaction(VendingMachineTransaction transaction, decimal currentBalance)
        {
            using (StreamWriter sw = new StreamWriter(LogFile, false))
            {
                sw.WriteLine($"{transaction.Timestamp} {transaction.Type} {transaction.Amount.ToString("C")} {currentBalance.ToString("C")}");
            };
        }

        public void GenerateSalesReport(List<VendingMachineItem> items)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("sales-report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt", false))
                {
                    decimal total = 0;
                    foreach (VendingMachineItem item in items)
                    {
                        decimal price = item.Price * (5 - item.Quantity);
                        total += price;
                        sw.WriteLine($"{item.Name}|{5 - item.Quantity}");
                    }
                    sw.WriteLine($"\n**TOTAL SALES** {total.ToString("C")}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Everything broke: {e.Message}");
                throw;
            }
        }
    }
}

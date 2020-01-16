using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public static class FileAccess
    {
        // This class should contain any and all details of access to files
        private static string InventoryFile { get; } = @"C:\Catering\cateringsystem.csv";
        private static string LogFile { get; } = @"C:\Catering\logfile.csv";
        private static string SalesReportFile { get; } = @"C:\Catering\TotalSales.rpt";

        public static bool LoadInventory()
        {
            Dictionary<string, CateringItem> inventory = new Dictionary<string, CateringItem>();

            try
            {
                using (StreamReader sr = new StreamReader(InventoryFile))
                {
                    while (!sr.EndOfStream)
                    {
                        CateringItem cateringItem = BuildItem(sr.ReadLine());
                        if (cateringItem != null)
                        {
                            inventory.Add(cateringItem.PID, cateringItem);
                        }
                    }

                }
                Catering.Inventory = inventory;
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }


        private static CateringItem BuildItem(string rawData)
        {
            string[] strArray = rawData.Split('|');
            CateringItem cateringItem = null;
            try
            {
                cateringItem = new CateringItem(strArray[0], strArray[1], decimal.Parse(strArray[2]), strArray[3]);
            }
            catch (FormatException ex)
            {
                // wasn't a cateringItem 
            }


            return cateringItem;
        }

        public static bool LogItem(string log)
        {
            try
            {
                using (StreamWriter swLog = new StreamWriter(LogFile, true))
                {
                    swLog.WriteLine(System.DateTime.Now + " " + log);
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        private static CateringItem ReadSalesReport (string input)
        {
            CateringItem cateringItem = null;

            try
            {
                string[] lineArray = input.Split("|");
                cateringItem = new CateringItem(lineArray[0], int.Parse(lineArray[1]));
            } 
            catch (FormatException ex)
            {
                // not a CateringItem
            }
            catch (IndexOutOfRangeException ex)
            {
                // not a CateringItem
            }

            return cateringItem;
        }

        public static bool GenerateSalesReport()
        {
            Dictionary<string, CateringItem> reportInput = new Dictionary<string, CateringItem>();

            try
            {
                using (StreamReader reader = new StreamReader(SalesReportFile))
                {
                    while (!reader.EndOfStream)
                    {
                        CateringItem cateringItem = ReadSalesReport(reader.ReadLine());
                        if (cateringItem != null)
                        {
                            reportInput.Add(cateringItem.Name, cateringItem);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // file doesn't exist, create it
            }
            catch (IOException ex)
            {
                return false;
            }
            
            foreach (KeyValuePair<string, CateringItem> kvp in Catering.Cart)
            {
                if (!reportInput.ContainsKey(kvp.Value.Name))
                {
                    reportInput.Add(kvp.Value.Name, kvp.Value);
                }
                else
                {
                    reportInput[kvp.Value.Name].AmountSold += kvp.Value.AmountSold;
                    reportInput[kvp.Value.Name].Price = kvp.Value.Price;
                }
            }

            StringBuilder sb = new StringBuilder();

            try
            {
                using (StreamWriter writer = new StreamWriter(SalesReportFile, false))
                {
                    decimal reportTotal = 0.00M;
                    foreach (KeyValuePair<string, CateringItem> kvp in reportInput)
                    {
                        decimal totalSold = kvp.Value.AmountSold * kvp.Value.Price;
                        reportTotal += totalSold;
                        sb.AppendLine($"{kvp.Value.Name}|{kvp.Value.AmountSold}|{totalSold}");
                    }
                    sb.AppendLine($"\n**TOTAL SALES** ${reportTotal}");
                    writer.Write(sb.ToString());
                }
            }
            catch (IOException ex)
            {
                return false;
            }

            return true;
        }
    }
}


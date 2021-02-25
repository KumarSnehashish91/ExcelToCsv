using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCSV
{
    public class ExportData
    {
        public static void ToCSVWithFile(DataTable dtDataTable, string strFilePath)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames
                    = dtDataTable.Columns.Cast<DataColumn>().
                        Select(column => column.ColumnName);
            sb.AppendLine(string.Join("^", columnNames));

            foreach (DataRow row in dtDataTable.Rows)
            {
                IEnumerable<string> fields
                    = row.ItemArray.Select(
                    field => field.ToString());
                sb.AppendLine(string.Join("^", fields));
            }

            File.WriteAllText(strFilePath, sb.ToString());
        }

        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static void GetProductList(DataTable table)
        {
            var productList = new
                        List<Product>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                var product = new Product()
                {
                    ProductID = Convert.ToInt32(row[0]),
                    ProductName = row[1].ToString(),
                    SupplierID = Convert.ToInt32(row[2]),
                    CategoryID = Convert.ToInt32(row[3]),
                    QuantityPerUnit = row[4].ToString(),
                    UnitPrice = Convert.ToDecimal(row[5]),
                    UnitsInStock = Convert.ToInt32(row[6]),
                    UnitsOnOrder = Convert.ToInt32(row[7]),
                    ReorderLevel = Convert.ToInt16(row[8]),
                    Discontinued = Convert.ToBoolean(row[9]),
                };
                productList.Add(product);
            }            
        }

        public static void GetProductJson(DataTable table)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(table);
            Console.Write(JSONresult);
        }
    }
}

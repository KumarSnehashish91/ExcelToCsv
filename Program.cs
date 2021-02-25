using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            const string productsFilePath = "e:\\products.csv";
            const string productsFilePath1 = "e:\\products2.csv";

            DataTable dtProducts = ImportData.GetProducts();

            //ExportData.ToCSV(dtProducts, productsFilePath);

            ExportData.ToCSVWithFile(dtProducts, productsFilePath1);

            //ExportData.GetProductList(dtProducts);

            //ExportData.GetProductJson(dtProducts);
        }
    }
}

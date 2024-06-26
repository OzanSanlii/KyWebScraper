//using ClosedXML.Excel;
//using CsvHelper;
//using ExcelDataReader;
//using System.Formats.Asn1;
//using System.Globalization;
//using static WebScraper.Program;

//namespace WebScrapper
//{
//    public static class ExcelHandler
//    {

//        public static void ExcelCsvWrite(List<ProductInformation> productDetailsResult)
//        {
//            using (var writer = new StreamWriter("./Product-Detail.csv", append: true))
//            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
//            {
//                csv.WriteRecords(productDetailsResult);
//            }
//        }

//        public static void ReadCsvtoWriteXlsx(string csvFilePath, string xlsxFilePath)
//        {
//            using (var workbook = new XLWorkbook())
//            {
//                var worksheet = workbook.Worksheets.Add("Sheet1");

//                // CSV dosyasından verileri oku ve Excel sayfasına aktar
//                using (var reader = new StreamReader(csvFilePath))
//                {
//                    int row = 1;
//                    while (!reader.EndOfStream)
//                    {
//                        var line = reader.ReadLine();
//                        var values = line.Split(',');
//                        for (int i = 0; i < values.Length; i++)
//                        {
//                            worksheet.Cell(row, i + 1).Value = values[i];
//                        }
//                        row++;
//                    }
//                }

//                workbook.SaveAs(xlsxFilePath);
//            }
//        }
//    }
//}

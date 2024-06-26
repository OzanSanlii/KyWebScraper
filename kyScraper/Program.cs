using ClosedXML.Excel;
using HtmlAgilityPack;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using WebScrapper;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Drawing.Charts;
using KitapYurduScrapper;




namespace WebScraper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Başlangıç ID Giriniz");
            var urlID = Console.ReadLine();
            Console.WriteLine("Sorgulama Adet Sayısı");
            var urlCount = Console.ReadLine();

            List<ProductInformation> ProductInformationResult = new List<ProductInformation>();
            List<ProductSalesByDate> ProductSalesByDatesResult = new List<ProductSalesByDate>();
            GetDataFromWeb(ProductInformationResult, ProductSalesByDatesResult, Convert.ToInt32(urlID), Convert.ToInt32(urlCount));

            try
            {
                using (var dbContext = new DataMiningContext())
                {
                    foreach (var newProduct in ProductInformationResult)
                    {

                        var existingProduct = dbContext.ProductInformations.FirstOrDefault(p => p.StockCode == newProduct.StockCode);

                        if (existingProduct != null)
                        {
                            existingProduct.KitapyurduId = newProduct.KitapyurduId;
                            existingProduct.Name = newProduct.Name;
                            existingProduct.Author = newProduct.Author;
                            existingProduct.PublishDate = newProduct.PublishDate;
                            existingProduct.Brand = newProduct.Brand;
                            existingProduct.PlatformPrice = newProduct.PlatformPrice;
                            existingProduct.SalesPrice = newProduct.SalesPrice;
                            existingProduct.Illustrator = newProduct.Illustrator;
                            existingProduct.Translator = newProduct.Translator;
                            existingProduct.Language = newProduct.Language;
                            existingProduct.PageCount = newProduct.PageCount;
                            existingProduct.CoverType = newProduct.CoverType;
                            existingProduct.PaperType = newProduct.PaperType;
                            existingProduct.Dimensions = newProduct.Dimensions;
                            existingProduct.UpdateDate = newProduct.UpdateDate;


                            dbContext.ProductInformations.Update(existingProduct);
                        }
                        else
                        {
                            dbContext.ProductInformations.Add(newProduct);
                        }

                        
                    }
                    dbContext.ProductSalesByDates.AddRange(ProductSalesByDatesResult);

                    dbContext.SaveChanges();

                    Console.WriteLine("Veriler güncellendi/eklendi");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception" + ex.InnerException.Message);
                }
            }
        }


        public static void GetDataFromWeb(List<ProductInformation> resultProducts, List<ProductSalesByDate> resultSales, int urlID, int urlCount)
        {
            ChromeOptions options = new ChromeOptions();
            UserReagent(options);

            using (var driver = new ChromeDriver(options))
            {
                try
                {

                    for (var url = urlID; url <= (urlID + urlCount); url++)
                    {

                        driver.Navigate().GoToUrl($"https://www.kitapyurdu.com/kitap/a/{url}.html");
                        Thread.Sleep(500);


                        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(35);

                        string pageSourceK = driver.PageSource;
                        File.WriteAllText("pageSource.txt", pageSourceK);

                        var newHtmlDocument = new HtmlDocument();
                        newHtmlDocument.LoadHtml(pageSourceK);

                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                        int KitapyurduId = url;
                        
                        string Name = ElementGet.GetName(newHtmlDocument);

                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            continue;
                        }

                        string StockCodes = ElementGet.GetIsbn(newHtmlDocument);
                        if (string.IsNullOrWhiteSpace(StockCodes))
                        {
                            continue;
                        }

                        string Author = ElementGet.GetAuthor(newHtmlDocument);

                        string Brand = ElementGet.GetBrand(newHtmlDocument);

                        DateOnly PublishDate = ElementGet.GetDate(newHtmlDocument);

                        decimal PlatformPrice = ElementGet.GetDistPrice(newHtmlDocument);

                        decimal SalesPrice = ElementGet.GetSalesPrice(newHtmlDocument);
                        
                        string Language = ElementGet.GetLanguage(newHtmlDocument);

                        string Translator = ElementGet.GetTranslator(newHtmlDocument);

                        string Illustrator = ElementGet.GetIllustrator(newHtmlDocument);

                        string OriginalName = ElementGet.GetOriginalName(newHtmlDocument);

                        int PageCount = ElementGet.GetPageCount(newHtmlDocument);

                        string CoverType = ElementGet.GetCoverType(newHtmlDocument);

                        DateTime CreateDate = ElementGet.GetCreateDate(newHtmlDocument);

                        DateTime UpdateDate = ElementGet.GetUpdateDate(newHtmlDocument);

                        string PaperType = ElementGet.GetPaperType(newHtmlDocument);

                        string Dimensions = ElementGet.GetDimensions(newHtmlDocument);

                        int Amount = ElementGet.GetAmount(newHtmlDocument);

                        DateOnly EntryDate = ElementGet.GetEntryDate(newHtmlDocument);   

                        var ProductSales = new ProductSalesByDate()
                        {
                            StockCode = StockCodes,
                            Amount = Amount,
                            EntryDate = EntryDate,
                        };

                        var ProductDetail = new ProductInformation()
                        {
                            PlatformId = 1,
                            KitapyurduId = KitapyurduId,
                            Name = Name,
                            Author = Author,
                            PublishDate = PublishDate,
                            Brand = Brand,
                            CreateDate = CreateDate,
                            UpdateDate = UpdateDate,
                            StockCode = StockCodes,
                            PlatformPrice = PlatformPrice,
                            SalesPrice = SalesPrice,
                            Illustrator = Illustrator,
                            OriginalName = OriginalName,
                            Translator = Translator,
                            Language = Language,
                            PageCount = PageCount,
                            CoverType = CoverType,
                            PaperType = PaperType,
                            Dimensions = Dimensions

                        };

                        List<ProductInformation> ProductInformationList = new List<ProductInformation>() { ProductDetail };
                        List<ProductSalesByDate> ProductSalesByDates = new List<ProductSalesByDate>() { ProductSales };
                        resultProducts.Add(ProductDetail);
                        resultSales.Add(ProductSales);

                        Thread.Sleep(2000);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error: " + ex.Message);

                }
                finally
                {
                    driver.Quit();

                }
            }
        }

        
        

        private static void UserReagent(ChromeOptions options)
        {
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.3029.110 Safari/537.36");
        }
    }
}
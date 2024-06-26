using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static WebScraper.Program;
using System.Xml;
using System.Text.Json.Nodes;
using System.Globalization;

namespace WebScrapper
{
    internal class ElementGet
    {
        

        public static string GetName(HtmlDocument newHtmlDocument)
        {
            string BookName = "";
            var bookNamer = newHtmlDocument.DocumentNode.SelectSingleNode("//h1[@class='pr_header__heading']");
            if(bookNamer != null)
            {
                BookName = bookNamer.InnerText.Trim();
            }
            else
            {

            }
            
            return BookName;


        }

        public static string GetAuthor(HtmlDocument newHtmlDocument)
        {
            string Author = "";
            var authorNamer = newHtmlDocument.DocumentNode.SelectSingleNode("//div[@class='pr_producers__manufacturer']");
            if (authorNamer != null)
            {
                var Authors = authorNamer.SelectSingleNode(".//a[@class='pr_producers__link']"); 
                if (Authors != null)
                {
                    Author = Authors.InnerText.Trim();
                }
            }

            return Author;
        }

        public static DateOnly GetDate(HtmlDocument newHtmlDocument)
        {
            DateOnly publishDate = default;

            var dateNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Yayın Tarihi:']/following-sibling::td");

            if (dateNode != null)
            {
                string dateString = dateNode.InnerText.Trim();
                if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    publishDate = new DateOnly(parsedDate.Year, parsedDate.Month, parsedDate.Day);
                }
            }

            return publishDate;
        }

        public static string GetBrand(HtmlDocument newHtmlDocument)
        {
            string BrandName = "";
            var brandNamer = newHtmlDocument.DocumentNode.SelectSingleNode("//div[@class='pr_producers__publisher']");
            if (brandNamer != null)
            {
                var Brands = brandNamer.SelectSingleNode(".//a[@class='pr_producers__link']"); 
                if (Brands != null)
                {
                    BrandName = Brands.InnerText.Trim();
                }
            }

            return BrandName;
        }


        public static decimal GetDistPrice(HtmlDocument newHtmlDocument)
        {
            string DistributorPrice = "0";
            var DistributorPriceNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Liste Fiyatı:']/following-sibling::td");

            if (DistributorPriceNode != null)
            {
                DistributorPrice = DistributorPriceNode.InnerText.Trim();
                var turkishCulture = new CultureInfo("tr-TR");

                if (decimal.TryParse(DistributorPrice, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, turkishCulture, out decimal distSalesPrice))
                {
                    Console.WriteLine(distSalesPrice);
                    return distSalesPrice;
                }
            }
            
            return 0; 
        }



        public static decimal GetSalesPrice(HtmlDocument newHtmlDocument)
        {
            string ShopPrice = "0";
            var ShopPricer = newHtmlDocument.DocumentNode.SelectSingleNode("//div[@class='price__item']/text()[normalize-space()]");
            var ShopPriced = newHtmlDocument.DocumentNode.SelectSingleNode("//div[@class='price__item']/small/text()[normalize-space()]");

            Console.WriteLine("ShopPricer: " + (ShopPricer != null ? ShopPricer.InnerText.Trim() : "null"));
            Console.WriteLine("ShopPriced: " + (ShopPriced != null ? ShopPriced.InnerText.Trim() : "null"));

            if (ShopPricer != null && ShopPriced != null)
            {
                string mainPrice = ShopPricer.InnerText.Trim();
                string decimalPart = ShopPriced.InnerText.Trim();

                var turkishCulture = new CultureInfo("tr-TR");
                string priceString = $"{mainPrice}{decimalPart}";
    
                if (decimal.TryParse(priceString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, turkishCulture, out decimal salesPrice))
                {
                    Console.WriteLine(salesPrice);
                    return salesPrice;
                }
            }

            return 0;
        }

        public static string GetIsbn(HtmlDocument newHtmlDocument)
        {
            string StockNumbers = "";
            var isbnNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='ISBN:']/following-sibling::td");

            if (isbnNode != null)
            {
                StockNumbers = isbnNode.InnerText.Trim();
            }
            else
            {
                var isbnNoder = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='ISSN:']/following-sibling::td");
                if (isbnNoder != null)
                {
                    StockNumbers = isbnNoder.InnerText.Trim();
                }
                
            }

           
            return StockNumbers;
        }

        public static string GetTranslator(HtmlDocument newHtmlDocument)
        {
            string Translator = "";
            var translatorNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Çevirmen:']/following-sibling::td");
            if (translatorNode != null)
            {
                Translator = translatorNode.InnerText.Trim();
            }
            else
            {
                Translator = "";
            }

            
            return Translator;

        }

        public static string GetIllustrator(HtmlDocument newHtmlDocument)
        {
            string Illustrator = "";
            var illustratorNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Çizer:']/following-sibling::td");
            if (illustratorNode != null)
            {
                Illustrator = illustratorNode.InnerText.Trim();
            }
            else
            {
                Illustrator = "";
            }


            return Illustrator;

        }

        public static string GetOriginalName(HtmlDocument newHtmlDocument)
        {
            string OriginalName = "";
            var orginNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Orjinal Adı:']/following-sibling::td");
            if (orginNode != null)
            {
                OriginalName = orginNode.InnerText.Trim();
            }
            else
            {
                OriginalName = "";
            }


            return OriginalName;

        }

        public static DateTime GetCreateDate(HtmlDocument newHtmlDocument)
        {
            DateTime now = DateTime.Now;

            return now;
        }

        public static DateOnly GetEntryDate(HtmlDocument newHtmlDocument)
        {
            DateTime now = DateTime.Now;

            return new DateOnly(now.Year, now.Month, now.Day);
        }


        public static DateTime GetUpdateDate(HtmlDocument newHtmlDocument)
        {
            DateTime updateTime = DateTime.Now;

            return updateTime;
        }

        public static string GetLanguage(HtmlDocument newHtmlDocument)
        {
            string Languages = "";
            var languageNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Dil:']/following-sibling::td");
            if (languageNode != null)
            {
                Languages = languageNode.InnerText.Trim();
            }

            return Languages;

        }

        public static int GetAmount(HtmlDocument newHtmlDocument)
        {
            HtmlNode amountNode = newHtmlDocument.DocumentNode.SelectSingleNode("//p[@class='purchased']");

            if (amountNode != null)
            {
                string amountText = amountNode.InnerText;
                int startIndex = amountText.IndexOf("üründen") + "üründen".Length;
                int endIndex = amountText.IndexOf("adet");

                if (startIndex >= 0 && endIndex >= 0)
                {
                    string amountString = amountText.Substring(startIndex, endIndex - startIndex).Trim();

                    if (int.TryParse(amountString, out int amount))
                    {
                        return amount;
                    }
                }
            }

            return 0;
        }




        public static int GetPageCount(HtmlDocument newHtmlDocument)
        {
            string PageCount = "0";
            var pageCountNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Sayfa Sayısı:']/following-sibling::td");

            if (pageCountNode != null)
            {
                PageCount = pageCountNode.InnerText.Trim();
            }

            
            if (int.TryParse(PageCount, out int pageCount))
            {
                return pageCount;
            }

            return 0; 
        }


        public static string GetCoverType(HtmlDocument newHtmlDocument)
        {
            string Types = "";
            var typesNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Cilt Tipi:']/following-sibling::td");
            if (typesNode != null)
            {
                Types = typesNode.InnerText.Trim();
            }

            
            return Types;

        }

        public static string GetPaperType(HtmlDocument newHtmlDocument)
        {
            string PaperType = "";
            var paperTypeNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Kağıt Cinsi:']/following-sibling::td");
            if (paperTypeNode != null)
            {
                PaperType = paperTypeNode.InnerText.Trim();
            }

            
            return PaperType;

        }

        public static string GetDimensions(HtmlDocument newHtmlDocument)
        {
            string Sizes = "";
            var sizesNode = newHtmlDocument.DocumentNode.SelectSingleNode("//td[text()='Boyut:']/following-sibling::td");
            if (sizesNode != null)
            {
                Sizes =sizesNode.InnerText.Trim();
            }

            
            return Sizes;

        }



    }
}





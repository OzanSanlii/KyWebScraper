using System;
using System.Collections.Generic;

namespace KitapYurduScrapper;

public partial class ProductSalesByDate
{
    public int Id { get; set; }

    public string StockCode { get; set; } = null!;

    public int Amount { get; set; }

    public DateOnly EntryDate { get; set; }
}

using System;
using System.Collections.Generic;

namespace KitapYurduScrapper;

public partial class ProductInformation
{
    public int Id { get; set; }

    public string StockCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal SalesPrice { get; set; }

    public decimal PlatformPrice { get; set; }

    public int PlatformId { get; set; }

    public string? Author { get; set; }

    public string Brand { get; set; } = null!;

    public int? PageCount { get; set; }

    public string? Dimensions { get; set; }

    public string? Language { get; set; }

    public string? CoverType { get; set; }

    public string? PaperType { get; set; }

    public string? Translator { get; set; }

    public string? Illustrator { get; set; }

    public DateOnly? PublishDate { get; set; }

    public string? OriginalName { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public int KitapyurduId { get; set; }

    public virtual Platform Platform { get; set; } = null!;
}

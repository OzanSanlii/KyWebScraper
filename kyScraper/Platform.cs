using System;
using System.Collections.Generic;

namespace KitapYurduScrapper;

public partial class Platform
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductInformation> ProductInformations { get; set; } = new List<ProductInformation>();
}

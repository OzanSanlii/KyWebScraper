using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KitapYurduScrapper;

public partial class DataMiningContext : DbContext
{
    public DataMiningContext()
    {
    }

    public DataMiningContext(DbContextOptions<DataMiningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OdakBooks3> OdakBooks3s { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<PmPrefixProduct3> PmPrefixProduct3s { get; set; }

    public virtual DbSet<ProductInformation> ProductInformations { get; set; }

    public virtual DbSet<ProductSalesByDate> ProductSalesByDates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=arkmes,1433;Initial Catalog=DataMining;Persist Security Info=False;User ID=WebAdmin;Password=!ConAdmin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OdakBooks3>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OdakBooks");

            entity.ToTable("OdakBooks3");

            entity.HasIndex(e => e.P7, "DataMining_OdakBooks3Index");

            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.P1).HasColumnName("p1");
            entity.Property(e => e.P10).HasColumnName("p10");
            entity.Property(e => e.P11).HasColumnName("p11");
            entity.Property(e => e.P12).HasColumnName("p12");
            entity.Property(e => e.P13).HasColumnName("p13");
            entity.Property(e => e.P14).HasColumnName("p14");
            entity.Property(e => e.P15).HasColumnName("p15");
            entity.Property(e => e.P16).HasColumnName("p16");
            entity.Property(e => e.P17).HasColumnName("p17");
            entity.Property(e => e.P18).HasColumnName("p18");
            entity.Property(e => e.P19).HasColumnName("p19");
            entity.Property(e => e.P2).HasColumnName("p2");
            entity.Property(e => e.P20).HasColumnName("p20");
            entity.Property(e => e.P21).HasColumnName("p21");
            entity.Property(e => e.P22).HasColumnName("p22");
            entity.Property(e => e.P23).HasColumnName("p23");
            entity.Property(e => e.P26id).HasMaxLength(60);
            entity.Property(e => e.P27).HasColumnName("p27");
            entity.Property(e => e.P3).HasColumnName("p3");
            entity.Property(e => e.P4).HasColumnName("p4");
            entity.Property(e => e.P5).HasColumnName("p5");
            entity.Property(e => e.P6).HasColumnName("p6");
            entity.Property(e => e.P7)
                .HasMaxLength(30)
                .HasColumnName("p7");
            entity.Property(e => e.P8).HasColumnName("p8");
            entity.Property(e => e.P9).HasColumnName("p9");
            entity.Property(e => e.Pb1).HasColumnName("pb1");
            entity.Property(e => e.Pb10).HasColumnName("pb10");
            entity.Property(e => e.Pb11).HasColumnName("pb11");
            entity.Property(e => e.Pb12).HasColumnName("pb12");
            entity.Property(e => e.Pb2).HasColumnName("pb2");
            entity.Property(e => e.Pb3).HasColumnName("pb3");
            entity.Property(e => e.Pb4).HasColumnName("pb4");
            entity.Property(e => e.Pb5).HasColumnName("pb5");
            entity.Property(e => e.Pb6).HasColumnName("pb6");
            entity.Property(e => e.Pb7).HasColumnName("pb7");
            entity.Property(e => e.Pb8).HasColumnName("pb8");
            entity.Property(e => e.Pb9).HasColumnName("pb9");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Platform_pk");

            entity.ToTable("Platform");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PmPrefixProduct3>(entity =>
        {
            entity.ToTable("PM_PrefixProduct3");

            entity.HasIndex(e => e.Isbn, "DataMining_PmPrefixISBNIndex");

            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.Isbn)
                .HasMaxLength(30)
                .HasColumnName("ISBN");
            entity.Property(e => e.ListPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrefixId).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductInformation_pk");

            entity.ToTable("ProductInformation");

            entity.HasIndex(e => e.StockCode, "ProductInformation_StockCode_index");

            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.CoverType).HasMaxLength(75);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Dimensions).HasMaxLength(50);
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.PaperType).HasMaxLength(75);
            entity.Property(e => e.PlatformPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalesPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StockCode).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Platform).WithMany(p => p.ProductInformations)
                .HasForeignKey(d => d.PlatformId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductInformation_Platform_Id_fk");
        });

        modelBuilder.Entity<ProductSalesByDate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductSalesByDate_pk");

            entity.ToTable("ProductSalesByDate");

            entity.HasIndex(e => e.StockCode, "ProductSalesByDate_StockCode_index");

            entity.Property(e => e.StockCode).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

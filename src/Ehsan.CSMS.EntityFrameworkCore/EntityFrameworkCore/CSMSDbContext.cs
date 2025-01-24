using Ehsan.CSMS.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
// remove migiration to re buld category
namespace Ehsan.CSMS.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class CSMSDbContext :
    AbpDbContext<CSMSDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    /* 
       <td>@Html.Encode(order.Id)</td>
                <td>@Html.Encode(order.Customer.CustomerName)</td>
                <td>@Html.Encode(order.Cashier.CashierName)</td>
                <td>@Html.Encode(order.Customer.LoyaltyPoints)</td>
                <td>@Html.Encode(order.CreationTime)</td>
                <td>@Html.Encode(order.TotalPrice)</td>
                <td>@Html.Encode(order.NetPrice)</td>
        
     */
    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Customer> Costumers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<Product> Products { get; set; }




    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public CSMSDbContext(DbContextOptions<CSMSDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configurations */
        // Category Done
        builder.Entity<Category>(b =>
        {
            b.ToTable("Category");
            b.HasKey(c => c.Id); //auto configure for the base class props
            b.Property(c => c.CategoryName).HasColumnType("Varchar(50)");
            b.HasMany(c => c.Products)
             .WithOne(p => p.Category)
             .HasForeignKey(p => p.CategoryId); // Means M(Product) will have CategoryId as a forign key
        });
        // Cashier Done
        builder.Entity<Cashier>(b =>
        {
            b.ToTable("Cashier");
            b.HasKey(c => c.Id); //auto configure for the base class props
            b.Property(c => c.CashierName).HasColumnType("varchar(50)");
            b.HasMany(c => c.Orders)
             .WithOne(o => o.Cashier)
             .HasForeignKey(o => o.CashierId);  // CashierId is a forign key to order table 
        });
        // Customer Done
        builder.Entity<Customer>(b =>
        {
            b.ToTable("Customer");
            b.HasKey(c => c.Id);
            b.Property(c => c.CustomerName).HasColumnType("varchar(50)");
            b.Property(c => c.ContactInfo).HasColumnType("varchar(50)");
            b.Property(c => c.LoyaltyPoints).HasColumnType("int");
            b.HasMany(c => c.Orders)
             .WithOne(o => o.Customer)
             .HasForeignKey(o => o.CustomerId); // means costumerid is FK to order 
        });
        // Order Done
        builder.Entity<Order>(b =>
        {
            b.ToTable("Order");
            b.HasKey(o => o.Id);
            b.Property(o => o.CreationTime)
             .IsRequired()
             .HasColumnType("DateTime")
             .HasDefaultValueSql("GetDate()")
             .HasColumnName("Order_Date");
            b.Property(o => o.OrderStatus).HasColumnType("int");
            b.HasMany(o => o.OrderDetails)
             .WithOne(od => od.Order)
             .HasForeignKey(od => od.OrderId);
            b.Property(c => c.TotalPrice)
            .HasColumnType("decimal(9,3)");
        });
        // derDetail done
        builder.Entity<OrderDetail>(b =>
        {
            b.ToTable("OrderDetail");
            b.HasKey(od => od.Id); //auto configure for the base class props
            b.Property(od => od.PricePerUnit).HasColumnType("decimal(9,3)");
            b.Property(od => od.TotalPrice).HasColumnType("decimal(9,3)");
            b.Property(od => od.Quantity).HasColumnType("int");
            b.HasOne(od => od.Product)
             .WithMany(p => p.OrderDetails)
             .HasForeignKey(od => od.ProductId);

        });
        // Product Done 
        builder.Entity<Product>(b =>
        {
            b.ToTable("Product");
            b.HasKey(p => p.Id); //auto configure for the base class props
            b.Property(p => p.ProductName).HasColumnType("varchar(100)");
            b.Property(p => p.ProductPrice).HasColumnType("decimal(9,3)");
            b.HasMany(p => p.OrderDetails)
             .WithOne(od => od.Product)
             .HasForeignKey(od => od.ProductId);
        });
    }


    /* Configure your own tables/entities inside here */

    //builder.Entity<YourEntity>(b =>
    //{
    //    b.ToTable(CSMSConsts.DbTablePrefix + "YourEntities", CSMSConsts.DbSchema);
    //    b.ConfigureByConvention(); //auto configure for the base class props
    //    //...
    //});
}


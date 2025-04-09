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
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Invoice> Invoices { get; set; }


    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public CSMSDbContext(DbContextOptions<CSMSDbContext> options)
        : base(options)
    {

    }
    /// <summary>
    /// Get-Migrations -Context CSMSDbContext
    /// </summary>
    /// <param name="builder"></param>
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
            b.HasKey(c => c.Id);
            b.Property(o => o.CreationTime)
             .HasDefaultValueSql("GetDate()")
             .HasColumnName("Category_Date");
            b.Property(c => c.Name)
             .HasColumnType("nvarchar(40)")
             .IsRequired();
            b.HasMany(c => c.Products)
             .WithOne(p => p.Category)
             .HasForeignKey(p => p.CategoryId);
            // ON DELETE NO ACTION
        });
        // Cashier Done
        builder.Entity<Cashier>(b =>
        {
            b.ToTable("Cashier");
            b.HasKey(c => c.Id);
            b.Property(o => o.CreationTime)
             .HasDefaultValueSql("GetDate()")
             .HasColumnName("Cashier_Date");
            b.Property(c => c.Name)
             .HasColumnType("nvarchar(40)")
             .IsRequired();
            b.HasMany(c => c.Orders)
             .WithOne(o => o.Cashier)
             .HasForeignKey(o => o.CashierId);
            // ON DELETE NO ACTION
        });
        // Customer Done
        builder.Entity<Customer>(b =>
        {
            b.ToTable("Customer");
            b.HasKey(c => c.Id);
            b.Property(o => o.CreationTime)
             .HasDefaultValueSql("GetDate()")
             .HasColumnName("Customer_Date");
            b.Property(c => c.Name)
             .HasColumnType("nvarchar(40)")
             .IsRequired();
            b.HasIndex(c => c.MobileNumber)
             .IsUnique();
            b.Property(c => c.MobileNumber)
             .HasColumnType("nvarchar(10)")
             .IsRequired();
            b.HasMany(c => c.Orders)
             .WithOne(o => o.Customer)
             .HasForeignKey(o => o.CustomerId);
            // ON DELETE NO ACTION
        });
        // Order Done
        builder.Entity<Order>(b =>
        {
            b.ToTable("Order");
            b.HasKey(o => o.Id);
            b.Property(o => o.CreationTime)
             .HasDefaultValueSql("GetDate()")
             .HasColumnName("Order_Date");
            b.Property(o => o.OrderStatus)
             .HasColumnType("int");
            b.HasMany(o => o.OrderDetails)
             .WithOne(od => od.Order)
             .HasForeignKey(od => od.OrderId);
            // ON DELETE NO ACTION
        });
        // derDetail done
        builder.Entity<OrderDetail>(b =>
        {
            b.ToTable("OrderDetail")
             .HasKey(od => new { od.OrderId, od.ProductID });
            b.Property(o => o.Quantity)
             .IsRequired();
        });
        // Product Done 
        builder.Entity<Product>(b =>
        {
            b.ToTable("Product");
            b.Property(o => o.CreationTime)
             .HasDefaultValueSql("GetDate()")
             .HasColumnName("Product_Date");
            b.HasKey(p => p.Id);
            b.Property(p => p.Name)
             .HasColumnType("nvarchar(40)")
             .IsRequired();
            b.Property(p => p.Price)
             .IsRequired();
            b.HasMany(p => p.OrderDetails)
             .WithOne(od => od.Product)
             .HasForeignKey(od => od.ProductID);
            // ON DELETE NO ACTION
        });
        // Invoice s
        builder.Entity<Invoice>(i =>
        {
            i.ToTable("Invoice");
            i.Property(o => o.CreationTime)
             .HasDefaultValueSql("GetDate()")
             .HasColumnName("Invoice_Date");
            i.HasKey(p => p.Id);
            i.HasOne(i => i.Order)
             .WithOne(o => o.Invoice)
             .HasForeignKey<Invoice>(i => i.OrderId);
            // ON DELETE NO ACTION
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


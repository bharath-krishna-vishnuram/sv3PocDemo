using Microsoft.EntityFrameworkCore;
using PDM.Models;
using System.Reflection.Emit;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Trial.MvcDemoApplication.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MvcDemoApplicationDbContext :
    AbpDbContext<MvcDemoApplicationDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<TextElement> TextElements { get; set; }
    public DbSet<Structure> Structures { get; set; }
    public DbSet<StructureElement> StructureElements { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentDescriptor> ComponentDescriptor { get; set; }
    public DbSet<DescriptorOption> Options { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public MvcDemoApplicationDbContext(DbContextOptions<MvcDemoApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder); 
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Structure>(
            e =>
            {
                e.HasOne(structure => structure.Name)
                .WithMany(text => text.Structures)
                .OnDelete(DeleteBehavior.NoAction);
                e.Navigation(structure => structure.Name).AutoInclude();
            });

        builder.Entity<StructureElement>()
            .HasOne(element => element.AssociatedStructure)
                .WithMany(structure => structure.StructureElements)
                .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Component>(
            e =>
            {
                e.HasOne(Component => Component.Name)
                .WithMany(text => text.Components)
                .OnDelete(DeleteBehavior.NoAction);
                e.Navigation(Component => Component.Name).AutoInclude();
            });

        builder.Entity<ComponentDescriptor>(
            e =>
            {
                e.HasOne(descriptor => descriptor.Name)
                .WithMany(text => text.ComponentDescriptors)
                .OnDelete(DeleteBehavior.NoAction);
                e.Navigation(descriptor => descriptor.Name).AutoInclude();
            });

        builder.Entity<DescriptorOption>(
            e =>
            {
                e.HasOne(option => option.Name)
                .WithMany(text => text.DescriptorOptions)
                .OnDelete(DeleteBehavior.NoAction);
                e.Navigation(option => option.Name).AutoInclude();
            });

        builder.Entity<TextElement>(
            e =>
            {
                e.HasIndex(option => new { option.IsDeleted, option.UniqueTextId })
                .IsUnique(true)
                .HasFilter("IsDeleted = 0");
            });


        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(MvcDemoApplicationConsts.DbTablePrefix + "YourEntities", MvcDemoApplicationConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}

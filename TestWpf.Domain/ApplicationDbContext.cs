using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestWpf.Domain.Entities;

namespace TestWpf.Domain;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Inventory> Inventories { get; set; }

    public DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Администратор
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "6EDB710E-3702-4F78-9E31-D572B0BC0429",
            Name = "admin",
            NormalizedName = "ADMIN"
        });

        builder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = "8597180C-3732-4482-A1EE-7C11D4DD4162",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "supercompany@mail.ru",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword")
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            UserId = "8597180C-3732-4482-A1EE-7C11D4DD4162",
            RoleId = "6EDB710E-3702-4F78-9E31-D572B0BC0429"
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "2E5D1273-F618-4DBB-9BE7-D8E983771F88",
            Name = "user",
            NormalizedName = "USER"
        });

        //Пользователь
        builder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = "AFD26019-2FAA-434A-B0B3-1526940F87DB",
            UserName = "user",
            NormalizedUserName = "USER",
            Email = "supercompany@mail.ru",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "user")
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            UserId = "AFD26019-2FAA-434A-B0B3-1526940F87DB",
            RoleId = "2E5D1273-F618-4DBB-9BE7-D8E983771F88"
        });

        //Ассет и инвентарь
        builder.Entity<Asset>().HasData(new Asset
        {
            AssetId = new Guid("9723FC62-E0CC-485F-BF6B-3097A8CA4138"),
            Name = "Крутой ассет 1",
            Description = "Описание крутого ассета",
            InvetoryId = new Guid("698E6F21-B313-43E2-86D5-1CB1427B3D44")
        });

        builder.Entity<Inventory>().HasData(new Inventory
        {
            InventoryId = new Guid("698E6F21-B313-43E2-86D5-1CB1427B3D44"),
            Name = "Крутой инвентарь 1",
            Description = "Описание крутого инвентаря",
            Count = 1,
            AssetId = new Guid("9723FC62-E0CC-485F-BF6B-3097A8CA4138")
        });
    }
}

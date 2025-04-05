using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ekart.Infrastructure.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = new Guid("b5a4221d-d65b-450e-80f0-4d04436f05cd").ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = new Guid("b5a4221d-d65b-450e-80f0-4d03036f055d").ToString(), Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Product.Service.Model.User
{
    public class UserEntityTypeConfig : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            // Configure table schema
            builder.ToTable("User", schema: "ecom");
            builder.HasKey(u => u.UserId).HasName("PK_User_UserId");  
            builder.Ignore(u => u.Password);

            builder.Property(u => u.Name).HasColumnType("nvarchar(128)").IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.PhoneNumber).HasColumnType("nvarchar(14)").IsRequired();
            builder.Property(u => u.Hash).IsRequired();
            builder.Property(u => u.Salt).IsRequired();
            builder.Property(u => u.CreatedBy).HasColumnType("nvarchar(128)").IsRequired();
            builder.Property(u => u.CreatedDate).HasColumnType("datetime").IsRequired();
            builder.Property(u => u.ModifiedBy).HasColumnType("nvarchar(128)");
            builder.Property(u => u.ModifiedDate).HasColumnType("datetime");
            builder.Property(u => u.IsActive).HasDefaultValue(true).IsRequired();
        }
    }
}

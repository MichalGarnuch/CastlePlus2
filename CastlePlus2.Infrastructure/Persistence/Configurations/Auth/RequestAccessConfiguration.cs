using CastlePlus2.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CastlePlus2.Infrastructure.Persistence.Configurations.Auth
{
    public sealed class RequestAccessConfiguration : IEntityTypeConfiguration<RequestAccess>
    {
        public void Configure(EntityTypeBuilder<RequestAccess> builder)
        {
            builder.ToTable("RequestAccess", "auth");

            builder.HasKey(x => x.IdRequestAccess);

            builder.Property(x => x.IdRequestAccess)
                   .HasColumnName("IdRequestAccess")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.FullName)
                   .HasColumnName("FullName")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasColumnName("Email")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Login)
                   .HasColumnName("Login")
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(x => x.Phone)
                   .HasColumnName("Phone")
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(x => x.Department)
                   .HasColumnName("Department")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Justification)
                   .HasColumnName("Justification")
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasColumnName("Status")
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.RequestedBy)
                   .HasColumnName("RequestedBy")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                   .HasColumnName("CreatedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.UpdatedAtUtc)
                   .HasColumnName("UpdatedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired();

            builder.Property(x => x.ApprovedBy)
                   .HasColumnName("ApprovedBy")
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(x => x.ApprovedAtUtc)
                   .HasColumnName("ApprovedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.Property(x => x.ApprovedLogin)
                   .HasColumnName("ApprovedLogin")
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(x => x.ApprovedEmail)
                   .HasColumnName("ApprovedEmail")
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(x => x.ApprovedRoleCodes)
                   .HasColumnName("ApprovedRoleCodes")
                   .HasMaxLength(400)
                   .IsRequired(false);

            builder.Property(x => x.RejectedBy)
                   .HasColumnName("RejectedBy")
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(x => x.RejectedAtUtc)
                   .HasColumnName("RejectedAtUtc")
                   .HasColumnType("datetime2(0)")
                   .IsRequired(false);

            builder.Property(x => x.RejectionReason)
                   .HasColumnName("RejectionReason")
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.CreatedAtUtc);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SureBackup.Domain.Entities;

namespace SureBackup.Infrastructure.EntityConfigurations;

public class DatabaseInfoEntityConfiguration : IEntityTypeConfiguration<DatabaseInfo>
{
    public void Configure(EntityTypeBuilder<DatabaseInfo> builder)
    {
        builder.ToTable(nameof(DatabaseInfo));
        builder.HasKey(prop => prop.ID);
        builder.Property(prop=>prop.ID).HasColumnName(nameof(DatabaseInfo.ID)).IsRequired().ValueGeneratedOnAdd();
        builder.Property(prop => prop.Name).HasColumnName(nameof(DatabaseInfo.Name)).IsRequired().HasMaxLength(20);
        builder.Property(prop=>prop.IsActive).HasColumnName(nameof(DatabaseInfo.IsActive)).IsRequired().HasDefaultValue(true);
        builder.Property(prop => prop.EncryptedConnectionString).HasColumnName(nameof(DatabaseInfo.EncryptedConnectionString)).IsRequired();

    }
}

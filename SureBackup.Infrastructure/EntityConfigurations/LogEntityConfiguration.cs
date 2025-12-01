
using SureBackup.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SureBackup.Infrastructure.EntityConfigurations;

public class LogEntityConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable(nameof(Log));
        builder.HasKey(prop=>prop.ID);
        builder.Property(prop=>prop.ID).HasColumnName(nameof(Log.ID)).IsRequired().ValueGeneratedOnAdd();
        builder.Property(prop => prop.Type).HasColumnName(nameof(Log.Type)).IsRequired();
        builder.Property(prop=>prop.Message).HasColumnName(nameof(Log.Message)).IsRequired().HasMaxLength(500);
        builder.Property(prop => prop.DatabaseInfoID).HasColumnName(nameof(Log.DatabaseInfoID)).IsRequired();
        builder.Property(prop=>prop.Date).HasColumnName(nameof(Log.Date)).IsRequired();
        builder.Property(prop => prop.BatchNumber).HasColumnName(nameof(Log.BatchNumber)).HasDefaultValue(1).IsRequired();

        builder.HasOne(prop=>prop.DatabaseInfo).WithMany(prop=>prop.BackupLogs).HasForeignKey(prop=>prop.DatabaseInfoID).OnDelete(DeleteBehavior.Cascade);
    }
}

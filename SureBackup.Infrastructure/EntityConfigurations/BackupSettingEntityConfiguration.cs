
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SureBackup.Domain.Entities;

namespace SureBackup.Infrastructure.EntityConfigurations;

public class BackupSettingEntityConfiguration : IEntityTypeConfiguration<BackupSetting>
{
    public void Configure(EntityTypeBuilder<BackupSetting> builder)
    {
        builder.ToTable(nameof(BackupSetting));
        builder.HasKey(prop=>prop.ID);
        builder.Property(prop=>prop.ID).HasColumnName(nameof(BackupSetting.ID)).IsRequired().ValueGeneratedOnAdd();
        builder.Property(prop => prop.IntervalMiliseconds).HasColumnName(nameof(BackupSetting.IntervalMiliseconds)).IsRequired();
        builder.Property(prop => prop.HostSizeInBytes).HasColumnName(nameof(BackupSetting.HostSizeInBytes));
        builder.Property(prop => prop.FTPEncryptedUrl).HasColumnName(nameof(BackupSetting.FTPEncryptedUrl)).HasMaxLength(500);
        builder.Property(prop => prop.FTPEncryptedUsername).HasColumnName(nameof(BackupSetting.FTPEncryptedUsername)).HasMaxLength(500);
        builder.Property(prop => prop.FTPEncryptedPassword).HasColumnName(nameof(BackupSetting.FTPEncryptedPassword)).HasMaxLength(500);
        builder.Property(prop => prop.FTPUpload).HasColumnName(nameof(BackupSetting.FTPUpload)).IsRequired();
        builder.Property(prop => prop.EncryptedBackupKey).HasColumnName(nameof(BackupSetting.EncryptedBackupKey)).HasMaxLength(500);
        builder.Property(prop => prop.EncryptionBackup).HasColumnName(nameof(BackupSetting.EncryptionBackup)).IsRequired();


    }
}

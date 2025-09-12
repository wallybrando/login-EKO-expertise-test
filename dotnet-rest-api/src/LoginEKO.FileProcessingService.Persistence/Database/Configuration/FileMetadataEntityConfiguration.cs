using LoginEKO.FileProcessingService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace LoginEKO.FileProcessingService.Persistence.Database.Configuration
{
    public class FileMetadataEntityConfiguration : IEntityTypeConfiguration<FileMetadata>
    {
        public void Configure(EntityTypeBuilder<FileMetadata> builder)
        {
            builder.ToTable("file_metadata");
            builder.Ignore(p => p.File);

            // primary key set up
            builder.Property(t => t.Id)
                .HasColumnName("id")
                .HasValueGenerator((Id, type) => new SequentialGuidValueGenerator())
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            builder.Property(p => p.Filename)
                .HasColumnName("filename")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(p => p.Extension)
                .HasColumnName("extension")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(p => p.SizeInBytes)
                .HasColumnName("size_in_bytes")
                .HasColumnType("BIGINT")
                .IsRequired();

            builder.Property(p => p.BinaryObject)
                .HasColumnName("binary_object")
                .HasColumnType("BYTEA")
                .IsRequired();

            builder.Property(p => p.MD5Hash)
                .HasColumnName("md5_hash")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("TIMESTAMP")
                .IsRequired();
        }
    }
}

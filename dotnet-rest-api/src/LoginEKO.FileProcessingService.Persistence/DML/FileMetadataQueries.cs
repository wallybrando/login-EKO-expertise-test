using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.DML
{
    public static class FileMetadataQueries
    {
        public const string InsertFileMetadataSql = """
                INSERT INTO blob_metadata (id, filename, extension, size_in_bytes, binary_object, md5_hash, created_date)
                VALUES (@Id, @Filename, @Extension, @SizeInBytes, @BinaryObject, @MD5Hash, @CreatedDate);
                """;

        public const string GetFileMetadataByMd5HashSql = $"""
                    SELECT
                    id AS {nameof(FileMetadata.Id)},
                    filename AS {nameof(FileMetadata.Filename)},
                    extension AS {nameof(FileMetadata.Extension)},
                    size_in_bytes AS {nameof(FileMetadata.SizeInBytes)},
                    binary_object AS {nameof(FileMetadata.BinaryObject)},
                    md5_hash AS {nameof(FileMetadata.MD5Hash)},
                    created_date AS {nameof(FileMetadata.CreatedDate)}
                    FROM blob_metadata WHERE md5_hash = @md5Hash
                    """;
    }
}

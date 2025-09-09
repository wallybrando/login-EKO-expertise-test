using Dapper;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly List<FileDto> _files = [];
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public FileRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<FileDto?> GetByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var file = await connection.QuerySingleOrDefaultAsync<FileDto>(
                new CommandDefinition($"""
                    select
                    id AS {nameof(FileDto.Id)},
                    filename AS {nameof(FileDto.Filename)},
                    extension AS {nameof(FileDto.Extension)},
                    size_in_bytes AS {nameof(FileDto.SizeInBytes)},
                    binary_object AS {nameof(FileDto.BinaryObject)},
                    md5_hash AS {nameof(FileDto.MD5Hash)},
                    created_date AS {nameof(FileDto.CreatedDate)}
                    from blob_metadata where id = @id
                    """, new { id }));

            return file;
        }

        public async Task<FileDto?> GetByFilenameAsync(string filename)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var file = await connection.QuerySingleOrDefaultAsync<FileDto>(
                new CommandDefinition($"""
                    select
                    id AS {nameof(FileDto.Id)},
                    filename AS {nameof(FileDto.Filename)},
                    extension AS {nameof(FileDto.Extension)},
                    size_in_bytes AS {nameof(FileDto.SizeInBytes)},
                    binary_object AS {nameof(FileDto.BinaryObject)},
                    md5_hash AS {nameof(FileDto.MD5Hash)},
                    created_date AS {nameof(FileDto.CreatedDate)}
                    from blob_metadata where filename = @filename
                    """, new { filename }));

            return file;
        }

        public async Task<bool> UploadFileAsync(FileDto file)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();

            var result = await connection.ExecuteAsync("""
                insert into blob_metadata (id, filename, extension, size_in_bytes, binary_object, md5_hash, created_date)
                values (@Id, @Filename, @Extension, @SizeInBytes, @BinaryObject, @MD5Hash, @CreatedDate);
                """, file);

            transaction.Commit();

            return result > 0;

        }
    }
}

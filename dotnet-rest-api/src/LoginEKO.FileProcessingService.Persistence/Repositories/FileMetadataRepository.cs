using Dapper;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Persistence.Database;
using LoginEKO.FileProcessingService.Persistence.DML;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoginEKO.FileProcessingService.Persistence.Repositories
{
    public class FileMetadataRepository : IFileMetadataRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IIdGenerator _idGenerator;
        private readonly ApplicationContext _dbContext;

        public FileMetadataRepository(ApplicationContext dbContext, IDbConnectionFactory dbConnectionFactory, IIdGenerator idGenerator)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _idGenerator = idGenerator;
            _dbContext = dbContext;
        }

        public async Task<FileMetadata?> GetByMD5HashAsync(string md5Hash, IDbConnection? connection = null)
        {
            if (string.IsNullOrEmpty(md5Hash))
                throw new ArgumentException();

            var entity = await _dbContext.FileMetadata.SingleOrDefaultAsync(x => x.MD5Hash == md5Hash);

            return entity;


            /*** Db Connection already created    ***************************/
            if (connection != null)
            {
                return await connection.QuerySingleOrDefaultAsync<FileMetadata>(new CommandDefinition(FileMetadataQueries.GetFileMetadataByMd5HashSql, new { md5Hash }));
            }

            /*** New Db Connection *****************************************/
            using (connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                return await connection.QuerySingleOrDefaultAsync<FileMetadata>(new CommandDefinition(FileMetadataQueries.GetFileMetadataByMd5HashSql, new { md5Hash }));
            }
        }
        //private const string GetFileMetadataByMd5HashSql = $"""
        //            select
        //            id AS {nameof(FileMetadata.Id)},
        //            filename AS {nameof(FileMetadata.Filename)},
        //            extension AS {nameof(FileMetadata.Extension)},
        //            size_in_bytes AS {nameof(FileMetadata.SizeInBytes)},
        //            binary_object AS {nameof(FileMetadata.BinaryObject)},
        //            md5_hash AS {nameof(FileMetadata.MD5Hash)},
        //            created_date AS {nameof(FileMetadata.CreatedDate)}
        //            from blob_metadata where md5_hash = @md5Hash
        //            """;


        public async Task<bool> CreateFileMetadataAsync(FileMetadata file, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            file.CreatedDate = DateTime.Now;
            _dbContext.FileMetadata.Add(file);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;

            file.Id = _idGenerator.GenerateId();
            file.CreatedDate = DateTime.UtcNow;

            /*** Db Connection already created *******************************/
            if (connection != null)
            {

                if (transaction != null)
                {
                    result = await connection.ExecuteAsync(FileMetadataQueries.InsertFileMetadataSql, file, transaction);
                    return result > 0;
                }

                /* New Db Transaction *************************************/
                using (transaction = connection.BeginTransaction())
                {
                    try
                    {
                        result = await connection.ExecuteAsync(FileMetadataQueries.InsertFileMetadataSql, file, transaction);
                        transaction.Commit();
                        return result > 0;
                    }
                    catch (Exception)
                    {
                        //transaction.Rollback();
                        return false;
                    }
                }

            }

            /*** New Db Connection and Transaction *****************************************/
            using (connection = await _dbConnectionFactory.CreateConnectionAsync())
            {
                using (transaction = connection.BeginTransaction())
                {
                    try
                    {
                        result = await connection.ExecuteAsync(FileMetadataQueries.InsertFileMetadataSql, file, transaction);
                        transaction.Commit();
                        return result > 0;
                    }
                    catch (Exception)
                    {
                        //transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        //private const string InsertFileMetadataSql = """
        //                    insert into blob_metadata (id, filename, extension, size_in_bytes, binary_object, md5_hash, created_date)
        //                    values (@Id, @Filename, @Extension, @SizeInBytes, @BinaryObject, @MD5Hash, @CreatedDate);
        //                    """;
    }
}

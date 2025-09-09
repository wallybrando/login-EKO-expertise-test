using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.Database
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            await connection.ExecuteAsync("""
                create table if not exists blob_metadata (
                id UUID primary key,
                filename TEXT not null,
                extension TEXT not null,
                size_in_bytes BIGINT not null,
                binary_object BYTEA not null,
                md5_hash TEXT not null,
                created_date TIMESTAMP not null,
                UNIQUE (id, filename, md5_hash));
                """);
        }
    }
}

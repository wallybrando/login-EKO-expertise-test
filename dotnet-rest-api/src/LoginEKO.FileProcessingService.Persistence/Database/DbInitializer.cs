using Dapper;
using LoginEKO.FileProcessingService.Domain.Interfaces;


namespace LoginEKO.FileProcessingService.Persistence.Database
{
    //public class DbInitializer
    //{
    //    private readonly IDbConnectionFactory _dbConnectionFactory;

    //    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    //    {
    //        _dbConnectionFactory = dbConnectionFactory;
    //    }

    //    public async Task InitializeAsync()
    //    {
    //        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

    //        await connection.ExecuteAsync(DbTypes.CreateParkingBreakStatusType);
    //        await connection.ExecuteAsync(DbTypes.CreateTransverseDifferentialLockStatusType);
    //        await connection.ExecuteAsync(DbTypes.CreateWheelDriveStatusType);

    //        await connection.ExecuteAsync(DbExtensions.CreateUUIDModule);

    //        await connection.ExecuteAsync(DbTables.FileMetadata.CreateTableSql);
    //        await connection.ExecuteAsync(DbTables.TractorTelemetry.CreateTableSql);

    //    }
    //}
}

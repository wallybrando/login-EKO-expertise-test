using LoginEKO.FileProcessingService.Persistence.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LoginEKO.FileProcessingService.Api.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public const string Name = "Database";

        private readonly ApplicationContext _context;
        private ILogger<DatabaseHealthCheck> _logger;
        public DatabaseHealthCheck(ApplicationContext context, ILogger<DatabaseHealthCheck> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

                return canConnect ?
                    HealthCheckResult.Healthy() :
                    HealthCheckResult.Unhealthy();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database is unhealthy");
                return HealthCheckResult.Unhealthy("Database is unhealthy", ex);
            }
        }
    }
}

using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Entities.Base;
using LoginEKO.FileProcessingService.Domain.Validators;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Domain.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly ITractorTelemetryRepository _tractorTelemetryRepository;
        private readonly ICombineTelemetryRepository _combineTelemetryRepository;

        private readonly FilterValidator<TractorTelemetry> _tractorFilterValidator;
        private readonly FilterValidator<CombineTelemetry> _combineFilterValidator;

        private readonly SchemaRegistry<TractorTelemetry> _tractorSchemaRegistry;
        private readonly SchemaRegistry<CombineTelemetry> _combineSchemaRegistry;

        public TelemetryService(
            ITractorTelemetryRepository tractorTelemetryRepository,
            ICombineTelemetryRepository combineTelemetryRepository,
            FilterValidator<TractorTelemetry> tractorFilterValidator,
            FilterValidator<CombineTelemetry> combineFilterValidator,
            SchemaRegistry<TractorTelemetry> tractorSchemaRegistry,
            SchemaRegistry<CombineTelemetry> combineSchemaRegistry)
        {
            _tractorTelemetryRepository = tractorTelemetryRepository;
            _combineTelemetryRepository = combineTelemetryRepository;
            _tractorFilterValidator = tractorFilterValidator;
            _combineFilterValidator = combineFilterValidator;

            _tractorSchemaRegistry = tractorSchemaRegistry;
            _combineSchemaRegistry = combineSchemaRegistry;
        }

        public async Task<UnifiedTelemetry> GetTractorTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            ValidateFilter(paginatedFilter.Filters);

            var tractorFilters = new List<Filter>();
            var combintFilters = new List<Filter>();

            foreach (var filter in paginatedFilter.Filters)
            {
                if (_tractorFilterValidator.ValidateFilterFieldName(filter.Field))
                    tractorFilters.Add(filter);
                if (_combineFilterValidator.ValidateFilterFieldName(filter.Field))
                    combintFilters.Add(filter);
            }

            var tractorsDynamicFilters = CreateFilterExpressionForTractorTelemetry(tractorFilters);
            var combinesDynamicFilters = CreateFilterExpressionForCombineTelemetry(combintFilters);

            var tractorTelemetry = await _tractorTelemetryRepository.GetAsync(tractorsDynamicFilters, paginatedFilter.PageNumber, paginatedFilter.PageSize, token);
            var totalTractorTelemetryCount = await _tractorTelemetryRepository.GetCountAsync(tractorsDynamicFilters, token);
            var combineTelemetry = await _combineTelemetryRepository.GetAsync(combinesDynamicFilters, paginatedFilter.PageNumber, paginatedFilter.PageSize, token);
            var totalCombineTelemetryCount = await _combineTelemetryRepository.GetCountAsync(combinesDynamicFilters, token);

            return new UnifiedTelemetry
            {
                TotalTractorsTelemetryCount = totalTractorTelemetryCount,
                TractorTelemetry = tractorTelemetry,

                TotalCombinesTelemetryCount = totalCombineTelemetryCount,
                CombinesTelemetry = combineTelemetry
            };
        }

        private Expression<Func<TractorTelemetry, bool>> CreateFilterExpressionForTractorTelemetry(IEnumerable<Filter> tractorFilters)
        {
            var serialNumberFilters = tractorFilters
                .Where(x => x.Field.Equals(nameof(AgroVehicleTelemetry.SerialNumber), StringComparison.OrdinalIgnoreCase));

            var predicate = new DynamicFilterBuilder<TractorTelemetry>();
            if (serialNumberFilters.Any())
            {
                if (!_tractorSchemaRegistry.TryGetFieldType(nameof(AgroVehicleTelemetry.SerialNumber), out var fieldType))
                {
                    throw new FilterValidationException("xxxxxxxx");
                }

                var serialNumberFilterTuple = serialNumberFilters.Select(x => (x.Field, x.Operation, x.Value, fieldType));
                predicate.And(b => b.Or(serialNumberFilterTuple));

            }

            var filtersWithoutSerialNumber = tractorFilters
                .Where(x => !x.Field.Equals(nameof(AgroVehicleTelemetry.SerialNumber), StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => new { x.Field });

            foreach (var filter in filtersWithoutSerialNumber)
            {

                if (!_tractorSchemaRegistry.TryGetFieldType(filter.Key.Field, out var fieldType))
                {
                    throw new FilterValidationException("xxxxxxxx");
                }

                if (fieldType != null && fieldType.IsEnum)
                {
                    if (!_tractorSchemaRegistry.TryGetEnumType(filter.Key.Field, out fieldType))
                        throw new FilterValidationException("yyyyyyyy");
                }

                var filtersTuple = filter.Select(x => (x.Field, x.Operation, x.Value, fieldType));
                predicate.And(b => b.Or(filtersTuple));
            }

            return predicate.Build();
        }

        private Expression<Func<CombineTelemetry, bool>> CreateFilterExpressionForCombineTelemetry(IEnumerable<Filter> combineFilters)
        {
            var serialNumberFilters = combineFilters
                .Where(x => x.Field.Equals(nameof(AgroVehicleTelemetry.SerialNumber), StringComparison.OrdinalIgnoreCase));

            var predicate = new DynamicFilterBuilder<CombineTelemetry>();
            if (serialNumberFilters.Any())
            {
                if (!_combineSchemaRegistry.TryGetFieldType(nameof(AgroVehicleTelemetry.SerialNumber), out var fieldType))
                {
                    throw new FilterValidationException("xxxxxxxx");
                }

                var serialNumberFilterTuple = serialNumberFilters.Select(x => (x.Field, x.Operation, x.Value, fieldType));
                predicate.And(b => b.Or(serialNumberFilterTuple));
            }

            var filtersWithoutSerialNumber = combineFilters
                .Where(x => !x.Field.Equals(nameof(AgroVehicleTelemetry.SerialNumber), StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => new { x.Field });

            foreach (var filter in filtersWithoutSerialNumber)
            {

                if (!_combineSchemaRegistry.TryGetFieldType(filter.Key.Field, out var fieldType))
                {
                    throw new FilterValidationException("xxxxxxxx");
                }

                if (fieldType != null && fieldType.IsEnum /*== typeof(Enum)*/)
                {
                    if (!_combineSchemaRegistry.TryGetEnumType(filter.Key.Field, out fieldType))
                        throw new FilterValidationException("yyyyyyyy");
                }

                var filtersTuple = filter.Select(x => (x.Field, x.Operation, x.Value, fieldType));
                predicate.And(b => b.Or(filtersTuple));
            }

            return predicate.Build();
        }

        /// <summary>
        /// SerialNumber field can have multiple eqvivalent filters where as remain filters must be distinct
        /// </summary>
        /// <example>
        /// This is valid:
        ///     Field: Date, Operation: LessThan
        ///     Field: Date, Operation: GreaterThan
        ///     Field: SerialNumber, Operation: Equals
        ///     Field: SerialNumber, Operation: Equals
        ///     
        /// This is not valid:
        ///     Field: Date, Operation: LessThan
        ///     Field: Date, Operation: LessThan
        /// </example>
        /// <param name="filters"></param>
        /// <returns></returns>
        private bool ValidateFilterUniqueness(IEnumerable<Filter> filters)
        {
            filters = filters
                .Where(x => x.Field != nameof(AgroVehicleTelemetry.SerialNumber));

            var result = filters
                 .GroupBy(x => new { x.Field, x.Operation })
                 .All(x => x.Count() == 1);

            return result;
        }

        private void ValidateFilter(IEnumerable<Filter> filters)
        {
            if (!ValidateFilterUniqueness(filters))
            {
                throw new FilterValidationException("Duplicated filters' parameters in filter criteria");
            }

            foreach (var filter in filters)
            {
                if (!_tractorFilterValidator.ValidateFilterFieldName(filter.Field) &&
                    !_combineFilterValidator.ValidateFilterFieldName(filter.Field))
                {
                    throw new FilterValidationException("One or more invalid fields in filter criteria");
                }

                if (!_tractorFilterValidator.ValidateOperation(filter.Field, filter.Operation) &&
                    !_combineFilterValidator.ValidateOperation(filter.Field, filter.Operation))
                {
                    throw new FilterValidationException("One or more invalid operations in filter criteria");
                }

                if (!_tractorFilterValidator.ValidFilterType(filter.Field) &&
                    !_combineFilterValidator.ValidFilterType(filter.Field))
                {
                    throw new FilterValidationException("Unknown data type for one or more filters");
                }

                if (!_tractorFilterValidator.ValidateNullAssignment(filter.Field, filter.Value, filter.Operation) &&
                    !_combineFilterValidator.ValidateNullAssignment(filter.Field, filter.Value, filter.Operation))
                {
                    throw new FilterValidationException("NULL filter value assigned to one or more non-nullable fields");
                }
            }
        }
    }
}

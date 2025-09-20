using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Entities.Base;
using LoginEKO.FileProcessingService.Domain.Validators;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<TelemetryService> _logger;

        public TelemetryService(
            ITractorTelemetryRepository tractorTelemetryRepository,
            ICombineTelemetryRepository combineTelemetryRepository,
            FilterValidator<TractorTelemetry> tractorFilterValidator,
            FilterValidator<CombineTelemetry> combineFilterValidator,
            SchemaRegistry<TractorTelemetry> tractorSchemaRegistry,
            SchemaRegistry<CombineTelemetry> combineSchemaRegistry,
            ILogger<TelemetryService> logger)
        {
            _tractorTelemetryRepository = tractorTelemetryRepository;
            _combineTelemetryRepository = combineTelemetryRepository;
            _tractorFilterValidator = tractorFilterValidator;
            _combineFilterValidator = combineFilterValidator;

            _tractorSchemaRegistry = tractorSchemaRegistry;
            _combineSchemaRegistry = combineSchemaRegistry;

            _logger = logger;
        }

        public async Task<UnifiedTelemetry> GetUnifiedTelemetriesAsync(PaginatedFilter paginatedFilter, CancellationToken token = default)
        {
            _logger.LogTrace("GetUnifiedTelemetriesAsync(PaginatedFilter, CancellationToken)");

            /*** Check filters validity *******************************/
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

            /*** Create dynamic conditions for tractors and combines *******************************/
            var tractorsDynamicFilters = CreateFilterExpressionForTractorTelemetry(tractorFilters);
            var combinesDynamicFilters = CreateFilterExpressionForCombineTelemetry(combintFilters);

            /*** Get results and return it to caller *******************************/
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
            var predicate = new DynamicFilterBuilder<TractorTelemetry>();

            /*** Handles SerialNumber filter  *************************************/
            var serialNumberFilters = GetSerialNumberFilters(tractorFilters);
            HandleSerialNumberFilters(serialNumberFilters, ref predicate);

            /*** Grouping remained filters by field name **************************/
            var filtersWithoutSerialNumber = tractorFilters
                .Where(x => !x.Field.Equals(nameof(AgroVehicleTelemetry.SerialNumber), StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => new { x.Field });

            foreach (var filter in filtersWithoutSerialNumber)
            {
                var tempFilter = new List<Filter>(filter);
                if (!_tractorSchemaRegistry.TryGetFieldType(filter.Key.Field, out var fieldType))
                {
                    _logger.LogError("Filter data type is not assigned to filter '{Field}", filter.Key.Field);
                    throw new InvalidOperationException($"Filter data type is not assigned to filter '{filter.Key.Field}");
                }

                if (fieldType != null && fieldType == typeof(Enum))
                {
                    if (!_tractorSchemaRegistry.TryGetEnumType(filter.Key.Field, out fieldType))
                    {
                        _logger.LogError("Filter data type is not assigned to filter '{Field}", filter.Key.Field);
                        throw new InvalidOperationException($"Filter data type is not assigned to filter '{filter.Key.Field}");
                    }
                }

                /*** Handles filter  **********************************************/
                HandleNonSerialNumberFilters(tempFilter, fieldType, ref predicate);
            }

            return predicate.Build();
        }

        private Expression<Func<CombineTelemetry, bool>> CreateFilterExpressionForCombineTelemetry(IEnumerable<Filter> combineFilters)
        {
            var predicate = new DynamicFilterBuilder<CombineTelemetry>();

            /*** Grouping remained filters by field name **************************/
            var serialNumberFilters = GetSerialNumberFilters(combineFilters);
            HandleSerialNumberFilters(serialNumberFilters, ref predicate);

            /*** Grouping remained filters by field name **************************/
            var filtersWithoutSerialNumber = combineFilters
                .Where(x => !x.Field.Equals(nameof(AgroVehicleTelemetry.SerialNumber), StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => new { x.Field });

            foreach (var filter in filtersWithoutSerialNumber)
            {
                var tempFilter = new List<Filter>(filter);
                if (!_combineSchemaRegistry.TryGetFieldType(filter.Key.Field, out var fieldType))
                {
                    _logger.LogError("Filter data type is not assigned to filter '{Field}", filter.Key.Field);
                    throw new InvalidOperationException($"Filter data type is not assigned to filter '{filter.Key.Field}");
                }

                if (fieldType != null && fieldType == typeof(Enum))
                {
                    if (!_combineSchemaRegistry.TryGetEnumType(filter.Key.Field, out fieldType))
                    {
                        _logger.LogError("Filter data type is not assigned to filter '{Field}", filter.Key.Field);
                        throw new InvalidOperationException($"Filter data type is not assigned to filter '{filter.Key.Field}");
                    }
                }

                /*** Handles filter  **********************************************/
                HandleNonSerialNumberFilters(tempFilter, fieldType, ref predicate);
            }

            return predicate.Build();
        }

        /// <summary> Filters only criterias for SerialNumber field </summary>
        /// <remarks>Filter criteria may have multiple SerialNumber filters with same operation</remarks>
        /// <example>
        /// Input filters can look like this:
        /// {
        ///     "field": "SerialNumber",
        ///     "operation": "Contains",
        ///     "value": "A5304997"
        /// },
        /// {
        ///     "field": "SerialNumber",
        ///     "operation": "Contains",
        ///     "value": "C7502627"
        /// }
        /// </example>
        private IEnumerable<Filter> GetSerialNumberFilters(IEnumerable<Filter> filters)
        {
            return filters
                .Where(x => x.Field.Equals(nameof(AgroVehicleTelemetry.SerialNumber), StringComparison.OrdinalIgnoreCase));
        }


        /// <summary> Builds predicate for SerialNumber field </summary>
        /// <remarks>Filter criteria may have multiple SerialNumber filters with same operation</remarks>
        /// <example>
        /// Output predicate will be in this format:
        ///     (SerialNumber = const1 OR SerialNumber = const2 OR SerialNumber LIKE '%const3%') 
        /// </example>
        private void HandleSerialNumberFilters<T>(IEnumerable<Filter> serialNumberFilters, ref DynamicFilterBuilder<T> predicate)
        {
            if (serialNumberFilters.Any())
            {
                if (!_tractorSchemaRegistry.TryGetFieldType(nameof(AgroVehicleTelemetry.SerialNumber), out var fieldType))
                {
                    _logger.LogError($"Filter data type is not assigned to filter '{nameof(AgroVehicleTelemetry.SerialNumber)}");
                    throw new InvalidOperationException($"Filter data type is not assigned to filter '{nameof(AgroVehicleTelemetry.SerialNumber)}");
                }

                var serialNumberFilterTuple = serialNumberFilters.Select(x => (x.Field, x.Operation, x.Value, fieldType));
                predicate.And(b => b.Or(serialNumberFilterTuple));
            }
        }

        /// <summary> Builds predicate for SerialNumber field </summary>
        /// <remarks>Filter criteria may have ONLY ONE filter with same operation</remarks>
        /// <example>
        /// Valid input filters can look like this:
        /// {
        ///     "field": "Date",
        ///     "operation": "Equals",
        ///     "value": "2022-10-07 9:58:54"
        /// },
        /// {
        ///     "field": "Date",
        ///     "operation": "GreaterThan",
        ///     "value": "2022-10-07 9:58:54"
        /// }
        /// 
        /// Invalid input filters can look like this:
        /// {
        ///     "field": "Date",
        ///     "operation": "Equals",
        ///     "value": "2022-10-07 9:58:54"
        /// },
        /// {
        ///     "field": "Date",
        ///     "operation": "Equals",
        ///     "value": "2023-10-07 9:58:54"
        /// }
        /// 
        /// Output predicate will be in this format:
        ///     1) (Date = dateConst1 AND Date > dateConst2) - avoid this filter because it will always return result that matches with first condition
        ///     2) ((Date > dateConst1 AND Date < dateConst3) AND Date = const3) - again avoid this filter because it will return result that matches with first condition
        /// </example>
        private void HandleNonSerialNumberFilters<T>(List<Filter> filters, Type fieldType, ref DynamicFilterBuilder<T> predicate)
        {
            if (filters.Count(x => x.Operation == Models.Enums.FilterOperation.LESSTHAN || x.Operation == Models.Enums.FilterOperation.GREATERTHAN) == 2)
            {
                var rangeFilterTuple = filters
                    .Where(x => x.Operation == Models.Enums.FilterOperation.LESSTHAN || x.Operation == Models.Enums.FilterOperation.GREATERTHAN)
                    .Select(x => (x.Field, x.Operation, x.Value, fieldType));
                predicate.And(b => b.And(rangeFilterTuple));

                filters = filters.Where(x => x.Operation != Models.Enums.FilterOperation.LESSTHAN && x.Operation != Models.Enums.FilterOperation.GREATERTHAN)
                    .ToList();
            }

            if (filters.Count != 0)
            {
                var filtersTuple = filters.Select(x => (x.Field, x.Operation, x.Value, fieldType));
                predicate.And(b => b.Or(filtersTuple));
            }
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
                _logger.LogError("Duplicated filters' parameters in filter criteria");
                throw new FilterValidationException("Duplicated filters' parameters in filter criteria");
            }

            foreach (var filter in filters)
            {
                if (!_tractorFilterValidator.ValidateFilterFieldName(filter.Field) &&
                    !_combineFilterValidator.ValidateFilterFieldName(filter.Field))
                {
                    _logger.LogError("Field {Field} is invalid in filter criteria", filter.Field);
                    throw new FilterValidationException($"Field '{filter.Field}' is invalid in filter criteria");
                }

                if (!_tractorFilterValidator.ValidateOperation(filter.Field, filter.Operation) &&
                    !_combineFilterValidator.ValidateOperation(filter.Field, filter.Operation))
                {
                    _logger.LogError("Operation '{Operation}' is invalid for field '{Field}' in filter criteria", filter.Operation, filter.Field);
                    throw new FilterValidationException($"Operation '{filter.Operation}' is invalid for field '{filter.Field}' in filter criteria");
                }

                //if (!_tractorFilterValidator.ValidFilterType(filter.Field) &&
                //    !_combineFilterValidator.ValidFilterType(filter.Field))
                //{
                //    // Error indicates that filter is valid but we (API) didn't include filter in allowed filters
                //    // Situation when system starts to support new filter but our API has not implemented it.
                //    _logger.LogError("Filter data type is not assigned to filter '{Field}", filter.Field);
                //    throw new InvalidOperationException($"Filter data type is not assigned to filter '{filter.Field}'");
                //}

                if (!_tractorFilterValidator.ValidateNullAssignment(filter.Field, filter.Value, filter.Operation) &&
                    !_combineFilterValidator.ValidateNullAssignment(filter.Field, filter.Value, filter.Operation))
                {
                    _logger.LogError("NULL value cannot be assigned to non-nullable field '{Field}'", filter.Field);
                    throw new FilterValidationException($"NULL value cannot be assigned to non-nullable field '{filter.Field}'");
                }
            }
        }
    }
}

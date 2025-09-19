using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace LoginEKO.FileProcessingService.Domain.Services.FileExtractors
{
    public class CsvFileExtractor : IFileExtractor
    {
        public FileType Type { get; init; }

        public CsvFileExtractor()
        {
            Type = FileType.CSV;
        }

        public async Task<IEnumerable<string[]>> ExtractDataAsync(IFormFile file, CancellationToken token = default)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            var line = string.Empty;
            bool isHeader = true;

            var fieldsCount = 0;

            var result = new List<string[]>();
            while ((line = await reader.ReadLineAsync(token)) != null)
            {
                if (isHeader)
                {
                    fieldsCount = line.Split(';').Length;
                    isHeader = false;
                    continue;
                }

                var splitedColumns = line.Split(',');
                var telemetryData = splitedColumns[2].Split(';');

                var dateAsString = $"{splitedColumns[0]} {splitedColumns[1]} {telemetryData[0]}";
                var date = DateTime.Parse(dateAsString);

                var columnValues = new string[fieldsCount];
                for (int i = 0; i < fieldsCount; i++)
                {
                    if (i == 0)
                    {
                        columnValues[i] = dateAsString;
                    }
                    else
                        columnValues[i] = telemetryData[i];
                }

                result.Add(columnValues);
            }

            return result;
        }
    }
}

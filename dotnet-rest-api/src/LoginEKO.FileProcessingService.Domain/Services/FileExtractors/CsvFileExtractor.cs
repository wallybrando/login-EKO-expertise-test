using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace LoginEKO.FileProcessingService.Domain.Services.FileExtractors
{
    public class CsvFileExtractor : ITextFileExtractor
    {
        public FileType Type { get; init; }

        public CsvFileExtractor()
        {
            Type = FileType.CSV;
        }

        public async Task<IEnumerable<string[]>> ExtractDataAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            var line = "";
            bool isHeader = true;

            var columnsNumber = 0;
            var counter = 1;

            var extractedData = new List<string[]>();
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (isHeader)
                {
                    columnsNumber = line.Split(';').Length;
                    isHeader = false;
                    counter++;
                    continue;
                }

                var values = new string[columnsNumber];

                var columns = line.Split(',');
                var actualData = columns[2].Split(';');

                var dateAsString = $"{columns[0]} {columns[1]} {actualData[0]}";
                var date = DateTime.Parse(dateAsString);

                for (int i = 0; i < columnsNumber; i++)
                {
                    if (i == 0)
                    {
                        values[i] = dateAsString;
                    }
                    else
                        values[i] = actualData[i];
                }

                extractedData.Add(values);
            }

            return extractedData;
        }
    }
}

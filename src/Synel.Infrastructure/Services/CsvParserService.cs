using CsvHelper;
using Microsoft.AspNetCore.Http;
using Synel.Domain.Entities.Employees;
using Synel.Domain.Interfaces.Services;
using Synel.Web.Mappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synel.Infrastructure.Services
{
    public class CsvParserService : ICsvParserService
    {
        public List<Employee> ReadCSVFile(IFormFile csvFile)
        {
            try
            {
                using (var streamReader = new StreamReader(csvFile.OpenReadStream(), Encoding.Default))
                using (var csvReader = new CsvReader(streamReader, new CultureInfo("en-GB"))) //Used CultureInfo("en-GB") for "dd/MM/yyyy" teplate
                {
                    csvReader.Context.RegisterClassMap<EmployeeMap>();
                    var employeeRecords = new List<Employee>();

                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        employeeRecords.Add(csvReader.GetRecord<Employee>());
                    }
                    return employeeRecords;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<byte[]> WriteCSVFileAsync(IEnumerable<Employee> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, new CultureInfo("en-GB"));

                csvWriter.Context.RegisterClassMap<EmployeeMap>();
                await csvWriter.WriteRecordsAsync(records);
            }

            return memoryStream.ToArray();
        }
    }
}

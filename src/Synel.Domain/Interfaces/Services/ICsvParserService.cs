using Microsoft.AspNetCore.Http;
using Synel.Domain.Entities.Employees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synel.Domain.Interfaces.Services
{
    public interface ICsvParserService
    {
        List<Employee> ReadCSVFile(IFormFile csvFile);
        Task<byte[]> WriteCSVFileAsync(IEnumerable<Employee> employees);
    }
}

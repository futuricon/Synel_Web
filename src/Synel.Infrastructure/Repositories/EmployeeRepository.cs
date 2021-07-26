using Microsoft.EntityFrameworkCore;
using Synel.Domain.Entities.Employees;
using Synel.Domain.Interfaces;
using Synel.Domain.Interfaces.Repositories;
using Synel.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synel.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get all Employees
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        //Get a specific Employee by id
        public async Task<Employee> GetEmployee(string id)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        //Add new Employee
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        //Update a specific Employee by id
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (result != null)
            {
                result.PayrollNumber = employee.PayrollNumber;
                result.Forenames = employee.Forenames;
                result.Surname = employee.Surname;
                result.DateOfBirth = employee.DateOfBirth;
                result.Telephone = employee.Telephone;
                result.Mobile = employee.Mobile;
                result.Address = employee.Address;
                result.Address2 = employee.Address2;
                result.Postcode = employee.Postcode;
                result.Email = employee.Email;
                result.StartDate = employee.StartDate;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        //Delete a specific Employee by id
        public async Task DeleteEmployeeAsync(string id)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.Employees.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}

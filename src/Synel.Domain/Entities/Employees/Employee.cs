using System;
using System.ComponentModel.DataAnnotations;

namespace Synel.Domain.Entities.Employees
{
    public class Employee 
    {
        public Employee()
        {
            Id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        }
        public string Id { get; set; }

        public string PayrollNumber { get; set; }

        public string Forenames { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Telephone { get; set; }

        public int Mobile { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string Postcode { get; set; }

        public string Email { get; set; }

        public DateTime StartDate { get; set; }
    }
}

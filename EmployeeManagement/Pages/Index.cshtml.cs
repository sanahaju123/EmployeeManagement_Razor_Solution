using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Models;

namespace EmployeeManagement.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int EmployeeID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0.00")]
        public decimal Salary { get; set; }

        public List<Employee> Employees { get; set; }

        public void OnGet()
        {
            Employees = FetchEmployees();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Employees = FetchEmployees();
           
            int newEmployeeId = Employees.Max(e => e.EmployeeID) + 1;

            var newEmployee = new Employee
            {
                EmployeeID = newEmployeeId,
                Name = Name,
                Email = Email,
                Salary = Salary
            };
            
            Employees.Add(newEmployee);

            return Content("Form submitted successfully.");
        }

        private List<Employee> FetchEmployees()
        {
            
            return new List<Employee>
            {
                new Employee { EmployeeID = 1, Name = "BC", Email = "bc@example.com", Salary = 50000 },
                new Employee { EmployeeID = 2, Name = "BC", Email = "bc@example.com", Salary = 60000 },
                new Employee { EmployeeID = 3, Name = "BC", Email = "bc@example.com", Salary = 70000 }
            };
        }

    }
}
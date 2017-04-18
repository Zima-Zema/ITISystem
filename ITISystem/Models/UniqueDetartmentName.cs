using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    public class UniqueDetartmentName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            Department department = validationContext.ObjectInstance as Department;

            if (String.IsNullOrWhiteSpace(department.Name)||department.Name==null)
            {
                return new ValidationResult("Birthdate is Required");

            }
            var dept = _context.Departments.FirstOrDefault(dd => dd.Name == department.Name);

            if (dept==null)
            {
                return ValidationResult.Success;
            }
            
            else
            {
                if (dept.Department_Id == department.Department_Id && string.Compare(dept.Name, department.Name) == 0)
                {
                        return ValidationResult.Success;
                }
                if (string.Compare(dept.Name, department.Name) != 0)
                {
                        return ValidationResult.Success;
                }
                return new ValidationResult("Department Name Is Already Taken");

            }
        }
    }
}
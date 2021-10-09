using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CrudeOperations.Models
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                        ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [MaxLength(10)]
        public String ContactNumber { get; set; }

        public int Age { get; set; }

        public int AddEmployee(EmployeeModel empModel)
        {
            int age = 0;
            age = DateTime.Now.Subtract(empModel.DateOfBirth).Days;
            age = age / 365;
            using (var context = new EmployeeEntities())
            {
                EmployeeTable emp = new EmployeeTable()
                {
                    EmpId = empModel.EmpId,
                    FirstName = empModel.FirstName,
                    LastName = empModel.LastName,
                    DateOfBirth = empModel.DateOfBirth,
                    Gender = empModel.Gender,
                    EmailAddress = empModel.EmailAddress,
                    ContactNumber = empModel.ContactNumber,
                    Age = age,
                };
                context.EmployeeTable.Add(emp);
                context.SaveChanges();
                return EmpId;
            }
        }

        public List<EmployeeModel> GetAllEmployeeDetails()
        {
            using (var context = new EmployeeEntities())
            {
                var Result = context.EmployeeTable.Select(x => new EmployeeModel()
                {
                    EmpId = x.EmpId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = (DateTime)x.DateOfBirth,
                    Gender = x.Gender,
                    EmailAddress = x.EmailAddress,
                    ContactNumber = x.ContactNumber,
                    Age = (int)x.Age,

                }).ToList();
                return Result; ;
            }
        }

        public EmployeeModel GetEmployeeDetails(int id)
        {
            using (var context = new EmployeeEntities())
            {
                var Result = context.EmployeeTable.Where(x => x.EmpId == id).Select(x => new EmployeeModel()
                {
                    EmpId = x.EmpId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = (DateTime)x.DateOfBirth,
                    Gender = x.Gender,
                    EmailAddress = x.EmailAddress,
                    ContactNumber =x.ContactNumber,
                }).FirstOrDefault();
                return Result; ;
            }
        }

            public bool UpdateEmployeeDetails(int id, EmployeeModel model)
            {
                using (var context = new EmployeeEntities())
                {
                    var employee = new EmployeeTable();
                    if (employee != null)
                    {
                        employee.EmpId = model.EmpId;
                        employee.FirstName = model.FirstName;
                        employee.LastName = model.LastName;
                        employee.DateOfBirth = (DateTime)model.DateOfBirth;
                        employee.Gender = model.Gender;
                        employee.EmailAddress = model.EmailAddress;
                        employee.ContactNumber = model.ContactNumber;
                    };
                    context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
        public bool DeleteEmployee(int id)
        {
            using (var context = new EmployeeEntities())
            {
                var employee = new EmployeeTable();

                {
                    employee.EmpId = id;
                }
                context.Entry(employee).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
        }   

    }
}

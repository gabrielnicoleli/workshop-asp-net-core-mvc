using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "{0} size between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter valid email")]
        public string Email { get; set; }
 
        [Required(ErrorMessage = "{0} required!")]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [Display(Name = "Base Salary")]
        [Range(100.0, 5000.0, ErrorMessage ="{0} must be from {1} to {2}")]
        // [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Salary { get; set; }
        public Department  Department { get; set; }
        public int DepartmentID { get; set; }
        public ICollection<SalesRecord> SalesRecord { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int iD, string name, string email, DateTime birthDate, double salary, Department department)
        {
            ID = iD;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salary = salary;
            Department = department;
        }

        public void addSales(SalesRecord sr)
        {
            SalesRecord.Add(sr);
        }

        public void removeSales(SalesRecord sr)
        {
            SalesRecord.Remove(sr);

        }

        public double totalSales(DateTime dtInicial, DateTime dtFinal)
        {

            double sales = SalesRecord.Where(sr => sr.Date >= dtInicial && sr.Date <= dtFinal).Sum(sr => sr.Amount);
            return sales;
        }
    }
}

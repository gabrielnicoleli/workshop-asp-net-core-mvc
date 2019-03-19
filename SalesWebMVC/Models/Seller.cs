﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double Salary { get; set; }
        public Department  Department { get; set; }
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
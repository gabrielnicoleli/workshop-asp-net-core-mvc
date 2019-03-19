using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Seller { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public void addSeller(Seller sr)
        {
            Seller.Add(sr);
        }

        public double totalDepSales(DateTime dtInicial, DateTime dtFinal)
        {
            double sales = Seller.Sum(sr => sr.totalSales(dtInicial, dtFinal));
            return sales;
        }
    }


}

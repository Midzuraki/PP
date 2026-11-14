using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Laba0
{
    public class CatalogCard
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double cal { get; set; }
        public double weight { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public string img { get; set; }

        public CatalogCard()
        {
        }
        public CatalogCard(int id, string name, string description, double cal, double weight, double price, int stock, string img)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.cal = cal;
            this.weight = weight;
            this.price = price;
            this.stock = stock;
            this.img = img;
        }
    }
}

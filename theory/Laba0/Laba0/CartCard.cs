using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Laba0
{
    public class CartCard
    {
        public int id { get; set; }
        public string nameBuyout { get; set; }
        public double calBuyout { get; set; }
        public double weightBuyout { get; set; }
        public double priceBuyout { get; set; }
        public int selectedBuyout { get; set; }
        public string imgBuyout { get; set; }

        public CartCard(int id, string name, double cal, double weight, double price, int selected, string img)
        {
            this.id = id;
            this.nameBuyout = name;
            this.calBuyout = cal;
            this.weightBuyout = weight;
            this.priceBuyout = price;
            this.selectedBuyout = selected;
            this.imgBuyout = img;
        }
    }
}

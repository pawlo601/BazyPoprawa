using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Invoices.Domain.Model.Invoice
{
    public class Item
    {
        public virtual int IdOfProduct { get; set; }
        public virtual int Volume 
        {
            get
            {
                return _volume;
            }
            set
            {
                ChangeVolume(value);
            }
        }
        private int _volume;
        public virtual Product.Money Cost { get; set; }
        public Product.Product Thing { get; set; }

        public Item()
        {
            Random rand = new Random();
            Cost = new Product.Money();
            Thing = new Product.Product();
            Volume = rand.Next(1, 100);
            IdOfProduct = rand.Next(1, 100);
        }
        public Item(Product.Product product, int vol)
        {
            this.Thing = product;
            ChangeVolume(vol);
            Cost = new Product.Money(0.0f, Product.Waluta.PLN);
            IdOfProduct = Thing.ID;
        }
        public virtual void ChangeVolume(int vol)
        {
            if (vol < 0)
                throw new Exception("Zła ilość produktów.\n");
            else
                this._volume = vol;
        }
        public virtual void Count()
        {
            Cost = Thing.Price.GetGross();
            Cost.Value *= Volume;
        }
        public virtual void Count(Client.Discount dis)
        {
            if (Thing.ID == dis.IDProduct)
            {
                if (dis.Type == Client.Bonus.Netto)
                {
                    Cost = Thing.Price.NetPrice;
                    Cost.Value *= Volume;
                }
                if (dis.Type == Client.Bonus.Zniżka)
                {
                    Cost = Thing.Price.NetPrice;
                    Cost.Value *= Volume;
                    Cost.Value *= (float)(1.0f - dis.ValueOfBonus);
                }
            }
            else
                throw new Exception("Nie ta zniżka.\n");
        }
        public virtual string FormatString()
        {
            string nazwaProduktu = "";
            if (Thing != null)
                nazwaProduktu = Thing.Name;
            string text = "Id produktu: " + IdOfProduct.ToString() + "\n" +
                           "Nazwa produktu: " + nazwaProduktu + "\n" +
                           "Ilość: " + Volume.ToString() + "\n" +
                           "Wartość: " + Cost.Value.ToString() + Cost.NameOfCurrency.ToString() + "\n";
            return text;
        }
        public override string ToString()
        {
            return IdOfProduct.ToString() + "\n" +
                   Volume.ToString() + "\n" +
                   Cost.ToString() + "\n";
        }
    }
}

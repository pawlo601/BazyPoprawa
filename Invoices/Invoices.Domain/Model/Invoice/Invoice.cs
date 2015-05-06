using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iesi = Iesi.Collections.Generic;

namespace Invoices.Domain.Model.Invoice
{
    public class Invoice
    {
        public virtual string ID { get; set; }
        public virtual string Title
        {
            get
            {
                return _title;
            }
            set
            {
                ChengeTitle(value);
            }
        }
        private string _title;
        public virtual DateTime DateOfCreate { get; set; }
        public virtual int IdClient { get; set; }
        public virtual iesi.ISet<Item> ListOfProducts { get; set; }
        public Client.Client Contractor { get; set; }
        public virtual string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                SetComments(value);
            }
        }
        private string _comments;

        private bool CalcDis = false;

        public Invoice()
        {
            Random rand = new Random();
            DateOfCreate = DateTime.Now;
            ID = CreateID();
            Title = "Tytul" + rand.Next(1, 100000).ToString();
            Contractor = new Client.Client();
            IdClient = rand.Next(1, 100);
            ListOfProducts = new iesi.HashedSet<Item>();
            Comments = "Brak komentarza";
        }
        public Invoice(string title, Client.Client contractor)
        {
            DateOfCreate = DateTime.Now;
            ID = CreateID();
            ChengeTitle(title);
            this.Contractor = contractor;
            ListOfProducts = new iesi.HashedSet<Item>();
            Comments = "Brak komentarza";
        }
        private string CreateID()
        {
            return "FAK/" + DateOfCreate.DayOfYear.ToString() + "/" +
                    DateOfCreate.Hour.ToString() + "/" + DateOfCreate.Minute.ToString() +
                    "/" + (DateOfCreate.Second + DateOfCreate.Millisecond).ToString();
        }
        public virtual void SetComments(string comm)
        {
            if (comm.Length > 250)
                this._comments = comm.Substring(0, 250);
            else
                this._comments = comm;
        }
        public virtual void ChengeTitle(string title)
        {
            if (title.Length < 30)
                this._title = title;
            else
                this._title = title.Substring(0, 30);
        }
        public virtual void AddSomeItems()
        {
            Random rand = new Random();
            int j = rand.Next(1, 3);
            for (int i = 0; i < j; i++)
            {
                ListOfProducts.Add(new Item());
                System.Threading.Thread.Sleep(50);
            }
        }
        public virtual void AddProduct(Product.Product product, int volume)
        {
            ListOfProducts.Add(new Item(product, volume));
        }
        public virtual void AddItem(Item a)
        {
            ListOfProducts.Add(a);
        }
        public virtual Item GetItem(int IdProduct)
        {
            foreach (Item a in ListOfProducts)
            {
                if (a.Thing.ID == IdProduct)
                    return a;
            }
            return null;
        }
        public virtual Item GetItem(string name)
        {
            foreach (Item a in ListOfProducts)
            {
                if (a.Thing.Name == name)
                    return a;
            }
            return null;
        }
        public virtual void CalculateDiscount()
        {
            foreach (Item a in ListOfProducts)
            {
                bool Flag = true;
                foreach (Client.Discount b in Contractor.ListOfDiscount)
                {
                    if (b.IDProduct == a.Thing.ID)
                    {
                        a.Count(b);
                        Flag = false;
                    }
                }
                if (Flag)
                    a.Count();
            }
            CalcDis = true;
        }
        public virtual List<Product.Money> Podsumowanie()
        {
            if (!CalcDis)
                CalculateDiscount();
            List<Product.Money> lista = new List<Product.Money>();
            foreach (Item a in ListOfProducts)
            {
                //lista.Add(a.Cost);
                Dodaj(lista, a.Cost);
            }
            return lista;
        }
        private void Dodaj(List<Product.Money> lista, Product.Money cost)
        {
            bool flag = true;
            foreach (Product.Money a in lista)
            {
                if (a.NameOfCurrency.Equals(cost.NameOfCurrency))
                {
                    a.Value += cost.Value;
                    flag = false;
                }
            }
            if (flag)
                lista.Add(cost);
        }
        public virtual string ListOfProduct()
        {
            string text = "";
            foreach (Item a in ListOfProducts)
            {
                text += a.Thing.Name + "\n";
            }
            return text;
        }
        public virtual string FormatString()
        {
            string stringCon = "---";
            if (Contractor != null)
                stringCon = Contractor.FormatString();
            string pr = "";
            foreach (Item a in ListOfProducts)
                pr += a.FormatString() + "\n";
            string text = String.Format("ID: {1}{0}Title: {2}{0}Date of create: {3}{0}Właściciel:{0}{0}{4}{0}List of Productds:{0}{0}{5}{0}Comments:{0}{6}",
                Environment.NewLine, ID, Title, DateOfCreate.ToString(), stringCon, pr, Comments);
            return text;
        }
        public override string ToString()
        {
            string stringCon = "---";
            if (Contractor != null)
                stringCon = Contractor.ToString();
            string pr = "";
            foreach (Item a in ListOfProducts)
                pr += a.ToString() + "\n";
            string text = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                Environment.NewLine, ID, Title, DateOfCreate.ToString(), stringCon, pr, Comments);
            return text;
        }
    }
}

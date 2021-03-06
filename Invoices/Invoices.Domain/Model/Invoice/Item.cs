﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

/*
drop table ITEMS
go

CREATE TABLE [dbo].[ITEMS](
	[ID_Invoice] [varchar](50) NOT NULL,
    [idProduct] [int] NOT NULL,
    [volume] [int] NOT NULL,
    [value] [float] NULL,
    [nameofcurrency] [varchar](5)
)

GO

SET ANSI_PADDING OFF
GO
 */
namespace Invoices.Domain.Model.Invoice
{
    [HasSelfValidation]
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
        public virtual float Value 
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _cost.Value = value;
            }
        }
        private float _value;
        public virtual Product.Waluta NameOfCurrency
        {
            get
            {
                return _nameOfCurrency;
            }
            set
            {
                _nameOfCurrency = value;
                _cost.NameOfCurrency = value;
            }
        }
        private Product.Waluta _nameOfCurrency;
        public Product.Money Cost 
        { 
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
                _value = value.Value;
                _nameOfCurrency = value.NameOfCurrency;
            }
        }
        private Product.Money _cost;
        public Product.Product Thing { get; set; }

        public Item()
        {
            Random rand = new Random();
            Cost = new Product.Money();
            Thing = null;
            Volume = rand.Next(1, 100);
            IdOfProduct = rand.Next(1, 100);
        }
        public Item(Product.Product product, int vol)
        {
            if(product!=null)
            {
                this.Thing = product;
                ChangeVolume(vol);
                Cost = new Product.Money(product.Price.NetPrice.Value * vol, Product.Waluta.PLN);
                IdOfProduct = Thing.ID;
            }
            else
            {
                product = new Product.Product();
                this.Thing = product;
                ChangeVolume(vol);
                Cost = new Product.Money(product.Price.NetPrice.Value * vol, Product.Waluta.PLN);
                IdOfProduct = Thing.ID;
            }
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
            string przerwa="------------------------------------";
            string nazwaProduktu = "";
            if (Thing != null)
                nazwaProduktu = Thing.Name;
            string text = String.Format("{1}{0}Id produktu: {2}{0}Nazwa produktu: {3}{0}Ilość: {4}{0}Wartość: {5}{0}",
                                        Environment.NewLine,
                                        przerwa,
                                        IdOfProduct.ToString(),
                                        nazwaProduktu,
                                        Volume.ToString(),
                                        Cost.Value.ToString() + Cost.NameOfCurrency.ToString());
            return text;
        }
        public override string ToString()
        {
            return IdOfProduct.ToString() + "\n" +
                   Volume.ToString() + "\n" +
                   Cost.ToString() + "\n";
        }
        [SelfValidation]
        public virtual void ItemValidation(ValidationResults results)
        {
            if (Volume < 0 )
                results.AddResult(new ValidationResult("Ilość powinna być wieksza od 0 ", this, "ItemValidationVolume", string.Empty, null));
            if(Value<0.0f)
                results.AddResult(new ValidationResult("Wartość powinna być wieksza od 0 ", this, "ItemValidationValue", string.Empty, null));
            if(Thing==null)
                results.AddResult(new ValidationResult("Produkt nie powinien być null-em", this, "ItemValidationThing", string.Empty, null));
        }
    }
}

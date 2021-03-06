﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
/*
drop table DISCOUNTS
go

CREATE TABLE [dbo].[DISCOUNTS](
	[ID_Client] [int] NOT NULL,
    [idProduct] [int] NOT NULL,
    [valueOfBonus] [float] NOT NULL,
    [type] [varchar](15) NULL
)

GO

SET ANSI_PADDING OFF
GO
 */
namespace Invoices.Domain.Model.Client
{
    public enum Bonus { Zniżka, Netto };
    [HasSelfValidation]
    public class Discount
    {
        public virtual int IDProduct { get; set; }
        public virtual Bonus Type { get; set; }
        public virtual float ValueOfBonus { get; set; }

        public Discount()
        {
            Random rand = new Random();
            IDProduct = rand.Next(1, 1000);
            if (rand.Next(0, 100) > 50)
            {
                Type = Bonus.Netto;
                ValueOfBonus = 0.0f;
            }
            else
            {
                Type = Bonus.Zniżka;
                ValueOfBonus = (float)(rand.NextDouble());
            }
        }
        public Discount(int idProduct, Bonus type)
        {
            IDProduct = idProduct;
            Type = Bonus.Netto;
            ValueOfBonus = 0.0f;
        }
        public Discount(int idProduct, Bonus type, float bonus)
            :this(idProduct,type)
        {
            if (bonus < 0.0f || bonus >= 1.0f)
                throw new Exception("Niewłaściwy bonus.\n");
            else
                ValueOfBonus = bonus;
        }
        public virtual void ChangeType(Bonus type, float bonus)
        {
            if (type != Bonus.Zniżka)
                this.ValueOfBonus = 0.0f;
            else
                this.ValueOfBonus = bonus;
            this.Type = type;
        }
        public virtual void ChangeBonus(float bonus)
        {
            if (bonus >= 0.0f && bonus < 1.0f)
                ValueOfBonus = bonus;
            else
                throw new Exception("Niewłaściwy bonus.\n");
        }
        public virtual string FormatString()
        {
            return String.Format("ID produktu: {1}{0}Typ bonusu: {2}{0}Wartość zniżki: {3}",
                        Environment.NewLine, 
                        IDProduct.ToString(), 
                        Type.ToString(), 
                        ValueOfBonus.ToString()
                        );
        }
        public override string ToString()
        {
            return IDProduct.ToString() + "\n" +
                   Type.ToString() + "\n" +
                   ValueOfBonus.ToString();
        }
        [SelfValidation]
        public virtual void ValueOfBonusValidation(ValidationResults results)
        {
            if (ValueOfBonus < 0.0f && ValueOfBonus >= 1.0f)
                results.AddResult(new ValidationResult("Bonus powinien być >= 0 i < 1", this, "ValueOfBonusValidation", string.Empty, null));
        }
    }
}

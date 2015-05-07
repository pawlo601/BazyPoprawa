using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Client
{
    public class Regon
    {
        public virtual string NumberRegon { get; set; }
        public Regon()
        {
            NumberRegon = "12345678512347";
        }
        public Regon(string number)
        {
            try
            {
                if (number.Length == 9 && Regon9Znakowy(number))
                    this.NumberRegon = number;
                else if (number.Length == 14 && Regon14Zankowy(number))
                    this.NumberRegon = number;
                else
                    throw new Exception("Błąd w Regonie-ie.\n");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        private bool Regon9Znakowy(string number)
        {
            int[] numbers = new int[number.Length];
            for (int c = 0; c < number.Length; c++)
                if (!int.TryParse(number.Substring(c, 1), out numbers[c]))
                    throw new Exception("Błąd w Regonie-ie. Niewłaściwe znaki.\n");
            int sum =
                8 * numbers[0] +
                9 * numbers[1] +
                2 * numbers[2] +
                3 * numbers[3] +
                4 * numbers[4] +
                5 * numbers[5] +
                6 * numbers[6] +
                7 * numbers[7];
            sum %= 11;
            if (sum == numbers[8] || (sum == 10 && numbers[8] == 0))
                return true;
            else
                throw new Exception("Błąd w Regonie-ie. Błędne znaczenie\n");
        }
        private bool Regon14Zankowy(string number)
        {
            int[] numbers = new int[number.Length];
            for (int c = 0; c < number.Length; c++)
                if (!int.TryParse(number.Substring(c, 1), out numbers[c]))
                    throw new Exception("Błąd w Regonie-ie. Niewłaściwe znaki.\n");
            int sum =
                2 * numbers[0] +
                4 * numbers[1] +
                8 * numbers[2] +
                5 * numbers[3] +
                0 * numbers[4] +
                9 * numbers[5] +
                7 * numbers[6] +
                3 * numbers[7] +
                6 * numbers[8] +
                1 * numbers[9] +
                2 * numbers[10] +
                4 * numbers[11] +
                8 * numbers[12];
            sum %= 11;
            if (sum == numbers[13])
                return true;
            else
                throw new Exception("Błąd w Regonie-ie. Błędne znaczenie\n");
        }
        public virtual string FormatString()
        {
            return "Regon: " + this.NumberRegon;
        }
        public override string ToString()
        {
            return this.NumberRegon;
        }
    }
}

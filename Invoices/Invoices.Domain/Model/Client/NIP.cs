using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Client
{
    public class NIP
    {
        public virtual string NumberNIP { get; set; }
        public NIP()
        {
            this.NumberNIP = "0000000000";
        }
        public NIP(string number)
        {
            if (number.Length != 10)
                throw new Exception("Błąd w NIP-ie. Zła długość.\n");
            else
            {
                int[] numbers = new int[number.Length];
                for (int c = 0; c < number.Length; c++)
                    if (!int.TryParse(number.Substring(c, 1), out numbers[c]))
                        throw new Exception("Błąd w NIP-ie.Niewłaściwe znaki.\n");
                int sum =
                    6 * numbers[0] +
                    5 * numbers[1] +
                    7 * numbers[2] +
                    2 * numbers[3] +
                    3 * numbers[4] +
                    4 * numbers[5] +
                    5 * numbers[6] +
                    6 * numbers[7] +
                    7 * numbers[8];
                sum %= 11;
                if (sum == numbers[9])
                    this.NumberNIP = number;
                else
                    throw new Exception("Błąd w NIP-ie. Niepoprawne znaczenie.\n");
            }
        }
        public string FormatString()
        {
            return "NIP: " + this.NumberNIP;
        }
        public override string ToString()
        {
            return NumberNIP;
        }
    }
}

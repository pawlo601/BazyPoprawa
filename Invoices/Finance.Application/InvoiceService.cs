using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Invoice;
using Invoices.Domain.Model.Invoice.Repositories;
using Finance.Infrastructure.Repositories;
using iesi = Iesi.Collections.Generic;
using collections = System.Collections.Generic;
using System.IO;

namespace Finance.Application
{
    public class InvoiceService:IInvoiceServices
    {
        private IInvoiceRepositories repo;

        public InvoiceService()
        {
            repo = new InvoiceIM();
        }
        public InvoiceService(IInvoiceRepositories re)
        {
            repo = re;
        }
        public void CreateInvoice(string id)
        {
            Invoice a = repo.Find(id);
            string path = @"c:\bazy\";
            path += a.ID + ".txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(a.FormatString());
                }
            }
        }
        public void SendToMailInvoice(string id)
        {
            throw new NotImplementedException();
        }
        public void SendToMailInvoice(string id, System.Net.Mail.MailAddress to)
        {
            throw new NotImplementedException();
        }
        public collections.List<Invoice> GetAllPerDate(DateTime p)
        {
            return repo.FindAllPerData(p);
        }
        public collections.List<Invoice> GetAllPerDateToDate(DateTime from, DateTime to)
        {
            collections.List<Invoice> a = new collections.List<Invoice>();
            while (from != to)
            {
                a.AddRange(repo.FindAllPerData(from));
                from.AddDays(1);
            }
            return a;
        }
        public collections.List<Invoice> GetAllPerIDC(int id)
        {
            return repo.FindAllPerContractor(id);
        }
    }
}

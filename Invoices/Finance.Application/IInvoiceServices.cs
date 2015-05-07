using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Invoice;
using Invoices.Domain.Model.Client;
using System.Net.Mail;
using collections = System.Collections.Generic;

namespace Finance.Application
{
    public interface IInvoiceServices
    {
        void CreateInvoice(string id);
        void SendToMailInvoice(string id);
        void SendToMailInvoice(string id, MailAddress to);
        collections.List<Invoice> GetAllPerClient(string name, string surname);
        collections.List<Invoice> GetAllPerCompany(string name);
        collections.List<Invoice> GetAllPerIDC(int id);
        collections.List<Invoice> GetAllPerDate(DateTime p);
        collections.List<Invoice> GetAllPerDateToDate(DateTime from, DateTime to);
    }
}

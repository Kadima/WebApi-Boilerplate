using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.ServiceModel.Tables
{
    public class _Plvi1
    {
        public int TrxNo { get; set; }
								public string VoucherNo { get; set; }
								public string VendorName { get; set; }
        public decimal InvoiceAmt { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string StatusCode { get; set; }
    }
}

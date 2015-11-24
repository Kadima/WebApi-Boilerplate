using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.ServiceModel.Tables
{
    public class Jmjm1
    {
        public string JobNo { get; set; }
        public string AwbBlNo { get; set; }
        public string CustomerCode { get; set; }
        public string JobType { get; set; }
        public string ModuleCode { get; set; }
        public string CustomerRefNo { get; set; }
        public Nullable<System.DateTime> Eta { get; set; }
        public Nullable<System.DateTime> Etd { get; set; }
        public string OriginCode { get; set; }
        public string DestCode { get; set; }
        public string VesselCode { get; set; }
        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public int Pcs { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal Volume { get; set; }
        public string AttachmentFlag { get; set; }
        public string StatusCode { get; set; }
    }
}

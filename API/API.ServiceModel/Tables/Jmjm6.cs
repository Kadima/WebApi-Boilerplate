using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.ServiceModel.Tables
{
    public class Jmjm6
    {
        public string JobNo { get; set; }
        public int LineItemNo { get; set; }
        public string ContainerNo { get; set; }
        public string Remark { get; set; }
        public string VehicleNo { get; set; }
        public string DriverNo { get; set; }
        public string CargoStatusCode { get; set; }
        public DateTime TruckDateTime { get; set; }
        public DateTime RecevieDateTime { get; set; }
        public DateTime ReadyDateTime { get; set; }
        public DateTime UnLoadDateTime { get; set; }
    }
}

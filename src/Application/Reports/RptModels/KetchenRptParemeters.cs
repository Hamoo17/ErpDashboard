using ErpDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Reports.RptModels
{
    public class KetchenRptParemeters
    {
        public string Branch { get; set; }
        public string Driver { get; set; }
        public DateTime RptDateTime { get; set; }
    }
}

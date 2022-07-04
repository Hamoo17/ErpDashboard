using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Units.Queries.Dto
{
    public class UnitResponse
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public decimal? DefQty { get; set; }
    }
}

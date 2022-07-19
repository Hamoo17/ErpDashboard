using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Customer.Quers.GetAllAreas
{
    public class GetAllAreaViewModal
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }

    }
}

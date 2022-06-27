using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Recipe.Queries.Dto
{
    public class ItemComponentHdrResponse
    {
        public int Id { get; set; }
        public int ComplexItemId { get; set; }
        public decimal QtyNeeded { get; set; }
        public int MainUnit { get; set; }

        public List<ItemComponentDetailResponse> itemComponentDetailResponse { get; set; }

    }
}

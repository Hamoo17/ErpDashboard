using ErpDashboard.Server.Reports;
using Microsoft.AspNetCore.Mvc;
namespace ErpDashboard.Server.Controllers.v1
{

    public class ReportsController : BaseApiController<ReportsController>
    {
        private protected Application.Reports.AllCustomersReport rptData;

        public ReportsController(Application.Reports.AllCustomersReport rptData)
        {
            this.rptData = rptData;
        }
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            MemoryStream ms = new MemoryStream();
            CustomersReport rpt = new CustomersReport();
            rpt.DataSource = rptData.CustomersList();
            rpt.ExportToPdf(ms);
            var data = ms.ToArray();
            return Ok(Convert.ToBase64String(data));
        }
    }
}

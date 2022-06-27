using ErpDashboard.Application.Features.PlanDays.Query.GetAll;
using ErpDashboard.Application.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ErpDashboard.Server.Reports;
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

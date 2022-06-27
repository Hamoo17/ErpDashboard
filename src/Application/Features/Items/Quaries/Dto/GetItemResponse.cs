namespace ErpDashboard.Application.Features.Items.Quaries.Dto
{
    public class GetItemResponse
    {
        public int Id { get; set; }
        public string ItemEnName { get; set; }
        public int CategoryId { get; set; }
        public int ItemType { get; set; }
        public int? ItemUnit1 { get; set; }
        public int? ItemUnit2 { get; set; }
        public int? ItemUnit3 { get; set; }
        public int? MainUnit { get; set; }
        public int MainUnitId { get; set; }
        public decimal? UnitRate1 { get; set; } // default by 1
        public decimal? UnitRate2 { get; set; }
        public decimal? UnitRate3 { get; set; }
    }
}

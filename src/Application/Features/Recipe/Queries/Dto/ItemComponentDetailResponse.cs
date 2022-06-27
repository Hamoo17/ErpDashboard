namespace ErpDashboard.Application.Features.Recipe.Queries.Dto
{
    public class ItemComponentDetailResponse
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int IsUnit_1_2_3 { get; set; }
        public string UnitName { get; set; }
        public decimal Qty { get; set; }
        public int ItemComHdr { get; set; }
        public int ItemType { get; set; }
    }
}

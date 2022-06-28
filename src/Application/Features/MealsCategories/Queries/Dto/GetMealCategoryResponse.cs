namespace ErpDashboard.Application.Features.MealsCategory.Queries.Dto
{
    public class GetMealCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public bool IsSnack { get; set; }

    }
}

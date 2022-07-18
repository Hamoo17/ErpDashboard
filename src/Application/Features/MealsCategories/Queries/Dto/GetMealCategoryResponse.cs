using ErpDashboard.Application.Models;

namespace ErpDashboard.Application.Features.MealsCategory.Queries.Dto
{
    public class GetMealCategoryResponse
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string Symbol { get; set; }
        public bool IsSnack { get; set; }
        public List<TbMealsType> tbMealsTypes { get; set; }
    }
}

namespace ErpDashboard.Client.Infrastructure.Routes
{
    public static class PlanCategoryEndPoints
    {
        public static string GetAll = "api/V1/PlanCategory";
        public static string Save = "api/V1/PlanCategory";
        public static string Delete(int id) => $"api/V1/PlanCategory/{id}";
    }
}

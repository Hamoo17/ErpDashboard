namespace ErpDashboard.Client.Infrastructure.Routes
{
    public static class PlanDayEndPoint
    {
        public static string GetAll = "api/v1/planDay";
        public static string Save = "api/V1/planDay";
        public static string Delete(int id) => $"api/V1/planDay/{id}";
    }
}

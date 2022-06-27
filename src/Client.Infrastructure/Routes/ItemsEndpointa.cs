namespace ErpDashboard.Client.Infrastructure.Routes
{
    public static class ItemsEndpointa
    {
        public static string SaveAsync = "/api/Item";
        public static string GetAll = "/api/Item";
        public static string DeleteAsync(int id) => $"/api/Item/{id}";

    }
}

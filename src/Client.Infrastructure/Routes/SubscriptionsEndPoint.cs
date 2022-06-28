namespace ErpDashboard.Client.Infrastructure.Routes
{
    public static class SubscriptionsEndPoint
    {
        public static string GetSidByPhone(string Phone) => $"api/v1/Subscription/{Phone}";
    }
}

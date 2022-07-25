namespace ErpDashboard.Client.Infrastructure.Routes
{
    public static class CustomersEndpoint
    {
        public static string GetAll(int pageNumber, int pageSize, string searchString, string[] orderBy)
		{
            var url = $"api/v1/Customer?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;

        }//= "api/v1/Customer";
        public static string Save = "api/v1/Customer";
        public static string GetCustomerCategory = "api/v1/Customer/GetAllCustomerCatrgories";
        public static string GetAreas = "api/v1/Customer/GetAllAreas";
        public static string isPhoneExist(int CustomerID , string Phone) => $"api/v1/Customer/isPhoneExist/{CustomerID}/{Phone}";
        public static string Delete(int id) => $"api/V1/Customer/{id}";
        public static string GetBranchies = "api/v1/Customer/GetAllBranchies";
        public static string isCustomerExist( string Phone) => $"api/v1/Customer/isCustomerExist/{Phone}";

    }
}

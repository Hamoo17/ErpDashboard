using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Routes
{
    public class CategoriesEndPoints
    {
        public static string GetAll = "api/V1/Categories";
        public static string Save = "api/V1/Categories";
        public static string Delete(int id) => $"api/V1/Categories/{id}";
    }
}

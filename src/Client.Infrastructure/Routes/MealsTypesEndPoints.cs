using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Routes
{
    public class MealsTypesEndPoints
    {
        public static string GetAll = "api/V1/MealsTypes";
        public static string Save = "api/V1/MealsTypes";
        public static string GetById(int id) => $"api/V1/MealsTypes/{id}";
        public static string Delete(int id) => $"api/V1/MealsTypes/{id}";
    }
}

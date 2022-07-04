using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Routes
{
    public class MealsCategoriesEndPoints
    {
        public static string GetAll = "api/V1/MealsCategories";
        public static string Save = "api/V1/MealsCategories";
        public static string GetById(int id) => $"api/V1/MealsCategories/{id}";
        public static string Delete(int id) => $"api/V1/MealsCategories/{id}";
    }
}

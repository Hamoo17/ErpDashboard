using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Routes
{
    public static class UnitsEndPoints
    {
        public static string GetAll = "api/V1/Units";
        public static string Save = "api/V1/Units";
        public static string GetById(int id) => $"api/V1/Units/{id}";
        public static string Delete(int id) => $"api/V1/Units/{id}";
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ErpDashboard.Shared.Constants.Permission
{
    public static class Permissions
    {
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
            public const string Export = "Permissions.Products.Export";
            public const string Search = "Permissions.Products.Search";
        }

        public static class Brands
        {
            public const string View = "Permissions.Brands.View";
            public const string Create = "Permissions.Brands.Create";
            public const string Edit = "Permissions.Brands.Edit";
            public const string Delete = "Permissions.Brands.Delete";
            public const string Export = "Permissions.Brands.Export";
            public const string Search = "Permissions.Brands.Search";
        }


        public static class Documents
        {
            public const string View = "Permissions.Documents.View";
            public const string Create = "Permissions.Documents.Create";
            public const string Edit = "Permissions.Documents.Edit";
            public const string Delete = "Permissions.Documents.Delete";
            public const string Search = "Permissions.Documents.Search";
        }

        public static class DocumentTypes
        {
            public const string View = "Permissions.DocumentTypes.View";
            public const string Create = "Permissions.DocumentTypes.Create";
            public const string Edit = "Permissions.DocumentTypes.Edit";
            public const string Delete = "Permissions.DocumentTypes.Delete";
            public const string Export = "Permissions.DocumentTypes.Export";
            public const string Search = "Permissions.DocumentTypes.Search";
        }

        public static class DocumentExtendedAttributes
        {
            public const string View = "Permissions.DocumentExtendedAttributes.View";
            public const string Create = "Permissions.DocumentExtendedAttributes.Create";
            public const string Edit = "Permissions.DocumentExtendedAttributes.Edit";
            public const string Delete = "Permissions.DocumentExtendedAttributes.Delete";
            public const string Export = "Permissions.DocumentExtendedAttributes.Export";
            public const string Search = "Permissions.DocumentExtendedAttributes.Search";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }
        public static class PlanCategory
        {
            public const string View = "Permissions.PlanCategory.View";
            public const string Create = "Permissions.PlanCategory.Create";
            public const string Edit = "Permissions.PlanCategory.Edit";
            public const string Delete = "Permissions.PlanCategory.Delete";
            public const string Export = "Permissions.PlanCategory.Export";
            public const string Search = "Permissions.PlanCategory.Search";
        }
        public static class planday
        {
            public const string View = "Permissions.Brands.View";
            public const string Create = "Permissions.Brands.Create";
            public const string Edit = "Permissions.Brands.Edit";
            public const string Delete = "Permissions.Brands.Delete";
            public const string Export = "Permissions.Brands.Export";
            public const string Search = "Permissions.Brands.Search";
        }
        public static class MealCategories
        {
            public const string View = "Permissions.MealCategories.View";
            public const string Create = "Permissions.MealCategories.Create";
            public const string Edit = "Permissions.MealCategories.Edit";
            public const string Delete = "Permissions.MealCategories.Delete";
            public const string Export = "Permissions.MealCategories.Export";
            public const string Search = "Permissions.MealCategories.Search";
        }
        public static class ItemDepartment
        {
            public const string View = "Permissions.ItemDepartment.View";
            public const string Create = "Permissions.ItemDepartment.Create";
            public const string Edit = "Permissions.ItemDepartment.Edit";
            public const string Delete = "Permissions.ItemDepartment.Delete";
            public const string Export = "Permissions.ItemDepartment.Export";
            public const string Search = "Permissions.ItemDepartment.Search";
        }
        public static class Item
        {
            public const string View = "Permissions.Item.View";
            public const string Create = "Permissions.Item.Create";
            public const string Edit = "Permissions.Item.Edit";
            public const string Delete = "Permissions.Item.Delete";
            public const string Export = "Permissions.Item.Export";
            public const string Search = "Permissions.Item.Search";
        }
        public static class Communication
        {
            public const string Chat = "Permissions.Communication.Chat";
        }
        public static class Company
        {
            public const string Select = "Permissions.Company.Select";
        }
        public static class Preferences
        {
            public const string ChangeLanguage = "Permissions.Preferences.ChangeLanguage";

            //TODO - add permissions
        }

        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

        public static class Hangfire
        {
            public const string View = "Permissions.Hangfire.View";
        }

        public static class AuditTrails
        {
            public const string View = "Permissions.AuditTrails.View";
            public const string Export = "Permissions.AuditTrails.Export";
            public const string Search = "Permissions.AuditTrails.Search";
        }
        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}
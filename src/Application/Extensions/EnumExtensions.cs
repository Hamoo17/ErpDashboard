using System.ComponentModel;

namespace ErpDashboard.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum val)
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? attributes[0].Description
                : val.ToString();
        }
        public static string Tips(this Enum val)
        {
            var attributes = (Tips[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(Tips), false);
            return attributes.Length > 0 ? attributes[0].Tip : val.ToString();
        }

    }
    public class Tips : Attribute
    {
        public string Tip { get; private set; }
        public Tips(string tip)
        {
            Tip = tip;
        }
    }
}
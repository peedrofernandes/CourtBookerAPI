using CourtBooker.Enuns;
using System.ComponentModel;
using System.Reflection;

namespace CourtBooker.Helpers
{
    public static class EnumHelper
    {
        public static List<EnumValueDescription> GetEnumValueDescriptionList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(e => new EnumValueDescription
                       {
                           Value = (int)(object)e,
                           Label = GetEnumDescription(e)
                       })
                       .ToList();
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}

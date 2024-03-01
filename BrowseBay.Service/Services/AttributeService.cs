using System.Reflection;

namespace BrowseBay.Service.Services
{
    public static class AttributeService
    {
        public static T? GetAttribute<T>(this Enum item) where T : Attribute
        {
            Type? typeEnum = item.GetType();

            string enumName = item.ToString();

            MemberInfo? memberInfo = typeEnum.GetField(enumName);

            if (memberInfo == null)
            {
                return default!;
            }

            T? attribute = (T?)Attribute.GetCustomAttribute(memberInfo, typeof(T));

            return attribute;
        }
    }
}

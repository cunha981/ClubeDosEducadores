using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Helper
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo info = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attrs =
                (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
                return attrs[0].Description;
            else
                return value.ToString();
        }

        public static string GetDescription<T>(T value)
        {
            FieldInfo info = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attrs =
                (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
                return attrs[0].Description;
            else
                return value.ToString();
        }

        public static string GetDescription<T>(string value)
        {
            var @enum = ToEnum<T>(value);
            return GetDescription<T>(@enum);
        }

        public static T GetEnum<T>(string description)
        {
            var list = GetEnumerable<T>();

            foreach (var item in list)
            {
                var valueDescription = GetDescription<T>(item);
                if (valueDescription.ToLower() == description.ToLower())
                    return item;
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T GetEnum<T>(int sequence)
        {
            return (T)Enum.ToObject(typeof(T), sequence);
        }

        public static IEnumerable<T> GetEnumerable<T>()
        {
            Type typeEnum = typeof(T);

            if (typeEnum.BaseType != typeof(Enum))
                throw new ArgumentException("T must be System.Enum");

            Array arrayEnum = Enum.GetValues(typeEnum);
            List<T> list = new List<T>(arrayEnum.Length);

            foreach (int val in arrayEnum)
            {
                list.Add((T)Enum.Parse(typeEnum, val.ToString()));
            }

            return list;
        }

        public static T ToEnum<T>(string valueString)
        {
            return (T)Enum.Parse(typeof(T), valueString, true);
        }

        public static IDictionary<int, string> ToDictionary<T>(this T value) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var list = new Dictionary<int, string>();
            var listEnum = GetEnumerable<T>().ToList();
            foreach (var valor in listEnum)
            {
                var description = GetDescription(valor);
                list.Add(Convert.ToInt32(valor), description);
            }
            return list;
        }

        public static IEnumerable ToEnumerable<T>() where T : IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var listEnum = GetEnumerable<T>().ToList();
            var array = new object[listEnum.Count];
            for (int i = 0; i < listEnum.Count; i++)
            {
                var value = listEnum[i];
                yield return new { Key = Convert.ToInt32(value), Value = GetDescription(value) };
            }
        }
    }
}

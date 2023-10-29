using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Simplified
{
    /// <summary>Статический класс для работы со свойствами через рефлексию.</summary>
    public static class PropertyValue
    {
        /// <summary>Словарь типов и метдов доступа (set, get) для их свойств.</summary>
        private static readonly Dictionary<Type, Dictionary<string, (MethodInfo set, MethodInfo get)>> Methods = new();

        /// <summary>Возвращает методы доступа для указаного свойства в указнном типе.</summary>
        /// <param name="type">Тип для которого надо получить методы доступа.</param>
        /// <param name="propertyName">Имя свойства для которого надо получить методы доступа.</param>
        /// <returns>Контейнер (set, get) с методами доступа.</returns>
        private static (MethodInfo set, MethodInfo get)? GetMethods(Type type, string propertyName)
        {
            if (!Methods.TryGetValue(type, out Dictionary<string, (MethodInfo set, MethodInfo get)> dict))
            {
                dict = new();
                foreach (PropertyInfo property
                    in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).OfType<PropertyInfo>())
                {
                    try
                    {
                        dict.Add(property.Name,
                    (
                        property.SetMethod,
                        property.GetMethod
                    )); ;

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                Methods.Add(type, dict);
            }

            return
                dict.TryGetValue(propertyName, out (MethodInfo set, MethodInfo get) methods)
                ? methods
                : null;
        }

        /// <summary>Задание значения свойству.</summary>
        /// <param name="obj">Объект, свойству которого надо задать значение.</param>
        /// <param name="propertyName">Имя свойства, которому надо задать значение.</param>
        /// <param name="value">Задаваемое значение.</param>
        public static void SetValue(object obj, string propertyName, object value)
        {
            var methods = GetMethods(obj.GetType(), propertyName);
            methods.Value.set.Invoke(obj, new object[] { value });
        }

        /// <summary>Получение значения свойства.</summary>
        /// <param name="obj">Объект, значение свойства которого надо получить.</param>
        /// <param name="propertyName">Имя свойства, значение которого надо получить.</param>
        /// <returns>Значение свойства.</returns>
        public static object GetValue(object obj, string propertyName)
        {
            var methods = GetMethods(obj.GetType(), propertyName);
            return methods.Value.get.Invoke(obj, null);
        }

        /// <summary>Возвращает список свойств объекта.</summary>
        /// <param name="obj">Объект для которого запрашивается список свойств.</param>
        /// <returns>Список имён всех свойств.</returns>
        public static IEnumerable<string> GetPropertyName(object obj)
        {
            _ = GetMethods(obj.GetType(), string.Empty);
            return Methods[obj.GetType()].Keys.ToArray();
        }
    }
}

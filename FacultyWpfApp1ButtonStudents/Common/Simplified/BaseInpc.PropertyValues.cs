using System.Collections.Generic;
using System.ComponentModel;

namespace Simplified
{
    // Реализация получения и здания значения свойству по его имени
    public abstract partial class BaseInpc : INotifyPropertyChanged
    {
        /// <summary>Установка значения свойства по его имени.</summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="value">Новое значение для свойства.</param>
        protected void SetValue(string propertyName, object value)
        {
            PropertyValue.SetValue(this, propertyName, value);
        }

        /// <summary>Получение значения свойства по его имени.</summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Значение свойства.</returns>
        protected object GetValue(string propertyName)
        {
            return PropertyValue.GetValue(this, propertyName);
        }

        /// <summary>Возвращает список всех свойств текущего экземпляра.</summary>
        /// <returns>Список имён всех свойств.</returns>
        protected IEnumerable<string> GetPropertyName()
        {
            return PropertyValue.GetPropertyName(this);
        }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HokeyViz.Common
{
    internal class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SendNotification(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, args);
        }

        protected void Change<T>(
            out T field,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            field = value;
            this.SendNotification(propertyName);
        }

        protected void Set<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = default)
        {
            if(!Equality.Equal(field, value))
            {
                this.Change(out field, value, propertyName);
            }
        }

        /// <summary>
        /// For when you can't use reference parameters
        /// </summary>
        protected void Set<T>(
            Action<T> setter,
            T value,
            T existing,
            [CallerMemberName] string propertyName = null)
        {
            if(!Equality.Equal(value, existing))
            {
                this.Change(setter, value, propertyName);
            }
        }

        /// <summary>
        /// Sets and notifies if changed. Returns the old value.
        /// </summary>
        protected T Set<T>(
            T value,
            Action<T> setter,
            Func<T> getter,
            [CallerMemberName] string propertyName = null)
        {
            var existing = getter();
            if (!Equality.Equal(value, existing))
            {
                this.Change(setter, value, propertyName);
            }
            return existing;
        }

        protected void Change<T>(
            Action<T> setter,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            setter(value);
            this.SendNotification(propertyName);
        }
    }
}

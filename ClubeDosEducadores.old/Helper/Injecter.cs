using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
using System;

namespace Helper
{
    public static class Injecter
    {
        public static T Clone<T>(this T obj)
        {
            var instance = Activator.CreateInstance<T>();
            instance.InjectFrom(obj);
            return instance;
        }

        public static T ConvertTo<T>(this object obj, IValueInjection injection = null)
        {
            var instance = Activator.CreateInstance<T>();

            if(obj == null)
            {
                return instance;
            }

            if (injection != null)
                instance.InjectFrom(injection, obj);
            else instance.InjectFrom(obj);
            return instance;
        }

        public static T ConvertTo<T>(this object obj, params string[] ignoredProps)
        {
            var instance = Activator.CreateInstance<T>();

            if (obj == null)
            {
                return instance;
            }

            instance.InjectFrom(new InjectionHelper(ignoredProps), obj);
            return instance;
        }

        public static T ConvertTo<T, TInject>(this object obj) where TInject : IValueInjection
        {
            var instance = Activator.CreateInstance<T>();

            if (obj == null)
            {
                return instance;
            }

            var injection = Activator.CreateInstance<TInject>();
            instance.InjectFrom(injection, obj);
            return instance;
        }

        public static T Inject<T>(this object from)
        {
            if (from == null)
            {
                return default(T);
            }

            var instance = Activator.CreateInstance<T>();
            instance.InjectFrom(from);
            return instance;
        }

        public static object Inject(this object to, object from, IValueInjection injection = null)
        {
            if (injection == null)
            {
                return to.InjectFrom(from);
            }

            return to.InjectFrom(injection, from);
        }

        public static object Inject(this object to, object from, params string[] ignoredProps)
        {
            return to.InjectFrom(new InjectionHelper(ignoredProps), from);
        }

        public static T Inject<T, V>(this T to, V from, IValueInjection injection = null)
            where V : class
        {
            if (injection == null)
            {
                return (T)to.InjectFrom(from);
            }

            return (T)to.InjectFrom(injection, from);
        }

        public static T Inject<T, V>(this T to, V from, params string[] ignoredProps)
            where V : class
        {
            return (T)to.InjectFrom(new InjectionHelper(ignoredProps), from);
        }
    }

    #region ValueInjection Helper
    public class InjectionHelper : LoopInjection
    {
        public InjectionHelper(params string[] ignoredProps)
        {
            this.ignoredProps = ignoredProps;
        }
    }
    #endregion
}

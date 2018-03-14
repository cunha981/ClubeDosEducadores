using System;
using System.Globalization;
using System.Web.Mvc;

namespace MundoCompilado.RF.App_Start.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, bindingContext.ValueProvider.GetValue(bindingContext.ModelName));
            try
            {
                if (!string.IsNullOrEmpty(displayFormat) && value != null)
                {
                    DateTime date;
                    displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                    if (DateTime.TryParseExact(value, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        return date;
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            string.Format("{0} is an invalid date format", value)
                        );
                    }
                }

                return null;
            }
            catch (Exception)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, String.Format("\"{0}\" is invalid.", bindingContext.ModelName));
                return null;
            }
        }
    }
}
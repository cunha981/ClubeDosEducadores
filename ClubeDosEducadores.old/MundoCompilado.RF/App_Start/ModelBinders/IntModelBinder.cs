using System;
using System.Web.Mvc;

namespace MundoCompilado.RF.App_Start.ModelBinders
{
    public class IntModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            try
            {
                if(valueResult == null)
                {
                    return 0;
                }

                if (valueResult.AttemptedValue.GetType().Name == "String"
                    && bindingContext.ModelType.Name == "Int32")
                {
                    string value;
                    if (valueResult.RawValue.GetType() == typeof(string[]))
                    {
                        value = ((string[])valueResult.RawValue)[0].ToString();
                    }
                    else
                    {
                        value = valueResult.RawValue.ToString();
                    }

                    if(!string.IsNullOrWhiteSpace(value))
                    {
                        return Convert.ToInt32(value);
                    }

                }
                else
                {
                    return Convert.ToInt32(valueResult.AttemptedValue);
                }
            }
            catch (FormatException)
            {
                
            }

            return 0;
        }
    }
}
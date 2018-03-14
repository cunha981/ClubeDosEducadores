using Helper.RegexHelper;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Helper
{
    public static class ValidatorExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            return email != null && RegexExtensions.IsValidEmail(email);
        }

        public static bool IsValidCep(this string cep)
        {
            return cep != null && RegexExtensions.IsValidCep(cep);
        }

        public static MvcHtmlString BootstrapValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            TagBuilder containerSpanBuilder = new TagBuilder("span");
            containerSpanBuilder.AddCssClass("field-error-box");

            TagBuilder containerIconBuilder = new TagBuilder("i");
            containerIconBuilder.AddCssClass("fa fa-asterisk m-r-1");

            containerSpanBuilder.InnerHtml = containerIconBuilder.ToString();
            containerSpanBuilder.InnerHtml += helper.ValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(containerSpanBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString BootstrapValidationMessage(this HtmlHelper htmlHelper, string modelName)
        {
            TagBuilder containerSpanBuilder = new TagBuilder("span");
            containerSpanBuilder.AddCssClass("field-error-box");

            TagBuilder containerIconBuilder = new TagBuilder("i");
            containerIconBuilder.AddCssClass("fa fa-asterisk m-r-1");

            containerSpanBuilder.InnerHtml = containerIconBuilder.ToString();
            containerSpanBuilder.InnerHtml += htmlHelper.ValidationMessage(modelName).ToString();

            return MvcHtmlString.Create(containerSpanBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ShowAlert<TModel>(this HtmlHelper<TModel> helper)
        {
            if (helper.ViewContext.Controller.TempData["HasError"] == null)
            {
                return MvcHtmlString.Create("");
            }

            var html = 
                $@"<div class='alert {helper.ViewContext.TempData["CSS"]} alert - dismissible' role='alert'>
                    <button type='button' class='close' data-dismiss='alert' aria-label='Close'>
                        <span aria-hidden='true'>&times;</span>
                    </button>
                    <strong>{helper.ViewContext.TempData["Warning"].ToString()}</strong> {helper.ViewContext.TempData["Message"]}
                </div>";

            return MvcHtmlString.Create(html);
        }

        public static void AlertError(this Controller controller, string warning = "Atenção!", string message = "Preencha os campos corretamente")
        {
            controller.TempData["HasError"] = true;
            controller.TempData["Warning"] = warning;
            controller.TempData["Message"] = message;
            controller.TempData["CSS"] = "alert-danger";
        }

        public static void AlertSuccess(this Controller controller, string warning = "", string message = "Operação realizada com sucesso")
        {
            controller.TempData["HasError"] = true;
            controller.TempData["Warning"] = warning;
            controller.TempData["Message"] = message;
            controller.TempData["CSS"] = "alert-success";
        }
        public static void AlertInfo(this Controller controller, string warning = "Atenção!", string message = "")
        {
            controller.TempData["HasError"] = true;
            controller.TempData["Warning"] = warning;
            controller.TempData["Message"] = message;
            controller.TempData["CSS"] = "alert-info";
        }

    }
}
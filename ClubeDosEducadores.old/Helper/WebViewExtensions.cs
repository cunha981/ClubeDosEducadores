using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Helper
{
    public static class WebViewExtensions
    {
        public static MvcHtmlString SelectStateFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var value = metaData.Model?.ToString();

            string select = $@"<select name='{metaData.PropertyName}' id='{metaData.PropertyName}' class='form-control'>
	                            <option value=''>Selecione</option>
	                            <option {GetValue("AC", value)}>Acre</option>
	                            <option {GetValue("AL", value)}>Alagoas</option>
	                            <option {GetValue("AP", value)}>Amapá</option>
	                            <option {GetValue("AM", value)}>Amazonas</option>
	                            <option {GetValue("BA", value)}>Bahia</option>
	                            <option {GetValue("CE", value)}>Ceará</option>
	                            <option {GetValue("DF", value)}>Distrito Federal</option>
	                            <option {GetValue("ES", value)}>Espirito Santo</option>
	                            <option {GetValue("GO", value)}>Goiás</option>
	                            <option {GetValue("MA", value)}>Maranhão</option>
	                            <option {GetValue("MS", value)}>Mato Grosso do Sul</option>
	                            <option {GetValue("MT", value)}>Mato Grosso</option>
	                            <option {GetValue("MG", value)}>Minas Gerais</option>
	                            <option {GetValue("PA", value)}>Pará</option>
	                            <option {GetValue("PB", value)}>Paraíba</option>
	                            <option {GetValue("PR", value)}>Paraná</option>
	                            <option {GetValue("PE", value)}>Pernambuco</option>
	                            <option {GetValue("PI", value)}>Piauí</option>
	                            <option {GetValue("RJ", value)}>Rio de Janeiro</option>
	                            <option {GetValue("RN", value)}>Rio Grande do Norte</option>
	                            <option {GetValue("RS", value)}>Rio Grande do Sul</option>
	                            <option {GetValue("RO", value)}>Rondônia</option>
	                            <option {GetValue("RR", value)}>Roraima</option>
	                            <option {GetValue("SC", value)}>Santa Catarina</option>
	                            <option {GetValue("SP", value)}>São Paulo</option>
	                            <option {GetValue("SE", value)}>Sergipe</option>
	                            <option {GetValue("TO", value)}>Tocantins</option>
                             </select>";

            return MvcHtmlString.Create(select);
        }

        public static string GetString(this object obj, string replace = null, string newChar = null)
        {
            if(obj == null)
            {
                return "";
            }

            if(replace == null || newChar == null)
            {
                return obj.ToString();
            }

            return obj.ToString().Replace(replace, newChar);
        }

        public static string Round(this double value)
        {
            return Math.Round(value, 2).ToString();
        }

        public static string GetConcat(this object obj, string valueIfNull,  params string[] concat)
        {
            if (obj == null)
            {
                return valueIfNull;
            }

            if (concat == null)
            {
                return obj.ToString();
            }

            return obj.ToString() + string.Join("", concat);
        }

        private static string GetValue(string value, string model)
        {
            return $"value ='{value}'" + (value == model ? "selected='selected'" : "");
        }
    }
}

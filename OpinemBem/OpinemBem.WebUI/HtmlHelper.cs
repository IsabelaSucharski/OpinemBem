using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace OpinemBem.WebUI
{
    public static class HtmlHelper
    {
        /// <summary>
        /// Binds a new dropdownList based on the enumerable passed
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="listToBind">The list to be bind</param>
        /// <param name="key">The Datasource Key</param>
        /// <param name="value">The Datasource Value</param>
        /// <param name="htmlAttributes"></param>
        /// <param name="showSelect"></param>
        /// <returns></returns>
        /// <example>
        /// @Html.ModelDropDownList((IEnumerable)ViewBag.ListUsuarios, "ID", "Nome")
        /// </example>
        public static HtmlString ModelDropDownList<TModel>(
            this HtmlHelper<TModel> htmlHelper,
            IEnumerable listToBind,
            String key,
            String value,
            object htmlAttributes = null,
            Boolean showSelect = true)
        {
            var returnList = new SelectList(listToBind != null ? listToBind : new List<Object>(), key, value).ToList();

            if (showSelect)
                returnList.Insert(0, new SelectListItem() { Text = "-- SELECIONE --", Value = " " });

            return htmlHelper.DropDownList(" ", returnList, htmlAttributes);
        }

        /// <summary>
        /// Function to generate a radioButton list with enum by its type.
        /// The function will only work correclty if the enum type passed has Descriptrion property.
        /// <example>
        /// public enum Color
        /// {
        ///     [Description("Blue")]
        ///     Blue,
        ///     [Description("Dark Blue")]
        ///     DarkBlue,
        ///     [Description("Acid Green")]
        ///     AcidGreen
        /// }
        /// </example>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="modelExpression"></param>
        /// <returns></returns>
        public static MvcHtmlString EnumRadioButtonFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> modelExpression,
            object htmlAttributes = null)
        {
            //Hold the property type.
            var typeProperty = modelExpression.ReturnType;

            //Check if the type passed is an enum.
            if (!typeProperty.IsEnum)
                throw new ArgumentException(string.Format("Type {0} is not an enum", typeProperty));

            //Get the fields from the type passed
            var properties = typeProperty.GetFields();

            //New instance of a StringBuilder where we will store ours the script to return
            var sb = new StringBuilder();

            //Run the properties items
            foreach (var fieldObject in properties)
            {
                //Gets the list of attributes description which the enum property has.
                var attributes = (DescriptionAttribute[])fieldObject.GetCustomAttributes(typeof(DescriptionAttribute), false);

                //If the attributes lenght <= 0 it means that the enum property doesn't have Description
                if (attributes.Length <= 0) continue;

                //Create the radioButton Html
                var radio = htmlHelper.RadioButtonFor(modelExpression, fieldObject.GetRawConstantValue().ToString(), htmlAttributes).ToHtmlString();

                //Add the radioButton Html + the description name into the stringBuilder.
                //Observation: the class 'labelRadio' is not obrigatory.
                sb.AppendFormat("<label class='labelRadio'>{0} {1} </label>",
                                    radio,
                                    HttpUtility.HtmlEncode(attributes[0].Description)
                );
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// Creates the DropDown List (HTML Select Element) from LINQ 
        /// Expression where the expression returns an Enum type.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null,
            bool showSelect = true,
            bool showOrder = true)
            where TModel : class
        {
            TProperty value = htmlHelper.ViewData.Model == null
                ? default(TProperty)
                : expression.Compile()(htmlHelper.ViewData.Model);
            string selected = value == null ? String.Empty : value.ToString();
            return htmlHelper.DropDownListFor(expression, CreateSelectList(expression.ReturnType, selected, showSelect, showOrder), htmlAttributes);
        }

        /// <summary>
        /// Creates the select list.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="selectedItem">The selected item.</param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> CreateSelectList(Type enumType, string selectedItem, bool showSelect, bool showOrder = true)
        {
            var lst = new List<SelectListItem>();
            if (showSelect) lst.Add(new SelectListItem { Text = "-- [SELECIONE] --", Value = "" });
            lst.AddRange((from object item in Enum.GetValues(enumType)
                          let fi = enumType.GetField(item.ToString())
                          let attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()
                          let title = attribute == null ? item.ToString() : ((DescriptionAttribute)attribute).Description
                          select new SelectListItem
                          {
                              Value = item.ToString(),
                              Text = title,
                              Selected = selectedItem == item.ToString()
                          }).ToList());
            if (showOrder) lst = lst.OrderBy(o => o.Value).ToList();
            return lst;
        }
    }
}
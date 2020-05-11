using System.Text;
using System.Web.Mvc;

namespace Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString RequireJs(this HtmlHelper htmlHelper, string module)
        {
            var require = new StringBuilder();

            require.AppendLine("<script>");
            require.AppendLine("require( [\"" + module + "\"] );");
            require.AppendLine("</script>");

            return new MvcHtmlString(require.ToString());
        }
    }
}
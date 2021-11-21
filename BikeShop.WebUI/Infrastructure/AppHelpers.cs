using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Infrastructure
{
    public static class AppHelpers
    {
        public static HtmlString PageLinks(this IHtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            TagBuilder outer = new TagBuilder("div");
            outer.AddCssClass("d-flex justify-content-center m-1");
            TagBuilder result = new TagBuilder("div");
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href ", pageUrl(i));
                tag.InnerHtml.Append(i.ToString());
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.InnerHtml.AppendHtml(tag);
            }
            result.AddCssClass("btn-group");
            outer.InnerHtml.AppendHtml(result);
            var writer = new System.IO.StringWriter();
            outer.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}

using System;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNetCore.Html;
using WEB.Models;

namespace WEB.Helpers
{
    public static class PagingHelpers
    {
        public static HtmlString PageLinks(PageViewModel pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");

                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }

                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            HtmlString htmlString = new HtmlString(result.ToString());

            return htmlString;
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Helper
{
    public class PaginationTagHelper : TagHelper
    {
        public int TotalPage { get; set; }
        public string Url { get; set; }
        public object CurrentPage { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination mx-auto");
            StringBuilder sb = new StringBuilder();

            if (CurrentPage == null)
            {
                string uri = string.Format(Url, "");
                sb.AppendFormat("<li class=\"page-item active\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", 1, uri);
                for (int i = 2; i <= 5; i++)
                {
                    uri = string.Format(Url, i);
                    sb.AppendFormat("<li class=\"page-item\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", i, uri);
                }

                sb.AppendFormat("<button class=\"page-link\">...</button>");
                uri = String.Format(Url, TotalPage);
                sb.AppendFormat("<li class=\"page-item\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", TotalPage, uri);
            }
            else
            {
                int currentPage = Convert.ToInt32(CurrentPage);
                string uri = string.Format(Url, "");
                sb.AppendFormat("<li class=\"page-item\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", 1, uri);
                if (currentPage >= 5)
                {
                    sb.AppendFormat("<button class=\"page-link\">...</button>");
                }
                for (int i = currentPage - 2; i <= currentPage + 4 && i <= TotalPage; i++)
                {
                    if (i == 0 || i == 1 || i == currentPage - 1 && currentPage == 2)
                    {
                        continue;
                    }
                    uri = string.Format(Url, i);
                    if (i == currentPage)
                    {
                        sb.AppendFormat("<li class=\"page-item active\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", i, uri);
                    }
                    else
                    {
                        sb.AppendFormat("<li class=\"page-item\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", i, uri);
                    }
                }
                if (currentPage <= TotalPage - 6)
                {
                    sb.AppendFormat("<button class=\"page-link\">...</button>");
                }
                if (currentPage + 4 < TotalPage)
                {
                    uri = string.Format(Url, TotalPage);
                    sb.AppendFormat("<li class=\"page-item\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", TotalPage, uri);
                }                
            }            
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}

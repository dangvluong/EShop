using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text;

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

            if (TotalPage <= 5)
            {
                if (CurrentPage is null)
                {
                    AddPageItem(sb, isActive: true);
                    AddMultiPageItem(sb, 1, 2, TotalPage);                    
                    if (TotalPage > 1)                    
                        AddNavigation(sb, 1);                    
                }
                else
                {
                    int currentPage = Convert.ToInt32(CurrentPage);
                    AddNavigation(sb, currentPage, true);
                    AddPageItem(sb);
                    AddMultiPageItem(sb, currentPage, 2, 5);
                    if (currentPage < TotalPage)                    
                        AddNavigation(sb, currentPage);
                    
                }
            }
            else
            {
                if (CurrentPage is null)
                {
                    AddPageItem(sb, isActive: true);
                    AddMultiPageItem(sb, 1, 2, 5);
                    AddNavigation(sb, 1);
                }
                else
                {
                    int currentPage = Convert.ToInt32(CurrentPage);
                    int j = currentPage;
                    if (currentPage - 1 == 0)                    
                        j += 2;                    
                    if (currentPage - 2 == 0)                    
                        j++;                    
                    if (TotalPage - currentPage == 1)                    
                        j -= 1;                    
                    if (TotalPage - currentPage == 0)                    
                        j -= 2;                    
                    if (currentPage > 1)                    
                        AddNavigation(sb, currentPage, true);                    
                    AddMultiPageItem(sb, currentPage, j - 2, j + 2);                    
                    if (currentPage < TotalPage)                    
                        AddNavigation(sb, currentPage);                    
                }
            }
            output.Content.SetHtmlContent(sb.ToString());
        }

        private void AddMultiPageItem(StringBuilder sb, int currentPage, int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                if (i == currentPage)                
                    AddPageItem(sb, i, true);                
                else                
                    AddPageItem(sb, i);                
            }
        }

        private void AddPageItem(StringBuilder sb, int? i = null, bool isActive = false)
        {
            var uri = string.Format(Url, i);
            var active = "";
            if (i is null)
                i = 1;
            if (isActive)
                active = "active";
            sb.AppendFormat("<li class=\"page-item {2}\"><a href=\"{1}\" class=\"page-link\">{0}</a></li>", i, uri, active);
        }
        private void AddNavigation(StringBuilder sb, int currentPage, bool backward = false)
        {
            int i = currentPage + 1;
            string iconClass = "fas fa-fast-forward";
            if (backward)
            {
                i = currentPage - 1;
                iconClass = "fas fa-fast-backward";
            }
            var uri = string.Format(Url, i);
            sb.AppendFormat($"<li class=\"page-item\"><a href=\"{uri}\" class=\"page-link\"><i class=\"{iconClass}\"></i></a></li>");
        }
    }
}

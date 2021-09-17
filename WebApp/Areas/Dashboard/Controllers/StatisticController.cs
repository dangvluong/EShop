using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class StatisticController : Controller
    {
        SiteProvider provider;
        public StatisticController(IConfiguration configuration)
        {
            provider = new SiteProvider(configuration);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetBestSellingProducts()
        {
            return Json(provider.Product.GetBestSellingProduct());
        }
        [HttpPost]
        public IActionResult GetRevenueRatioBySize()
        {
            IEnumerable<Statistic> top5Size = provider.Size.GetBestSellingSize();
            int totalRevenue = provider.InvoiceDetail.GetTotalRevenue();
            List<object> sizeRatio = new List<object>();
            int totalRevenueOfTop5Size = 0;
            foreach (var item in top5Size)
            {
                sizeRatio.Add(new
                {
                    Name = item.Name,
                    Ratio = Math.Round(item.Total / (float)totalRevenue, 2)
                });
                totalRevenueOfTop5Size += item.Total;
            }
            sizeRatio.Add(new
            {
                Name = "Other",
                Ratio = Math.Round((totalRevenue - totalRevenueOfTop5Size) / (float)totalRevenue, 2)
            });
            return Json(sizeRatio);
        }
        [HttpPost]
        public IActionResult GetRenvenueByMonths()
        {
            IEnumerable<Statistic> dataFromDb = provider.InvoiceDetail.GetRevenueByMonths();
            Dictionary<short, int> dictRevenue = new Dictionary<short, int>();
            foreach (Statistic item in dataFromDb)
            {
                dictRevenue.Add(item.Id, item.Total);
            }
            List<Statistic> revenueByMonths = new List<Statistic>();
            for (short i = 1; i <= 12; i++)
            {
                if (dictRevenue.ContainsKey(i))
                {
                    revenueByMonths.Add(new Statistic
                    {
                        Id = i,
                        Total = dictRevenue[i]
                    });
                }
                else
                {
                    revenueByMonths.Add(new Statistic
                    {
                        Id = i,
                        Total = 0
                    });
                }
            }
            return Json(revenueByMonths);
        }
        [HttpPost]
        public IActionResult GetTop5HighestInventoryProducts()
        {
            return Json(provider.Product.GetTop5HighestInventoryProducts());
        }        
    }
}

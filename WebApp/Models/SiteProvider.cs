using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SiteProvider:BaseProvider
    {
        public SiteProvider(IConfiguration configuration) : base(configuration) { }
        ProductRepository product;
        ProductImageRepository productImage;
        ColorRepository color;
        CategoryRepository category;
        SizeRepository size;
        InventoryStatusRepository inventoryStatus;
        GuideRepository guide;

        public ProductRepository Product
        {
            get
            {
                if(product is null)
                {
                    product = new ProductRepository(Connection);
                }
                return product;
            }
        }

        public ProductImageRepository ProductImage
        {
            get
            {
                if(productImage is null)
                {
                    productImage = new ProductImageRepository(Connection);
                }
                return productImage;
            }
        }

        public ColorRepository Color
        {
            get
            {
                if (color is null)
                {
                    color = new ColorRepository(Connection);
                }
                return color;
            }
        }
        public CategoryRepository Category
        {
            get
            {
                if (category is null)
                {
                    category = new CategoryRepository(Connection);
                }
                return category;
            }
        }
        public SizeRepository Size
        {
            get
            {
                if (size is null)
                {
                    size = new SizeRepository(Connection);
                }
                return size;
            }
        }
        public InventoryStatusRepository InventoryStatus
        {
            get
            {
                if (inventoryStatus is null)
                {
                    inventoryStatus = new InventoryStatusRepository(Connection);
                }
                return inventoryStatus;
            }
        }
        public GuideRepository Guide
        {
            get
            {
                if (guide is null)
                {
                    guide = new GuideRepository(Connection);
                }
                return guide;
            }
        }


    }
}

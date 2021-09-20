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
        ImageOfProductRepository productImage;
        ColorRepository color;
        CategoryRepository category;
        SizeRepository size;
        InventoryQuantityRepository inventoryQuantity;
        GuideRepository guide;
        MemberRepository member;
        RoleRepository role;
        ProvinceRepository province;
        DistrictRepository district;
        WardRepository ward;
        ContactRepository contact;
        CartRepository cart;
        InvoiceRepository invoice;
        InvoiceDetailRepository invoiceDetail;
        MemberInRoleRepository memberInRole;
        ProductInCategoryRepository productCategory;
        ColorOfProductRepository productColor;
        GuideOfProductRepository productGuide;
        SizeOfProductRepository productSize;
        public SizeOfProductRepository ProductSize
        {
            get
            {
                if(productSize is null)
                {
                    productSize = new SizeOfProductRepository(Connection);
                }
                return productSize;
            }
        }
        public GuideOfProductRepository ProductGuide
        {
            get
            {
                if(productGuide is null)
                {
                    productGuide = new GuideOfProductRepository(Connection);
                }
                return productGuide;
            }
        }
        public ColorOfProductRepository ProductColor
        {
            get
            {
                if(productColor is null)
                {
                    productColor = new ColorOfProductRepository(Connection);
                }
                return productColor;
            }
        }
        public ProductInCategoryRepository ProductCategory
        {
            get
            {
                if (productCategory is null)
                {
                    productCategory = new ProductInCategoryRepository(Connection);
                }
                return productCategory;
            }
        }
        public MemberInRoleRepository MemberInRole
        {
            get
            {
                if(memberInRole is null)
                {
                    memberInRole = new MemberInRoleRepository(Connection);
                }
                return memberInRole;
            }
        }
        public InvoiceDetailRepository InvoiceDetail
        {
            get
            {
                if(invoiceDetail is null)
                {
                    invoiceDetail = new InvoiceDetailRepository(Connection);
                }
                return invoiceDetail;
            }
        }
        public InvoiceRepository Invoice
        {
            get
            {
                if(invoice is null)
                {
                    invoice = new InvoiceRepository(Connection);
                }
                return invoice;
            }
        }
        public CartRepository Cart
        {
            get
            {
                if(cart is null)
                {
                    cart = new CartRepository(Connection);
                }
                return cart;
            }
        }
        public ContactRepository Contact
        {
            get
            {
                if(contact is null)
                {
                    contact = new ContactRepository(Connection);
                }
                return contact;
            }
        }
        public WardRepository Ward
        {
            get
            {
                if (ward is null)
                {
                    ward = new WardRepository(Connection);
                }
                return ward;
            }
        }

        public DistrictRepository District
        {
            get
            {
                if (district is null)
                {
                    district = new DistrictRepository(Connection);
                }
                return district;
            }
        }
        public ProvinceRepository Province
        {
            get
            {
                if(province is null)
                {
                    province = new ProvinceRepository(Connection);
                }
                return province;
            }
        }


        public RoleRepository Role
        {
            get
            {
                if(role is null)
                {
                    role = new RoleRepository(Connection);
                }
                return role;
            }
        }
        public MemberRepository Member
        {
            get
            {
                if(member is null)
                {
                    member = new MemberRepository(Connection);
                }
                return member;
            }
        }
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

        public ImageOfProductRepository ImageOfProduct
        {
            get
            {
                if(productImage is null)
                {
                    productImage = new ImageOfProductRepository(Connection);
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
        public InventoryQuantityRepository InventoryQuantity
        {
            get
            {
                if (inventoryQuantity is null)
                {
                    inventoryQuantity = new InventoryQuantityRepository(Connection);
                }
                return inventoryQuantity;
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

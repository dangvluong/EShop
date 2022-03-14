using Microsoft.Extensions.Configuration;
using WebApp.Interfaces;
using WebApp.Repositories;

namespace WebApp.Repositories
{
    public class RepositoryManager:RepositoryManagerBase,IRepositoryManager
    {
        public RepositoryManager(IConfiguration configuration) : base(configuration) { }
        IProductRepository product;
        IImageOfProductRepository imageOfProduct;
        IColorRepository color;
        ICategoryRepository category;
        ISizeRepository size;
        IInventoryQuantityRepository inventoryQuantity;
        IGuideRepository guide;
        IMemberRepository member;
        IRoleRepository role;
        IProvinceRepository province;
        IDistrictRepository district;
        IWardRepository ward;
        IContactRepository contact;
        ICartRepository cart;
        IInvoiceRepository invoice;
        IInvoiceDetailRepository invoiceDetail;
        IMemberInRoleRepository memberInRole;
        IProductInCategoryRepository productInCategory;
        IColorOfProductRepository colorOfProduct;
        IGuideOfProductRepository guideOfProduct;
        ISizeOfProductRepository sizeOfProduct;
        public ISizeOfProductRepository SizeOfProduct
        {
            get
            {
                if(sizeOfProduct is null)
                {
                    sizeOfProduct = new SizeOfProductRepository(Connection);
                }
                return sizeOfProduct;
            }
        }
        public IGuideOfProductRepository GuideOfProduct
        {
            get
            {
                if(guideOfProduct is null)
                {
                    guideOfProduct = new GuideOfProductRepository(Connection);
                }
                return guideOfProduct;
            }
        }
        public IColorOfProductRepository ColorOfProduct
        {
            get
            {
                if(colorOfProduct is null)
                {
                    colorOfProduct = new ColorOfProductRepository(Connection);
                }
                return colorOfProduct;
            }
        }
        public IProductInCategoryRepository ProductInCategory
        {
            get
            {
                if (productInCategory is null)
                {
                    productInCategory = new ProductInCategoryRepository(Connection);
                }
                return productInCategory;
            }
        }
        public IMemberInRoleRepository MemberInRole
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
        public IInvoiceDetailRepository InvoiceDetail
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
        public IInvoiceRepository Invoice
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
        public ICartRepository Cart
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
        public IContactRepository Contact
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
        public IWardRepository Ward
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

        public IDistrictRepository District
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
        public IProvinceRepository Province
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


        public IRoleRepository Role
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
        public IMemberRepository Member
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
        public IProductRepository Product
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

        public IImageOfProductRepository ImageOfProduct
        {
            get
            {
                if(imageOfProduct is null)
                {
                    imageOfProduct = new ImageOfProductRepository(Connection);
                }
                return imageOfProduct;
            }
        }

        public IColorRepository Color
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
        public ICategoryRepository Category
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
        public ISizeRepository Size
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
        public IInventoryQuantityRepository InventoryQuantity
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
        public IGuideRepository Guide
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

using WebApp.Repositories;

namespace WebApp.Interfaces
{
    public interface IRepositoryManager
    {
        ISizeOfProductRepository SizeOfProduct { get; }
        IGuideOfProductRepository GuideOfProduct { get; }
        IColorOfProductRepository ColorOfProduct { get; }
        IProductInCategoryRepository ProductInCategory { get; }
        IMemberInRoleRepository MemberInRole { get; }
        IInvoiceDetailRepository InvoiceDetail { get; }
        IInvoiceRepository Invoice { get; }
        ICartRepository Cart { get; }
        IContactRepository Contact { get; }
        IWardRepository Ward { get; }

        IDistrictRepository District { get; }
        IProvinceRepository Province { get; }


        IRoleRepository Role { get; }
        IMemberRepository Member { get; }
        IProductRepository Product { get; }

        IImageOfProductRepository ImageOfProduct { get; }

        IColorRepository Color { get; }
        ICategoryRepository Category { get; }
        ISizeRepository Size { get; }
        IInventoryQuantityRepository InventoryQuantity { get; }
        IGuideRepository Guide { get; }
    }
}

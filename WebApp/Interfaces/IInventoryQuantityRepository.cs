using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IInventoryQuantityRepository
    {
        IEnumerable<InventoryQuantity> GetInventoryQuantitiesByProduct(short productId);
        int GetInventoryQuantityByProductColorAndSize(short productId, short colorId, short sizeId);
        int UpdateInventoryQuantityFromInvoice(InvoiceDetail obj);
        int UpdateInventoryQuantity(List<InventoryQuantity> list);
    }
}

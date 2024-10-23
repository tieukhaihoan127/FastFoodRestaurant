using FastFoodRestaurant.Models;

namespace FastFoodRestaurant.Repository.IGenericRepository
{
    public interface IVoucherRepository : IGenericRepository<Voucher>
    {
        void Update(Voucher obj);
        void Save();
    }
}

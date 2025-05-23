using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodRestaurant.Areas.Client.Controllers
{
    [Area("Client")]
    public class CartController : Controller
    {
        private readonly IComboRepository _comboRepo;
        private readonly IMenuRepository _menuRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IBillRepository _billRepo;
        private List<CartItem> cartList = new List<CartItem>();

        public CartController(IComboRepository comboRepo, IMenuRepository menuRepo, IStoreRepository storeRepo, ICartItemRepository cartItemRepo, IBillRepository billRepo)
        {
            _comboRepo = comboRepo;
            _menuRepo = menuRepo;
            _storeRepo = storeRepo;
            _cartItemRepo = cartItemRepo;
            _billRepo = billRepo;
        }
        public IActionResult Index()
        {
            List<Combo> comboList;
            List<Menu> menuList;
            List<Store> storeRepo;
            List<CartItem> cartItemList;

            comboList = _comboRepo.GetAll().ToList();
            menuList = _menuRepo.GetIncludeCategoryAll().ToList();
            cartItemList = _cartItemRepo.GetIncludeCategoryAll().ToList();
            Store selectedStore = _storeRepo.getSingleStore(s => s.StoreId == "ST0002");
            var totalPricePaid = 0.0;
            foreach (var item in cartItemList) {
                totalPricePaid += item.Price;
            }
            ViewData["ToBuyList"] = comboList;
            ViewData["SuggestList"] = menuList;
            ViewData["SelectedStore"] = selectedStore;
            ViewData["CartList"] = cartItemList;
            ViewData["TotalPrice"] = totalPricePaid;
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(string id)
        {
            Menu single = _menuRepo.GetSingleMenuWithCategory(m => m.MenuId == id);
            var check = _cartItemRepo.GetSingleMenuWithCategory(c => c.MenuId == id);
            if(check == null)
            {
                _cartItemRepo.Add(new CartItem()
                {
                    CartId = single.MenuId,
                    MenuId = single.MenuId,
                    Name = single.Name,
                    Quantity = 1,
                    Price = single.Price
                });
            }
            else
            {
                check.Quantity = check.Quantity + 1;
                check.Price = check.Price + check.Menu.Price;
            }
            
            _cartItemRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCartPlus(string id)
        {
            var check = _cartItemRepo.GetSingleMenuWithCategory(c => c.MenuId == id);
            check.Quantity = check.Quantity + 1;
            check.Price = check.Price + check.Menu.Price;

            _cartItemRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCartMinus(string id)
        {
            var check = _cartItemRepo.GetSingleMenuWithCategory(c => c.MenuId == id);
            if(check.Quantity <= 1)
            {
                _cartItemRepo.Remove(check);
            }
            else
            {
                check.Quantity = check.Quantity - 1;
                check.Price = check.Price - check.Menu.Price;
            }

            _cartItemRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCartItem(string id)
        {
            var check = _cartItemRepo.GetSingleMenuWithCategory(c => c.MenuId == id);
            _cartItemRepo.Remove(check);

            _cartItemRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateBill(String? ClientName, String? ClientPhoneNumber, String? ClientEmail, String? Note, double TotalPrice)
        {
            var currentId = _billRepo.getCurrentId(b => b.BillId);
            var cartItemList = _cartItemRepo.GetAll().ToList();
            string numberExtract = currentId.BillId.Trim().Substring(1);
            int num = int.Parse(numberExtract);
            num += 1;
            string newId = "";

            if (num < 10)
            {
                newId = "B0000000000" + num;
            }
            else if (num < 100)
            {
                newId = "B000000000" + num;
            }
            else if (num < 1000)
            {
                newId = "B00000000" + num;
            }
            else if (num < 10000)
            {
                newId = "B0000000" + num;
            }
            else if (num < 100000)
            {
                newId = "B000000" + num;
            }
            else if (num < 1000000)
            {
                newId = "B00000" + num;
            }
            else if (num < 10000000)
            {
                newId = "B0000" + num;
            }
            else if (num < 100000000)
            {
                newId = "B000" + num;
            }
            else if (num < 1000000000)
            {
                newId = "B00" + num;
            }
            else if(num < 10000000000)
            {
                newId = "B0" + num;
            }
            else
            {
                newId = "B" + num;
            }

            _billRepo.Add(new Bill() { 
                BillId = newId,
                ClientName = ClientName,
                ClientPhoneNumber = ClientPhoneNumber,
                ClientEmail = ClientEmail,
                TotalPrice = TotalPrice,
                Note = Note,
                Status = 1,
                ShippingDate = DateTime.Now,
                PaymentMethod = "COD",
                UserId = "SU0000000002        ",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                DeleteDate = DateTime.Now,
                PaymentStatus = false
            });
            _billRepo.Save();

            if(cartItemList == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var obj in cartItemList)
                {
                    _cartItemRepo.Remove(obj);
                    _cartItemRepo.Save();
                }
            }
            return RedirectToAction("Index");
        }

    }
}

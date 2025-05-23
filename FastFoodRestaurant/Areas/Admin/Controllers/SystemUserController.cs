using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FastFoodRestaurant.DTO;
using FastFoodRestaurant.Models;
using FastFoodRestaurant.Repository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FastFoodRestaurant.Controllers
{
    [Area("Admin")]
    public class SystemUserController : Controller
    {
        private readonly ISystemUserRepository _systemUserRepo;
        private readonly IStoreRepository _storeRepo;
        private const int pageSize = 4;
        private readonly Cloudinary _cloudinary;

        public SystemUserController(ISystemUserRepository systemUserRepo, IStoreRepository storeRepo)
        {
            _systemUserRepo = systemUserRepo;
            _storeRepo = storeRepo;
            var account = new Account("dwdhkwu0r", "182449369914218", "d1gJYHWsz6Rz9wFmFD9S6kYtka4");
            _cloudinary = new Cloudinary(account);
        }

        public IActionResult Index(int pageNumber = 1)
        {
            List<SystemUser> usersList;

            usersList = _systemUserRepo.GetAllExpression(u => u.IsDeleted == false).ToList();

            var pagedUsersList = usersList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in pagedUsersList)
            {
                obj.UserId = obj.UserId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(usersList.Count() / (double)pageSize);

            return View(pagedUsersList);
        }

        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(sha.ComputeHash(bytes));
            }
        }

        [HttpPost]
        public IActionResult Index(string? object_id, string? object_name,Boolean? status, int pageNumber = 1)
        {
            List<SystemUser> usersList;

            if (!string.IsNullOrEmpty(object_id) && !string.IsNullOrEmpty(object_name))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.UserId == object_id && u.PhoneNumber == object_name && u.IsDeleted == false).ToList();
            }
            else if (!string.IsNullOrEmpty(object_id))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.UserId == object_id && u.IsDeleted == false).ToList();
            }
            else if (!string.IsNullOrEmpty(object_name))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.PhoneNumber == object_name && u.IsDeleted == false).ToList();
            }
            else if (status.HasValue)
            {
                if (status == false)
                {
                    usersList = _systemUserRepo.GetAllExpression(v => v.Status == false).ToList();
                }
                else
                {
                    usersList = _systemUserRepo.GetAllExpression(v => v.Status == true).ToList();
                }
            }
            else
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.IsDeleted == false).ToList();
            }

            var pagedUsersList = usersList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in pagedUsersList)
            {
                obj.UserId = obj.UserId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(usersList.Count() / (double)pageSize);

            return View(pagedUsersList);
        }

        public IActionResult RecycleBin(int pageNumber = 1)
        {
            List<SystemUser> usersList;

            usersList = _systemUserRepo.GetAllExpression(u => u.IsDeleted == true).ToList();

            var pagedUsersList = usersList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in pagedUsersList)
            {
                obj.UserId = obj.UserId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(usersList.Count() / (double)pageSize);

            return View(pagedUsersList);
        }

        [HttpPost]
        public IActionResult RecycleBin(string? object_id, string? object_name, int pageNumber = 1)
        {
            List<SystemUser> usersList;

            if (!string.IsNullOrEmpty(object_id) && !string.IsNullOrEmpty(object_name))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.UserId == object_id && u.PhoneNumber == object_name && u.IsDeleted == true).ToList();
            }
            else if (!string.IsNullOrEmpty(object_id))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.UserId == object_id && u.IsDeleted == true).ToList();
            }
            else if (!string.IsNullOrEmpty(object_name))
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.PhoneNumber == object_name && u.IsDeleted == true).ToList();
            }
            else
            {
                usersList = _systemUserRepo.GetAllExpression(u => u.IsDeleted == true).ToList();
            }

            var pagedUsersList = usersList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var obj in pagedUsersList)
            {
                obj.UserId = obj.UserId.Trim();
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling(usersList.Count() / (double)pageSize);

            return View(pagedUsersList);
        }

        public IActionResult Create()
        {
            var storesList = _storeRepo.GetAllIds(s => new StoreIdName
            {
                StoreId = s.StoreId,
                Name = s.Name
            });

            var currentId = _systemUserRepo.getCurrentId(u => u.UserId);
            string numberExtract = currentId.UserId.Trim().Substring(2);
            int num = int.Parse(numberExtract);
            num += 1;
            string newId = "";

            if (num < 10)
            {
                newId = "SU000000000" + num;
            }
            else if (num < 100)
            {
                newId = "SU00000000" + num;
            }
            else if (num < 1000)
            {
                newId = "SU0000000" + num;
            }
            else if (num < 10000)
            {
                newId = "SU000000" + num;
            }
            else if (num < 100000)
            {
                newId = "SU00000" + num;
            }
            else if (num < 1000000)
            {
                newId = "SU0000" + num;
            }
            else if (num < 10000000)
            {
                newId = "SU000" + num;
            }
            else if (num < 100000000)
            {
                newId = "SU00" + num;
            }
            else if (num < 1000000000)
            {
                newId = "SU0" + num;
            }
            else
            {
                newId = "SU" + num;
            }

            ViewData["CurrentId"] = newId;
            ViewData["StoresList"] = storesList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(SystemUser obj, IFormFile pictureFile)
        {
            List<SystemUser> usersList = _systemUserRepo.GetAllExpression(u => u.IsDeleted == false).ToList();

            if (pictureFile != null && pictureFile.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, pictureFile.OpenReadStream()),
                    PublicId = obj.UserId.Trim()
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                obj.PictureUrl = uploadResult.Url.ToString();
            }

            obj.CreatedDate = DateTime.Now;
            obj.UpdatedDate = null;
            obj.DeleteDate = null;
            obj.Status = true;
            obj.IsDeleted = false;
            obj.IsLocked = false;
            obj.Password = HashPassword(obj.PhoneNumber);

            _systemUserRepo.Add(obj);
            _systemUserRepo.Save();
            int cnt = usersList.Count() + 1;
            int totalPages = (int)Math.Ceiling(cnt / (double)pageSize);
            return RedirectToAction("Index", new { pageNumber = totalPages });
        }

        public IActionResult Edit(string id)
        {
            var storesList = _storeRepo.GetAllIds(s => new StoreIdName
            {
                StoreId = s.StoreId,
                Name = s.Name
            });
          
            var userItem = _systemUserRepo.Get(u => u.UserId == id);

            ViewData["StoresList"] = storesList;
            return View(userItem);
        }

        [HttpPost]
        public IActionResult Edit(SystemUser obj, IFormFile? pictureFile)
        {
            var existingCategory = _systemUserRepo.Get(u => u.UserId == obj.UserId);

            if (existingCategory == null)
            {
                return NotFound();
            }

            if (pictureFile != null && pictureFile.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(pictureFile.FileName, pictureFile.OpenReadStream()),
                    PublicId = obj.UserId.Trim()
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    existingCategory.PictureUrl = uploadResult.Url.ToString();
                }
                else
                {
                    ModelState.AddModelError("", "Upload hình ảnh không thành công.");
                    return RedirectToAction("Index");
                }
            }

            existingCategory.UpdatedDate = DateTime.Now;

            if (obj.Name != null)
            {
                existingCategory.Name = obj.Name;
            }

            if (obj.PhoneNumber != null)
            {
                existingCategory.PhoneNumber = obj.PhoneNumber;
            }

            if (obj.Email != null)
            {
                existingCategory.Email = obj.Email;
            }

            if (obj.DOB != null)
            {
                existingCategory.DOB = obj.DOB;
            }

            if (obj.Gender != null)
            {
                existingCategory.Gender = obj.Gender;
            }

            if (obj.Role != null)
            {
                existingCategory.Role = obj.Role;
            }

            if (obj.StoreId != null)
            {
                existingCategory.StoreId = obj.StoreId;
            }

            if (obj.IsLocked != null)
            {
                existingCategory.IsLocked = obj.IsLocked;
            }

            _systemUserRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var userItem = _systemUserRepo.Get(u => u.UserId == id);

            if (userItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                userItem.IsDeleted = true;
                _systemUserRepo.Save();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
            }
        }

        [HttpPost]
        public IActionResult Reload(string id)
        {
            var userItem = _systemUserRepo.Get(u => u.UserId == id);

            if (userItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                userItem.IsDeleted = false;
                _systemUserRepo.Save();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
            }
        }

        [HttpPost]
        public IActionResult ChangeStatus(string id)
        {
            var userItem = _systemUserRepo.Get(u => u.UserId == id);

            if (userItem == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                if(userItem.IsLocked == true)
                {
                    userItem.IsLocked = false;
                }
                else
                {
                    userItem.IsLocked = true;
                }
                _systemUserRepo.Save();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
            }
        }

        [HttpPost]
        public IActionResult DeleteMulti(string[] idArr)
        {
            if (idArr.Length > 0)
            {
                string[] splitId = idArr[0].Split(',');
                foreach (string id in splitId)
                {
                    var userItem = _systemUserRepo.Get(u => u.UserId == id);

                    if (userItem == null)
                    {
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        userItem.IsDeleted = true;
                        _systemUserRepo.Save();
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                        return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng chọn danh mục để thao tác");
                return StatusCode(400, new { message = "Vui lòng chọn danh mục để thao tác." });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ReloadMulti(string[] idArr)
        {
            if (idArr.Length > 0)
            {
                string[] splitId = idArr[0].Split(',');
                foreach (string id in splitId)
                {
                    var userItem = _systemUserRepo.Get(u => u.UserId == id);

                    if (userItem == null)
                    {
                        return RedirectToAction("Index");
                    }

                    try
                    {
                        userItem.IsDeleted = false;
                        _systemUserRepo.Save();
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác.");
                        return StatusCode(400, new { message = "Không thể xóa danh mục này vì nó đang được sử dụng ở nơi khác." });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng chọn danh mục để thao tác");
                return StatusCode(400, new { message = "Vui lòng chọn danh mục để thao tác." });
            }

            return RedirectToAction("Index");
        }
    }
}

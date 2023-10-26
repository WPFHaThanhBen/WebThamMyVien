using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Repository;

namespace WebThamMyVien.Controllers
{
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeRepository _accountTypeRepository;

        public AccountTypeController(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["AllAccountType"] = await _accountTypeRepository.GetAllAccountType();
            return View();
        }

    }
}

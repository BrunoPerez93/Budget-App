using BudgetApp.Models;
using BudgetApp.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BudgetApp.Controllers
{
    public class TypesAccountController : Controller
    {
        private readonly IRespositoryTypesAccount respositoryTypesAccount;

        public TypesAccountController(IRespositoryTypesAccount respositoryTypesAccount)
        {
            this.respositoryTypesAccount = respositoryTypesAccount;
        }

        public async Task<IActionResult> Index()
        {
            var userId = 1;
            var accountType = await respositoryTypesAccount.ObtainAccount(userId);
            return View(accountType);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeAccount typeAccount)
        {
            if (!ModelState.IsValid)
            {
                return View(typeAccount);
            }
            typeAccount.UserId = 1;

            var alreadyExist = await respositoryTypesAccount.Exist(typeAccount.Name, typeAccount.UserId);
            if (alreadyExist)
            {
                ModelState.AddModelError(nameof(typeAccount.Name),
                    $"The name {typeAccount.Name} already exist");
                return View(typeAccount);
            }

            await respositoryTypesAccount.Create(typeAccount);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerifyExistAccountType(string name)
        {
            var userId = 1;
            var alreadyExist = await respositoryTypesAccount.Exist(name, userId);

            if (alreadyExist)
            {
                return Json($"The name {name} already exist");
            }

            return Json(true);
        }
    }
}

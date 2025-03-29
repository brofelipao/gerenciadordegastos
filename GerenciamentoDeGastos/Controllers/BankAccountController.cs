using GerenciamentoDeGastos.Application.Services;
using GerenciamentoDeGastos.Application.ViewModels;
using GerenciamentoDeGastos.MVC.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeGastos.MVC.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class BankAccountController : GenericController
    {
        BankAccountService _bankAccountService;
        public BankAccountController(BankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }
        public IActionResult Index()
        {
            return View(_bankAccountService.GetBankAccountsByPersonId(UserViewModel.PersonId));
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Save(BankAccountViewModel bankAccount)
        {
            try
            {
                await _bankAccountService.Save(bankAccount, UserViewModel.PersonId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var ba = await _bankAccountService.Get(id);
            return View(ba);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ba = await _bankAccountService.Get(id);
            return View(ba);
        }
    }
}

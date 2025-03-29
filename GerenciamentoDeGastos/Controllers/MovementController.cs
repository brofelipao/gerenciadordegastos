using GerenciamentoDeGastos.Application.Services;
using GerenciamentoDeGastos.Application.ViewModels;
using GerenciamentoDeGastos.MVC.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeGastos.MVC.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class MovementController : GenericController
    {
        MovementService _movementService;
        public MovementController(MovementService movementService) 
        {
            _movementService = movementService;
        }

        public async Task<IActionResult> Index(int BankAccountId)
        {
            var movements = await _movementService.GetMovementsByBankAndPerson(BankAccountId, UserViewModel.PersonId);
            return View(movements);
        }

        public IActionResult Create(int BankAccountId)
        {
            return View(new MovementViewModel() { BankAccountId = BankAccountId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movement = await _movementService.Get(id);

            if (movement.DateInvoiced.HasValue)
                return View("Details", movement);

            return View(movement);
        }

        public async Task<IActionResult> Settle(int MovementId, DateTime InvoicedDate)
        {
            try
            {
                await _movementService.Settle(MovementId, InvoicedDate);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Save(MovementViewModel movement)
        {
            try
            {
                await _movementService.Save(movement, UserViewModel.PersonId);

                return RedirectToAction("Index", new { BankAccountId = movement.BankAccountId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

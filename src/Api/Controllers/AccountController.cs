using Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDtoRequest accountDto)
        {
            var account = await accountService.CreateAsync(accountDto.Name);
            return CreatedAtAction(nameof(GetAllAccounts), new { id = account.Id }, account);
        }
    }
}

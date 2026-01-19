using Api.DTOs.Account;
using Domain;
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
        public async Task<ActionResult<IReadOnlyList<AccountResponse>>> List()
        {
            var items = await accountService.GetAllAccountsAsync();
            return Ok(items.Select(a => a.ToResponse()).ToList());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AccountResponse>> GetById(Guid id)
        {
            var account = await accountService.GetByIdAsync(id);
            if (account is null) return NotFound();
            return Ok(account.ToResponse());
        }

        [HttpPost]
        public async Task<ActionResult<AccountResponse>> Create([FromBody] CreateAccountRequest req)
        {
            var created = await accountService.CreateAsync(req.Name);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }
    }
}

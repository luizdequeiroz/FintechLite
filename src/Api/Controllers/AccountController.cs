using Api.DTOs.Account;
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
        public async Task<ActionResult<IReadOnlyList<AccountResponse>>> List(CancellationToken cancellationToken)
        {
            var items = await accountService.GetAllAccountsAsync(cancellationToken);
            return Ok(items.Select(a => a.ToResponse()).ToList());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AccountResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var account = await accountService.GetByIdAsync(id, cancellationToken);
            if (account is null) return NotFound();
            return Ok(account.ToResponse());
        }

        [HttpPost]
        public async Task<ActionResult<AccountResponse>> Create([FromBody] CreateAccountRequest req, CancellationToken cancellationToken)
        {
            var created = await accountService.CreateAsync(req.Name, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }
    }
}

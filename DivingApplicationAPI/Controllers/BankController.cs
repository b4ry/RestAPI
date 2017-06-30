using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CrankBankAPI.Entity;
using CrankBankAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using CrankBankAPI.DataTransferObjects;
using CrankBankAPI.Queries;
using CrankBankAPI.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrankBankAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BankController : Controller
    {

        private readonly IAccountQuery _accountSummaryQuery;
        private readonly ICommandBus _commandBus;

        public BankController(IAccountQuery accountSummaryQuery, ICommandBus commandBus)
        {
            _accountSummaryQuery = accountSummaryQuery;
            _commandBus = commandBus;
        }

        /// <summary>
        /// Returns list of account summaries
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountSummaries()
        {
            _commandBus.Send(new AddAccountSummaryCommand { AccountNumber = "11111", Balance = 523, Name = "Command test", Type = AccountTypeEnum.Credit });

            return new JsonResult(await _accountSummaryQuery.GetAccountSummariesAsync());
        }

        /// <summary>
        /// Returns details of an account specified by provided id
        /// </summary>
        /// <param name="id"> Account identification number </param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAccountDetail(string id)
        {
            var summaries = await _accountSummaryQuery.GetAccountSummariesAsync();
            var summary = summaries.SingleOrDefault(a => a.AccountNumber == id);

            var accountDetail = await _accountSummaryQuery.GetAccountDetailByIdAsync(summary.Id);

            var accountDetailDto = Mapper.Map<AccountDetailDto>(accountDetail);

            if (summary == null)
            {
                return NotFound();
            }

            return new JsonResult(accountDetailDto);
        }

    }
}

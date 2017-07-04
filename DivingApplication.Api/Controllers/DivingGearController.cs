using AutoMapper;
using DivingApplication.Api.DataTransferObjects;
using DivingApplication.Entities.Entity;
using DivingApplication.Entities.Enum;
using DivingApplication.Services.CQRS.Commands;
using DivingApplication.Services.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DivingApplication.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DivingGearController : Controller
    {

        private readonly IDivingGearTypeQuery _divingGearTypeQuery;
        private readonly ICommandBus _commandBus;

        public DivingGearController(IDivingGearTypeQuery divingGearTypeQuery, ICommandBus commandBus)
        {
            _divingGearTypeQuery = divingGearTypeQuery;
            _commandBus = commandBus;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetDivingGearTypes()
        {
            //_commandBus.Send(new AddAccountSummaryCommand { AccountNumber = "11111", Balance = 523, Name = "Command test", Type = AccountTypeEnum.Credit });

            return new JsonResult(await _divingGearTypeQuery.GetDivingGearTypesAsync());
        }

        [HttpPost("[action]")]
        public void PostDivingGear([FromBody] DivingGearDto divingGearDto)
        {
            if (ModelState.IsValid)
            {
                DivingGear mappedDivingGear = Mapper.Map<DivingGear>(divingGearDto);

                //_commandBus.Send(new AddDivingGearCommand(mappedDivingGear.Name, mappedDivingGear.DivingGearType));
                
            }
        }

        /// <summary>
        /// Returns details of an account specified by provided id
        /// </summary>
        /// <param name="id"> Account identification number </param>
        /// <returns></returns>
        //[HttpGet("[action]/{id}")]
        //public async Task<IActionResult> GetAccountDetail(string id)
        //{
        //    var summaries = await _divingGearQuery.GetAccountSummariesAsync();
        //    var summary = summaries.SingleOrDefault(a => a.AccountNumber == id);

        //    var accountDetail = await _divingGearQuery.GetAccountDetailByIdAsync(summary.Id);

        //    var accountDetailDto = Mapper.Map<AccountDetailDto>(accountDetail);

        //    if (summary == null)
        //    {
        //        return NotFound();
        //    }

        //    return new JsonResult(accountDetailDto);
        //}

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApplication.Api.CQRS.Queries;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Api.DataTransferObjects.Project;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Controllers
{
    /// <summary>
    /// Controller processing requests for Experience entities. Produces JSON output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ExperienceController : Controller
    {
        private readonly IExperienceQuery _experienceQuery;

        /// <summary>
        /// ExperienceController constructor
        /// </summary>
        /// <param name="experienceQuery"> Query consumed to retrieve Experience entities </param>
        public ExperienceController(
            IExperienceQuery experienceQuery)
        {
            _experienceQuery = experienceQuery;
        }

        /// <summary>
        /// Get endpoint retrieving Experience entity by its id
        /// </summary>
        /// <param name="id"> Identification number of Experience entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> ProjectType in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ExperienceDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundObjectResult))]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetExperienceById([Required]int id)
        {
            var experienceDto = await _experienceQuery.Get(id);

            return new JsonResult(experienceDto);

        }

        /// <summary>
        /// Get endpoint retrieving all Experience entities
        /// </summary>
        /// <returns> Experience collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ProjectTypeDto>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetExperiences()
        {
            var experienceDtos = await _experienceQuery.Get();

            return new JsonResult(experienceDtos);
        }
    }
}

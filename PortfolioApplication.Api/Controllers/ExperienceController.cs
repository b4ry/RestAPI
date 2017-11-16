using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Queries;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
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
        /// GET endpoint retrieving Experience entity by its id
        /// </summary>
        /// <param name="id"> Identification number of Experience entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> Experience entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ExperienceDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundObjectResult))]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetExperienceById([Required]int id)
        {
            Func<DbSet<ExperienceEntity>, Task<ExperienceEntity>> retrivalFunc =
                dbSet => dbSet
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.Technologies)
                .ThenInclude(techs => techs.Technology)
                .ThenInclude(tech => tech.TechnologyType)
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.ProjectType)
                .SingleAsync(exp => exp.Id == id);

            var experienceDto = await _experienceQuery.Get(id, retrivalFunc);

            return new JsonResult(experienceDto);

        }

        /// <summary>
        /// GET endpoint retrieving all Experience entities
        /// </summary>
        /// <returns> Experience entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<ExperienceDto>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetExperiences()
        {
            Func<DbSet<ExperienceEntity>, Task<List<ExperienceEntity>>> retrivalFunc = 
                dbSet => dbSet
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.Technologies)
                .ThenInclude(techs => techs.Technology)
                .ThenInclude(tech => tech.TechnologyType)
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.ProjectType)
                .ToListAsync();

            var experienceDtos = await _experienceQuery.Get(retrivalFunc);

            return new JsonResult(experienceDtos);
        }
    }
}

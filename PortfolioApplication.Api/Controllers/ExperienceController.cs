using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Commands;
using PortfolioApplication.Api.CQRS.Commands.Experiences.Commands;
using PortfolioApplication.Api.CQRS.Queries;
using PortfolioApplication.Entities.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly ICommandBus _commandBus;

        /// <summary>
        /// ExperienceController constructor
        /// </summary>
        /// <param name="experienceQuery"> Query consumed to retrieve Experience entities </param>
        /// <param name="commandBus"> Command bus managing incoming Experience commands </param>
        public ExperienceController(IExperienceQuery experienceQuery, ICommandBus commandBus)
        {
            _experienceQuery = experienceQuery;
            _commandBus = commandBus;
        }

        /// <summary>
        /// Retrieve Experience entity by its id
        /// </summary>
        /// <param name="id"> Identification number of Experience entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> Experience entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entity from database")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Enquired entity does not exist in database")]
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

            var experienceDto = await _experienceQuery.GetAsync(id.ToString(), retrivalFunc);

            return new JsonResult(experienceDto);
        }

        /// <summary>
        /// Retrieve Experience entity by its key (Company name and position)
        /// </summary>
        /// <param name="companyName"> Company name associated with the experience </param>
        /// <param name="position"> Position associate with the experience </param>
        /// <returns> Experience entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entity from database")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Enquired entity does not exist in database")]
        [HttpGet]
        public async Task<IActionResult> GetExperience([Required]string companyName, [Required]string position)
        {
            Func<DbSet<ExperienceEntity>, Task<ExperienceEntity>> retrivalFunc =
                dbSet => dbSet
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.Technologies)
                .ThenInclude(techs => techs.Technology)
                .ThenInclude(tech => tech.TechnologyType)
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.ProjectType)
                .SingleAsync(exp => exp.CompanyName == companyName && exp.Position == position);

            string id = companyName + ":" + position;
            var experienceDto = await _experienceQuery.GetAsync(id, retrivalFunc);

            return new JsonResult(experienceDto);
        }

        /// <summary>
        /// Retrieve all Experience entities
        /// </summary>
        /// <param name="companyName"> Company name parameter to filter results </param>
        /// <returns> Experience entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entities from database")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, description: "Collection of enquired entities is empty")]
        [HttpGet]
        public async Task<IActionResult> GetExperiences([FromQuery]string companyName)
        {
            Func<DbSet<ExperienceEntity>, Task<List<ExperienceEntity>>> retrievalFunc =
                dbSet => dbSet
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.Technologies)
                .ThenInclude(techs => techs.Technology)
                .ThenInclude(tech => tech.TechnologyType)
                .Include(exp => exp.Projects)
                .ThenInclude(proj => proj.ProjectType)
                .ToListAsync();

            if(companyName != string.Empty && companyName != null)
            {
                Func<DbSet<ExperienceEntity>, Task<List<ExperienceEntity>>> whereCondition = 
                    dbset => dbset.Where(exp => exp.CompanyName == companyName).ToListAsync();
                retrievalFunc += whereCondition;
            }

            var experienceDtos = await _experienceQuery.GetAsync(retrievalFunc, companyName);

            return new JsonResult(experienceDtos);
        }

        /// <summary>
        /// Create new Experience entity
        /// </summary>
        /// <param name="createExperienceCommand"> Command containing parameters to create new Experience entity </param>
        /// <returns> JSON containing information about processed command </returns>
        [SwaggerResponse((int)HttpStatusCode.Created, description: "Successfully created new entity in database")]
        [SwaggerResponse((int)HttpStatusCode.NotAcceptable, description: "Provided values are not acceptable, e.g. empty entity")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, description: "Entity already exists in database")]
        [HttpPost]
        public async Task<IActionResult> CreateExperience([FromBody]CreateExperienceCommand createExperienceCommand)
        {
            await _commandBus.SendAsync(createExperienceCommand);

            return new JsonResult($"Processed command '{createExperienceCommand}'.");
        }

        /// <summary>
        /// Delete Experience entity
        /// </summary>
        /// <param name="deleteExperienceCommand"> Command containing parameters to delete Experience entity </param>
        /// <returns> JSON containing information about processed command </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully removed enquired entity from database")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Enquired entity does not exist in database")]
        [HttpDelete]
        public async Task<IActionResult> DeleteExperience([FromBody]DeleteExperienceCommand deleteExperienceCommand)
        {
            Expression<Func<ExperienceEntity, bool>> retrievalFunc = 
                (exp) => exp.CompanyName == deleteExperienceCommand.CompanyName 
                    && exp.Position == deleteExperienceCommand.Position;

            await _commandBus.SendAsync(deleteExperienceCommand, retrievalFunc);

            return new JsonResult($"Processed command '{deleteExperienceCommand}'.");
        }

        /// <summary>
        /// Update Experience entity
        /// </summary>
        /// <param name="updateExperienceCommand"> Command containing parameters to update Experience entity </param>
        /// <returns> JSON containing information about processed command </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully updated enquired entity in database")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Enquired entity does not exist in database")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, description: "Entity already exists in database")]
        [HttpPatch]
        public async Task<IActionResult> UpdateExperience([FromBody]UpdateExperienceCommand updateExperienceCommand)
        {
            Expression<Func<ExperienceEntity, bool>> retrievalFunc =
                (exp) => exp.CompanyName == updateExperienceCommand.CompanyNameId
                    && exp.Position == updateExperienceCommand.PositionId;

            await _commandBus.SendAsync(updateExperienceCommand, retrievalFunc);

            return new JsonResult($"Processed command '{updateExperienceCommand}'.");
        }
    }
}

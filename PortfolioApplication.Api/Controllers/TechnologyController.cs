using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Commands;
using PortfolioApplication.Api.CQRS.Commands.Technologies.Commands;
using PortfolioApplication.Api.CQRS.Queries;
using PortfolioApplication.Api.DataTransferObjects.Technologies;
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
    /// Controller processing requests for Technology entities. Produces JSON output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TechnologyController : Controller
    {
        private readonly ITechnologyQuery _technologyQuery;
        private readonly IProjectQuery _projectQuery;
        private readonly ICommandBus _commandBus;

        /// <summary>
        /// TechnologyController constructor
        /// </summary>
        /// <param name="technologyQuery"> Query consumed to retrieve Technology entities </param>
        /// <param name="commandBus"> Command bus managing incoming Technology commands </param>
        public TechnologyController(
            ITechnologyQuery technologyQuery,
            IProjectQuery projectQuery,
            ICommandBus commandBus)
        {
            _technologyQuery = technologyQuery;
            _projectQuery = projectQuery;
            _commandBus = commandBus;
        }

        /// <summary>
        /// Retrieve Technology entity by its id
        /// </summary>
        /// <param name="id"> Identification number of Technology entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> Technology entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entity from database")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Enquired entity does not exist in database")]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetTechnologyById([Required]int id)
        {
            Func<DbSet<TechnologyEntity>, Task<TechnologyEntity>> retrievalFunc =
                dbSet => dbSet.Include(tech => tech.TechnologyType)
                .Include(tech => tech.Projects)
                .ThenInclude(projs => projs.Project)
                .ThenInclude(proj => proj.ProjectType)
                .SingleAsync(tech => tech.Id == id);

            var technologyDto = await _technologyQuery.GetAsync(id.ToString(), retrievalFunc);

            return new JsonResult(technologyDto);
        }

        /// <summary>
        /// Retrieve all Technology entities
        /// </summary>
        /// <returns> Technology entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entities from database")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, description: "Collection of enquired entities is empty")]
        [HttpGet]
        public async Task<IActionResult> GetTechnologies()
        {
            Func<DbSet<TechnologyEntity>, Task<List<TechnologyEntity>>> retrievalFunc =
                dbSet => dbSet.Include(tech => tech.TechnologyType)
                .Include(tech => tech.Projects)
                .ThenInclude(projs => projs.Project)
                .ThenInclude(proj => proj.ProjectType)
                .ToListAsync();

            var technologyDtos = await _technologyQuery.GetAsync(retrievalFunc);

            return new JsonResult(technologyDtos);
        }

        /// <summary>
        /// Create new Technology entity in database
        /// </summary>
        /// <param name="technologyDto"> DTO containing parameters to create a new Technology entity </param>
        /// <returns> JSON containing information about processed command </returns>
        [SwaggerResponse((int)HttpStatusCode.Created, description: "Successfully created new entity in database")]
        [SwaggerResponse((int)HttpStatusCode.NotAcceptable, description: "Provided values are not acceptable, e.g. empty entity")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, description: "Entity already exists in database")]
        [HttpPost]
        public async Task<IActionResult> CreateTechnology([FromBody]CreateTechnologyDto technologyDto)
        {
            var createTechnologyCommand = new CreateTechnologyCommand(technologyDto.Name, technologyDto.TechnologyType.TechnologyTypeEnum);

            await _commandBus.SendAsync(createTechnologyCommand);

            return new JsonResult($"Processed command '{createTechnologyCommand}'.");
        }

        /// <summary>
        /// Patch (modify) existing Technology entity in database
        /// </summary>
        /// <param name="technologyNameId"> </param>
        /// <param name="patchedTechnologyDto"> Patch operations to alter Technology entity </param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromQuery]string technologyNameId, [FromBody]JsonPatchDocument<TechnologyDto> patchedTechnologyDto)
        {
            Func<DbSet<TechnologyEntity>, Task<TechnologyEntity>> retrievalFunc =
                dbSet => dbSet.Include(tech => tech.TechnologyType)
                .Include(tech => tech.Projects)
                .ThenInclude(projs => projs.Project)
                .ThenInclude(proj => proj.ProjectType)
                .SingleAsync(tech => tech.Name == technologyNameId);

            var technologyDto = await _technologyQuery.GetAsync(technologyNameId, retrievalFunc);
            patchedTechnologyDto.ApplyTo(technologyDto, ModelState);

            IList<TechnologyProjectDto> technologyProjectDtos = new List<TechnologyProjectDto>();

            foreach(var project in technologyDto.Projects)
            {
                Func<DbSet<ProjectEntity>, Task<ProjectEntity>> retrivalFunc =
                dbSet => dbSet
                .Include(proj => proj.ProjectType)
                .Include(proj => proj.Technologies)
                .ThenInclude(techs => techs.Technology)
                .ThenInclude(tech => tech.TechnologyType)
                .SingleAsync(proj => proj.Name == project.Name);

                var projectDto = await _projectQuery.GetAsync(project.Name.ToString(), retrivalFunc);
                var technologyProjectDto = Mapper.Map<TechnologyProjectDto>(projectDto);

                technologyProjectDtos.Add(technologyProjectDto);
            }

            technologyDto.Projects = technologyProjectDtos;

            var technology = Mapper.Map<TechnologyEntity>(technologyDto);

            // todo: SAVE TO DB

            return Ok(technologyDto);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Services.CQRS.Queries;
using PortfolioApplication.Services.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Controllers
{
    /// <summary>
    /// Controller processing requests for ProjectType entities. Produces JSON output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProjectTypeController : Controller
    {
        private readonly IProjectTypeQuery _projectTypeEntityQuery;

        /// <summary>
        /// ProjectTypeController constructor
        /// </summary>
        /// <param name="projectTypeEntityQuery"> Query consumed to retrieve ProjectType entities </param>
        public ProjectTypeController(
            IProjectTypeQuery projectTypeEntityQuery)
        {
            _projectTypeEntityQuery = projectTypeEntityQuery;
        }

        /// <summary>
        /// Get endpoint retrieving ProjectType entity by its id
        /// </summary>
        /// <param name="id"> Identification number of ProjectType entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> ProjectType in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ProjectTypeDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundObjectResult))]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetProjectTypeById([Required]int id)
        {
            var projectTypeEntity = await _projectTypeEntityQuery.Get(id);
            var projectTypeDto = Mapper.Map<ProjectTypeDto>(projectTypeEntity);

            return new JsonResult(projectTypeDto);

        }

        /// <summary>
        /// Get endpoint retrieving all ProjectType entities
        /// </summary>
        /// <returns> ProjectType collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ProjectTypeDto>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundObjectResult))]
        [HttpGet]
        public async Task<IActionResult> GetProjectTypes()
        {
            var projectTypeEntities = await _projectTypeEntityQuery.Get();
            var projectTypeDtos = Mapper.Map<IEnumerable<ProjectTypeDto>>(projectTypeEntities);

            return new JsonResult(projectTypeDtos);
        }
    }
}

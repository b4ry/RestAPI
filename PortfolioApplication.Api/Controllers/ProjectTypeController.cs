using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Queries;
using Swashbuckle.AspNetCore.SwaggerGen;
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
        private readonly IProjectTypeQuery _projectTypeQuery;

        /// <summary>
        /// ProjectTypeController constructor
        /// </summary>
        /// <param name="projectTypeEntityQuery"> Query consumed to retrieve ProjectType entities </param>
        public ProjectTypeController(
            IProjectTypeQuery projectTypeEntityQuery)
        {
            _projectTypeQuery = projectTypeEntityQuery;
        }

        /// <summary>
        /// Retrieve ProjectType entity by its id
        /// </summary>
        /// <param name="id"> Identification number of ProjectType entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> ProjectType in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetProjectTypeById([Required]int id)
        {
            var projectTypeDto = await _projectTypeQuery.Get(id, dbSet => dbSet.SingleAsync(x => x.Id == id));

            return new JsonResult(projectTypeDto);

        }

        /// <summary>
        /// Retrieve all ProjectType entities
        /// </summary>
        /// <returns> ProjectType entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetProjectTypes()
        {
            var projectTypeDtos = await _projectTypeQuery.Get(dbSet => dbSet.ToListAsync());

            return new JsonResult(projectTypeDtos);
        }
    }
}

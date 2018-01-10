using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Queries;
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
    /// Controller processing requests for Project entities. Produces JSON output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProjectController : Controller
    {
        private readonly IProjectQuery _projectQuery;

        /// <summary>
        /// ProjectController constructor
        /// </summary>
        /// <param name="projectQuery"> Query consumed to retrieve Project entities </param>
        public ProjectController(
            IProjectQuery projectQuery)
        {
            _projectQuery = projectQuery;
        }

        /// <summary>
        /// Retrieve Project entity by its id
        /// </summary>
        /// <param name="id"> Identification number of Project entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> Project entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entity from database")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Enquired entity does not exist in database")]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetProjectById([Required]int id)
        {
            Func<DbSet<ProjectEntity>, Task<ProjectEntity>> retrivalFunc =
                dbSet => dbSet
                .Include(proj => proj.ProjectType)
                .Include(proj => proj.Technologies)
                .ThenInclude(techs => techs.Technology)
                .ThenInclude(tech => tech.TechnologyType)
                .SingleAsync(proj => proj.Id == id);

            var projectDto = await _projectQuery.GetAsync(id.ToString(), retrivalFunc);

            return new JsonResult(projectDto);
        }

        /// <summary>
        /// Retrieve all Project entities
        /// </summary>
        /// <returns> Project entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entities from database")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, description: "Collection of enquired entities is empty")]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            Func<DbSet<ProjectEntity>, Task<List<ProjectEntity>>> retrivalFunc =
                dbSet => dbSet
                .Include(proj => proj.ProjectType)
                .Include(proj => proj.Technologies)
                .ThenInclude(techs => techs.Technology)
                .ThenInclude(tech => tech.TechnologyType)
                .ToListAsync();

            var projectDtos = await _projectQuery.GetAsync(retrivalFunc);

            return new JsonResult(projectDtos);
        }
    }
}

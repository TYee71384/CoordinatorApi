using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoordinatorTaskProject.Data;
using CoordinatorTaskProject.Models.AmsTables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoordinatorTaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly ITaskRepository repo;

        public DictionaryController(ITaskRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("site")]
        public async Task<IEnumerable<AmsAccountMain>> GetSites()
        {
            
            return await repo.GetAmsSites();
        }
    }
}
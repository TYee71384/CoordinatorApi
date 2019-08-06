using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoordinatorTaskProject.Data;
using CoordinatorTaskProject.Models.CoordinatorDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoordinatorTaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ITaskRepository repo;

        public SiteController(ITaskRepository repo)
        {
            this.repo = repo;
        }

        public async Task<ActionResult> GetAllUpdates()
        {
            var updates = await repo.GetAllSites();
            return Ok(updates);
        }


        [HttpGet("{site}/{updateNum}")]
        public async Task<ActionResult> GetUpdateInfo(string site, int updateNum)
        {
            var updateInfo = await repo.GetUpdateBySiteNameAndUpdateNum(site.ToUpper(), updateNum);
            if (updateInfo == null)

                return BadRequest("This update does not exist. Please Try Again");
            return Ok(updateInfo);
        }

        [HttpPost("{site}")]
        public async Task<ActionResult> AddUpdate(string site, Update update)
        {
            //  var siteFromAms = repo.FindSite(site);
            update.SiteMnemonic = site.ToUpper();
            update.SiteId = await repo.FindSiteId(site);
            var number = await repo.CheckUpdate(update.UpdateNumber, update.SiteMnemonic);
            if (number == 0)
            {
                repo.Add(update);
                if (await repo.SaveAll())
                    return CreatedAtAction("GetUpdateInfo", new { site, updateNum = update.UpdateNumber }, update);
                return BadRequest("Cannot Save. Please Try Again");
            }
            else return Ok(await repo.GetUpdateBySiteNameAndUpdateNum(site, update.UpdateNumber));
        }

        [HttpDelete("{site}/{updateNum}")]
        public async Task<ActionResult> RemoveUpdate(string site, int updateNum)
        {
            var updateInfo = await repo.GetUpdateBySiteNameAndUpdateNum(site.ToUpper(), updateNum);
            if (updateInfo == null)
                return BadRequest("This update does not exist. Please Try Again");

            repo.Delete(updateInfo);
            if(await repo.SaveAll())
            return NoContent();

            return BadRequest("Problem deleting this update. Please Try again");
                
        }


    }
}
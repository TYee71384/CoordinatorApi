using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoordinatorTaskProject.Data;
using CoordinatorTaskProject.DTO;
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


        [HttpGet("{accountID}/{updateNum}", Name ="GetUpdateInfo")]
        public async Task<ActionResult> GetUpdateInfo(int accountID, int updateNum)
        {
            var updateFromRepo = await repo.GetUpdateBySiteNameAndUpdateNum(accountID, updateNum);
            var Tasks = await repo.GetTaskDataForUpdate(updateFromRepo.Id);
            var name = await repo.GetSiteName(accountID, updateFromRepo.BillingCodeID);
            
            UpdateInfoDTO updateInfo = new UpdateInfoDTO
            {
                CurrentRelease = updateFromRepo.CurrentRelease,
                Id = updateFromRepo.Id,
                NextUpdate = updateFromRepo.NextUpdate,
                AccountID = updateFromRepo.AccountID,
                BillingCodeID = updateFromRepo.BillingCodeID,
                UpdateNumber = updateFromRepo.UpdateNumber,
                Tasks = Tasks,
                Name = name
            };
            if (updateInfo == null)

                return BadRequest("This update does not exist. Please Try Again");
            return Ok(updateInfo);
        }

        [HttpPost("addSite")]
        public async Task<ActionResult> AddUpdate(Update update)
        {
              var siteFromAms = await repo.FindSite(update.AccountID);
            update.BillingCodeID = siteFromAms.BillingCodeID;
            //update.SiteMnemonic = site.ToUpper();
            //update.SiteId = accountId;
            var number = await repo.CheckUpdate(update.UpdateNumber, update.BillingCodeID);
            if (number == 0)
            {
                repo.Add(update);
                if (await repo.SaveAll())
                    return CreatedAtAction("GetUpdateInfo", new { accountID=update.AccountID, updateNum = update.UpdateNumber }, update);
                return BadRequest("Cannot Save. Please Try Again");
            }
            else return Ok(await repo.GetUpdateBySiteNameAndUpdateNum(update.AccountID, update.UpdateNumber));
        }

        [HttpDelete("{site}/{updateNum}")]
        public async Task<ActionResult> RemoveUpdate(int site, int updateNum)
        {
            var updateInfo = await repo.GetUpdateBySiteNameAndUpdateNum(site, updateNum);
            if (updateInfo == null)
                return BadRequest("This update does not exist. Please Try Again");

            repo.Delete(updateInfo);
            if(await repo.SaveAll())
            return NoContent();

            return BadRequest("Problem deleting this update. Please Try again");
                
        }


    }
}
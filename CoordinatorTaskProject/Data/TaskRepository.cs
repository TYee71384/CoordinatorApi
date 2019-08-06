using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoordinatorTaskProject.DTO;
using CoordinatorTaskProject.Models.AmsTables;
using CoordinatorTaskProject.Models.CoordinatorDb;
using Microsoft.EntityFrameworkCore;
using Task = CoordinatorTaskProject.Models.CoordinatorDb.Task;

namespace CoordinatorTaskProject.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AmsDataContext amsCtx;
        private readonly CoordinatorContext ctx;

        public TaskRepository(AmsDataContext amsCtx, CoordinatorContext ctx)
        {
            this.amsCtx = amsCtx;
            this.ctx = ctx;
        }

        public async Task<IEnumerable<Update>> GetAllSites()
            {
            return  await ctx.Updates.Include(x => x.Tasks).ToListAsync();
            }
        public async Task<TaskData> GetData(int taskId)
        {
            var amsTask = await amsCtx.AmsTaskMain.Include(x => x.AmsTaskText).Include(x => x.AmsAccountMain).FirstOrDefaultAsync(x => x.TaskID == taskId);
            //      var TaskFromDb = await ctx.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId);
            //       if(TaskFromDb.Comment == null) TaskFromDb.Comment = "";
            if (amsTask != null)
            {
                TaskData task = new TaskData
                {
                    AssignedTo = amsTask.AssignedTo,
                    //          Comment = TaskFromDb.Comment,
                    Description = amsTask.AmsTaskText.Description,
                    ModuleID = amsTask.ModuleID,
                    PriorityID = amsTask.PriorityID,
                    StatusID = amsTask.StatusID,
                    TaskID = amsTask.TaskID,
                    SiteMnemonic = amsTask.AmsAccountMain.BillingCodeID,
                    SiteName = amsTask.AmsAccountMain.Name
                };

                return task;
            }
            return null;
        }

        public async Task<AmsTaskMain> GetTaskFromAms(int taskId)
        {
            return await amsCtx.AmsTaskMain.Include(x => x.AmsTaskText).Include(x => x.AmsAccountMain).FirstOrDefaultAsync(x => x.TaskID == taskId);
        }

        public async Task<IEnumerable<TaskData>> GetTaskDataForUpdate(int update)
            {
            var updateInfo = await ctx.Updates.FirstOrDefaultAsync(x => x.Id == update);
            var tasks = new List<TaskData>();
            foreach(var task in updateInfo.Tasks)
            {
                var amsInfo = await amsCtx.AmsTaskMain.FirstOrDefaultAsync(x=> x.TaskID == task.TaskId);
                var t = new TaskData
                {
                    AssignedTo = amsInfo.AssignedTo,
                    Comment = task.Comment,
                    Description = amsInfo.AmsTaskText.Description,
                    ModuleID = amsInfo.ModuleID,
                    PriorityID = amsInfo.PriorityID,
                    StatusID = amsInfo.StatusID,
                    TaskID = amsInfo.TaskID,
                    SiteMnemonic = amsInfo.AmsAccountMain.BillingCodeID,
                    SiteName = amsInfo.AmsAccountMain.Name
                };
                tasks.Add(t);
            }
            return tasks;
            }

      public async Task<AmsAccountMain> FindSite(string name)
        {
            return await amsCtx.amsAccountMain.FirstOrDefaultAsync(s => s.BillingCodeID == name);
        }

        public async Task<int> FindSiteId(string name)
        {
            var site = await amsCtx.amsAccountMain.FirstOrDefaultAsync(x => x.BillingCodeID == name.ToUpper());
            return site.AccountID;
        }

        public async Task<int> CheckUpdate(int id, string site)
        {
            var update = await ctx.Updates.FirstOrDefaultAsync(x => x.UpdateNumber == id && x.SiteMnemonic == site);
            if (update != null)
                return update.UpdateNumber;
            return 0;
        }

        public async Task<Update> GetUpdateBySiteNameAndUpdateNum(string name, int updatenum)
        {
            return await ctx.Updates.FirstOrDefaultAsync(x => x.SiteMnemonic == name && x.UpdateNumber == updatenum);
        }

        public async Task<bool> CheckForDuplicateTask(int taskNum, int updateId)
        {
            var task = await ctx.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskNum && x.UpdateId == updateId);
           return task != null ? true : false;

        }

       
        public void Add<T>(T entity) where T : class
        {
            ctx.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            ctx.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await ctx.SaveChangesAsync() > 0;
        }

        public async  Task<Task> GetTask(int id)
        {
            return await ctx.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);
        }

        
    }
}

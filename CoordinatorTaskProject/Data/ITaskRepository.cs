using CoordinatorTaskProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Data
{
    public interface ITaskRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> CheckForDuplicateTask(int taskNum, int updateId);
        Task<int> CheckUpdate(int id, string site);
        void Delete<T>(T entity) where T : class;
        Task<Models.AmsTables.AmsAccountMain> FindSite(string name);
        Task<int> FindSiteId(string name);
        Task<IEnumerable<Models.CoordinatorDb.Update>> GetAllSites();
        Task<TaskData> GetData(int taskId);
        Task<Models.CoordinatorDb.Task> GetTask(int id);
        Task<Models.AmsTables.AmsTaskMain> GetTaskFromAms(int taskId);
        Task<Models.CoordinatorDb.Update> GetUpdateBySiteNameAndUpdateNum(string name, int updatenum);
        Task<bool> SaveAll();
    }
}

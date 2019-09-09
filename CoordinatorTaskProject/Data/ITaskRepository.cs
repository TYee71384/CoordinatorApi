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
        Task<Models.AmsTables.AmsAccountMain> FindSite(int id);
        Task<int> FindSiteId(string name);
        Task<IEnumerable<Models.CoordinatorDb.Update>> GetAllSites();
        Task<IEnumerable<Models.AmsTables.AmsAccountMain>> GetAmsSites();
        Task<TaskData> GetData(int taskId);
        Task<string> GetSiteName(int accountId, string billingCode);
        Task<Models.CoordinatorDb.Task> GetTask(int id);
        Task<IEnumerable<TaskData>> GetTaskDataForUpdate(int update);
        Task<Models.AmsTables.AmsTaskMain> GetTaskFromAms(int taskId);
        Task<Models.CoordinatorDb.Update> GetUpdateBySiteNameAndUpdateNum(int id, int updatenum);
        Task<bool> SaveAll();
    }
}

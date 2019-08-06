using CoordinatorTaskProject.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorTaskProject.Hubs
{
    public class DataHub: Hub
    {
        public DataHub(ITaskRepository repo)
        {

        }
    }
}

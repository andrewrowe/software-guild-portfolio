using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesWebsite.Models.Repositories.Interfaces
{
    public interface IJobRepository
    {
        List<Job> GetAll();
        Job Get(int id);
        void Add(Job job);
        void Remove(int id);
        void Edit(Job job);
    }
}

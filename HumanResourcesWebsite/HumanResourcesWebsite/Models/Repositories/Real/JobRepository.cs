using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Repositories.Interfaces;

namespace HumanResourcesWebsite.Models.Repositories.Real
{
    public class JobRepository : IJobRepository
    {
        public List<Job> GetAll()
        {
            throw new NotImplementedException();
        }

        public Job Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Job job)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
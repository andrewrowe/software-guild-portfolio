using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Data;
using HumanResourcesWebsite.Models.Repositories.Interfaces;

namespace HumanResourcesWebsite.Models.Repositories.Real
{
    public class StateRepository : IStateRepository
    {
        public List<State> GetAll()
        {
            throw new NotImplementedException();
        }

        public State Get(string stateAbbrv)
        {
            throw new NotImplementedException();
        }

        public void Add(State state)
        {
            throw new NotImplementedException();
        }

        public void Remove(string stateAbbrv)
        {
            throw new NotImplementedException();
        }
    }
}
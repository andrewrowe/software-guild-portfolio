using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanResourcesWebsite.Models.Data;

namespace HumanResourcesWebsite.Models.Repositories.Interfaces
{
    public interface IStateRepository
    {
        List<State> GetAll();
        State Get(string stateAbbrv);
        void Add(State state);
        void Remove(string stateAbbrv);
    }
}

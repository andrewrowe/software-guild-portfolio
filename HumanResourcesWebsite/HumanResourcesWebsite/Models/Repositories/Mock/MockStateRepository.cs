using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Data;
using HumanResourcesWebsite.Models.Repositories.Interfaces;

namespace HumanResourcesWebsite.Models.Repositories
{
    public class MockStateRepository : IStateRepository
    {
        private static List<State> _states = new List<State>()
        {
            new State() {StateAbbreviation = "PA", StateName = "Pennsylvania"},
            new State() {StateAbbreviation = "OH", StateName = "Ohio"},
            new State() {StateAbbreviation = "MI", StateName = "Michigan"},
            new State() {StateAbbreviation = "IL", StateName = "Illinois"}
        };

        public List<State> GetAll()
        {
            return _states;
        }

        public State Get(string stateAbbrv)
        {
            return _states.First(m => m.StateAbbreviation == stateAbbrv);
        }

        public void Add(State state)
        {
            _states.Add(state);
        }

        public void Remove(string stateAbbrv)
        {
            _states.RemoveAll(m => m.StateAbbreviation == stateAbbrv);
        }
    }
}
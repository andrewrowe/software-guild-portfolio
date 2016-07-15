using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Data;

namespace HumanResourcesWebsite.Models.Repositories.Mock
{
    public class MockPolicyRepository
    {
        private static List<Policy> _policies = new List<Policy>()
        {
            new Policy() {Category = "New Hire", Id = 1, PolicyText = "Applicant Reference Release...This is where Applicant Reference Release Text would go for form 1.", Title = "Applicant Reference Release"},
            new Policy() {Category = "New Hire", Id = 2, PolicyText = "Direct Deposit Enrollment Form...This is where Direct Deposit Enrollment Form Text would go for form 2.", Title = "Direct Deposit Enrollment Form"},
            new Policy() {Category = "Harassment", Id = 3, PolicyText = "Sexual Harassment Complaint Form...This is where Sexual Harassment Complaint Form Text would go for form 3.", Title = "Sexual Harassment Complaint Form"},
            new Policy() {Category = "Vacation", Id = 4, PolicyText = "Leave Request...This is where Leave Request Text would go for form 4.", Title = "Leave Request"},
            new Policy() {Category = "Self Assessments", Id = 5, PolicyText = "Affirmative Action Recruitment Report Form...This is where Affirmative Action Recruitment Report Form Text would go for form 5.", Title = "Affirmative Action Recruitment Report Form"},
            new Policy() {Category = "Self Assessments", Id = 6, PolicyText = "Meeting Minutes Form...This is where Meeting Minutes Form Text would go for form 6.", Title = "Meeting Minutes Form"},
        };

        public List<Policy> GetAll()
        {
            return _policies;
        }

        public Policy Get(int id)
        {
            return _policies.First(p => p.Id == id);
        }

        public void Add(Policy policy)
        {
            policy.Id = _policies.Max(p => p.Id) + 1;
            _policies.Add(policy);
        }

        public void Remove(int id)
        {
            _policies.RemoveAll(p => p.Id == id);
        }

        public void Edit(Policy policy)
        {
            var selectedPolicy = _policies.First(p => p.Id == policy.Id);

            selectedPolicy.Title = policy.Title;
            selectedPolicy.PolicyText = policy.PolicyText;
            selectedPolicy.Category = policy.Category;
        }
    }
}
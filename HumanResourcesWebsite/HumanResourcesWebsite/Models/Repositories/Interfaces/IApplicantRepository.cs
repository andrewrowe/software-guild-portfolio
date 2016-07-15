using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesWebsite.Models.Repositories.Interfaces
{
    public interface IApplicantRepository
    {
        List<Applicant> GetAll();
        Applicant Get(int id);
        void Add(Applicant applicant);
        void Remove(int id);
        void Edit(Applicant applicant);
    }
}

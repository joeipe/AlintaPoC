using AlintaPoC.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Application.Services
{
    public interface IAppService
    {
        IList<PersonDto> GetAllPeople();
        PersonDto GetPersonById(int id);
        void AddPerson(PersonDto value);
        void UpdatePerson(PersonDto value);
        void DeletePerson(int id);
        PersonDto GetPersonsDetailsById(int id);


        IList<RoleDto> GetAllRoles();
        RoleDto GetRoleById(int id);
        void AddRole(RoleDto value);
        void UpdateRole(RoleDto value);
        void DeleteRole(int id);
    }
}

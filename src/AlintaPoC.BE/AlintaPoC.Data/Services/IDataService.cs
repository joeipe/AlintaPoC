using AlintaPoC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Data.Services
{
    public interface IDataService
    {
        IEnumerable<Person> GetAllPeople();
        Person GetPersonById(int id);
        void AddPerson(Person data);
        void UpdatePerson(Person data);
        void DeletePerson(int id);
        Person GetPersonsDetailsById(int id);


        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int id);
        void AddRole(Role data);
        void UpdateRole(Role data);
        void DeleteRole(int id);
    }
}

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
    }
}

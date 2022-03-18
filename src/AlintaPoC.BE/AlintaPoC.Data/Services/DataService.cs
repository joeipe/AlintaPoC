using AlintaPoC.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlintaPoC.Data.Services
{
    public class DataService : IDataService
    {
        private readonly Uow _uow;

        public DataService(Uow uow)
        {
            _uow = uow;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            var data = _uow.PersonRepo.GetAll();
            return data;
        }

        public Person GetPersonById(int id)
        {
            var data = _uow.PersonRepo.GetById(id);
            return data;
        }

        public void AddPerson(Person data)
        {
            _uow.PersonRepo.Create(data);
            _uow.Save();
        }

        public void UpdatePerson(Person data)
        {
            _uow.PersonRepo.Update(data);
            _uow.Save();
        }

        public void DeletePerson(int id)
        {
            _uow.PersonRepo.Delete(id);
            _uow.Save();
        }
    }
}

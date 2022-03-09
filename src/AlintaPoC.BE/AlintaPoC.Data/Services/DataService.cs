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

        public Person GetPersonsDetailsById(int id)
        {
            var data = _uow.PersonRepo.SearchForInclude
                (
                    a => a.Id == id,
                    source => source.Include(x => x.Role)
                ).FirstOrDefault();
            return data;
        }


        public void DeletePerson(int id)
        {
            _uow.PersonRepo.Delete(id);
            _uow.Save();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var data = _uow.RoleRepo.GetAll();
            return data;
        }

        public Role GetRoleById(int id)
        {
            var data = _uow.RoleRepo.GetById(id);
            return data;
        }

        public void AddRole(Role data)
        {
            _uow.RoleRepo.Create(data);
            _uow.Save();
        }

        public void UpdateRole(Role data)
        {
            _uow.RoleRepo.Update(data);
            _uow.Save();
        }

        public void DeleteRole(int id)
        {
            _uow.RoleRepo.Delete(id);
            _uow.Save();
        }
    }
}

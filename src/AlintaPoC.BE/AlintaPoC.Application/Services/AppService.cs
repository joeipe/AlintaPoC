using AlintaPoC.Contracts;
using AlintaPoC.Data.Services;
using AlintaPoC.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Application.Services
{
    public class AppService : IAppService
    {
        private readonly IMapper _mapper;
        private readonly IDataService _dataService;

        public AppService(
            IMapper mapper,
            IDataService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        public IList<PersonDto> GetAllPeople()
        {
            var data = _dataService.GetAllPeople();
            var vm = _mapper.Map<IList<PersonDto>>(data);
            return vm;
        }

        public PersonDto GetPersonById(int id)
        {
            var data = _dataService.GetPersonById(id);
            var vm = _mapper.Map<PersonDto>(data);
            return vm;
        }

        public void AddPerson(PersonDto value)
        {
            var data = _mapper.Map<Person>(value);
            _dataService.AddPerson(data);
        }

        public void UpdatePerson(PersonDto value)
        {
            var data = _mapper.Map<Person>(value);
            _dataService.UpdatePerson(data);
        }

        public void DeletePerson(int id)
        {
            _dataService.DeletePerson(id);
        }

        public PersonDto GetPersonsDetailsById(int id)
        {
            var data = _dataService.GetPersonsDetailsById(id);
            var vm = _mapper.Map<PersonDto>(data);
            return vm;
        }



        public IList<RoleDto> GetAllRoles()
        {
            var data = _dataService.GetAllRoles();
            var vm = _mapper.Map<IList<RoleDto>>(data);
            return vm;
        }

        public RoleDto GetRoleById(int id)
        {
            var data = _dataService.GetRoleById(id);
            var vm = _mapper.Map<RoleDto>(data);
            return vm;
        }

        public void AddRole(RoleDto value)
        {
            var data = _mapper.Map<Role>(value);
            _dataService.AddRole(data);
        }

        public void UpdateRole(RoleDto value)
        {
            var data = _mapper.Map<Role>(value);
            _dataService.UpdateRole(data);
        }

        public void DeleteRole(int id)
        {
            _dataService.DeleteRole(id);
        }
    }
}

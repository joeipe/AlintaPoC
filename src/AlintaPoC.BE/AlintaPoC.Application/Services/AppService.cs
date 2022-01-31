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
            if (id != 0)
            {
                throw new Exception("Man made error");
            }
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

        public void AddTwoPeople(PersonDto value)
        {
            var data = _mapper.Map<Person>(value);
            _dataService.AddTwoPeople(data);
        }
    }
}

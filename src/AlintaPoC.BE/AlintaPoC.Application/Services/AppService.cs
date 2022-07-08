using AlintaPoC.Contracts;
using AlintaPoC.Data.Services;
using AlintaPoC.Domain;
using AlintaPoC.Integration.TableStorage.Domain;
using AlintaPoC.Integration.TableStorage.Repositories;
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
        private readonly ITodoRepository _todoRepository;

        public AppService(
            IMapper mapper,
            IDataService dataService,
            ITodoRepository todoRepository)
        {
            _mapper = mapper;
            _dataService = dataService;
            _todoRepository = todoRepository;
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



        public async Task<IList<TodoDto>> GetAllTodoAsync()
        {
            var data = await _todoRepository.GetAllTodoAsync();
            var vm = _mapper.Map<IList<TodoDto>>(data);
            return vm;
        }

        public async Task<TodoDto> GetTodoByIdAsync(string partitionKey, string rowKey)
        {
            var data = await _todoRepository.GetTodoByIdAsync(partitionKey, rowKey);
            var vm = _mapper.Map<TodoDto>(data);
            return vm;
        }

        public async Task AddTodoAsync(TodoDto value)
        {
            var data = _mapper.Map<TodoEntity>(value);
            await _todoRepository.AddTodoAsync(data);
        }

        public async Task UpdateTodoAsync(TodoDto value)
        {
            var data = _mapper.Map<TodoEntity>(value);
            await _todoRepository.UpdateTodoAsync(data);
        }

        public async Task DeleteTodoAsync(string partitionKey, string rowKey)
        {
            await _todoRepository.DeleteTodoAsync(partitionKey, rowKey);
        }
    }
}

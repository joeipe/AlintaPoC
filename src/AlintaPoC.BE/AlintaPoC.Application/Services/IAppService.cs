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

        Task<IList<TodoDto>> GetAllTodoAsync();
        Task<TodoDto> GetTodoByIdAsync(string partitionKey, string rowKey);
        Task AddTodoAsync(TodoDto value);
        Task UpdateTodoAsync(TodoDto value);
        Task DeleteTodoAsync(string partitionKey, string rowKey);
    }
}

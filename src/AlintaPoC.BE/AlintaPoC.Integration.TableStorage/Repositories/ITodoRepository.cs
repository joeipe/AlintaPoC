using AlintaPoC.Integration.TableStorage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Integration.TableStorage.Repositories
{
    public interface ITodoRepository
    {
        Task<List<TodoEntity>> GetAllTodoAsync();
        Task<TodoEntity> GetTodoByIdAsync(string partitionKey, string rowKey);
        Task AddTodoAsync(TodoEntity value);
        Task UpdateTodoAsync(TodoEntity value);
        Task DeleteTodoAsync(string partitionKey, string rowKey);
    }
}

using AlintaPoC.Integration.TableStorage.Domain;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Integration.TableStorage.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TableClient _client;

        public TodoRepository(TableServiceClient tableServiceClient)
        {
            _client = tableServiceClient.GetTableClient("Todo");
            _client.CreateIfNotExists();
        }

        public async Task<List<TodoEntity>> GetAllTodoAsync()
        {
            var result = new List<TodoEntity>();

            // approach 1
            //var partitionKey = "Vacation";
            //var todoEntities = _client.QueryAsync<TodoEntity>(TableClient.CreateQueryFilter($"PartitionKey eq {partitionKey}"));

            // approach 2
            //var todoEntities = _client.QueryAsync<TodoEntity>(customer => customer.RowKey != "0");

            var todoEntities = _client.QueryAsync<TodoEntity>();

            await foreach (Page<TodoEntity> page in todoEntities.AsPages())
            {
                var val = page.Values.ToList();
                result.AddRange(val);
            }
            return result;
        }

        public async Task<TodoEntity> GetTodoByIdAsync(string partitionKey, string rowKey)
        {
            var result = await _client.GetEntityAsync<TodoEntity>(partitionKey, rowKey);
            return result;
        }

        public async Task AddTodoAsync(TodoEntity value)
        {
            await _client.AddEntityAsync(value);
        }

        public async Task UpdateTodoAsync(TodoEntity value)
        {
            await _client.UpsertEntityAsync(value);
        }

        public async Task DeleteTodoAsync(string partitionKey, string rowKey)
        {
            await _client.DeleteEntityAsync(partitionKey, rowKey);
        }
    }
}

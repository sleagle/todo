using TodoApi.Data.Models;

namespace TodoApi.Data.Repositories;

public interface ITodoRepository
{
    Task<List<TodoTask>> GetAll();

    Task<TodoTask> GetById(int id);
    
    Task<bool> Add(TodoTask model);

    Task<bool> Delete(int id);
}
using TodoApi.Data.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<List<TodoTask>> GetAllTodos();
    
    Task<TodoTask?> GetTodoById(int id);
    
    Task<bool> AddTodo(TodoTask model);
    
    Task<bool> DeleteTodo(int id);
}
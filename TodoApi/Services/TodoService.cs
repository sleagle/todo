using TodoApi.Data.Models;
using TodoApi.Data.Repositories;

namespace TodoApi.Services;

public class TodoService: ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<List<TodoTask>> GetAllTodos()
    {
        return await _todoRepository.GetAll();
    }

    public async Task<TodoTask?> GetTodoById(int id)
    {
        return await _todoRepository.GetById(id);
    }

    public async Task<bool> AddTodo(TodoTask model)
    {
        return await _todoRepository.Add(model);
    }

    public async Task<bool> DeleteTodo(int id)
    {
        return await _todoRepository.Delete(id);
    }
}
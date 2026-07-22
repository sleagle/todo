using Microsoft.AspNetCore.Mvc;
using TodoApi.Data.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class TodoController : Controller
{
    
    private readonly ITodoService _todoService;
    
    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _todoService.GetAllTodos();
        
        return Ok(todos);

    }

    [HttpGet]
    [Route("task/{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var todo = await _todoService.GetTodoById(id);

        if (todo is null)
        {
            return NotFound($"Todo with id {id} not found");
        }

        return Ok(todo);
    }

    [HttpPost]
    [Route("task/add")]
    public async Task<IActionResult> AddTodo(TodoTask model)
    {
        await _todoService.AddTodo(model);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("task/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleted = await _todoService.DeleteTodo(id);

        if (!deleted)
        {
            return NotFound($"Todo with id {id} not found");
        }
        
        return NoContent();
    }
}
using System.Text.Json;
using TodoApi.Data.Models;

namespace TodoApi.Data.Repositories;

public class JsonTodoRepository: ITodoRepository
{
    
    private readonly string _filePath;
    
    /*
     * 
     */
    private readonly SemaphoreSlim _fileLock = new(1, 1); 
    
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public JsonTodoRepository(IWebHostEnvironment env, IConfiguration config)
    {
        var configuredPath = config["DataFilePath"] ?? "DataFiles/products.json";
        
        _filePath = Path.Combine(env.ContentRootPath, configuredPath);
        
        var directory = Path.GetDirectoryName(_filePath);
        
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public async Task<List<TodoTask>> GetAll()
    {
        await _fileLock.WaitAsync();

        try
        {
            var todos = await ReadTodos();

            return todos.OrderBy(todo => todo.Id).ToList();
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<TodoTask?> GetById(int id)
    {
        await _fileLock.WaitAsync();

        try
        {
            var todos = await ReadTodos();

            return todos.FirstOrDefault(todo => todo.Id == id);
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<bool> Add(TodoTask model)
    {
        await _fileLock.WaitAsync();

        try
        {
            var todos = await ReadTodos();

            todos.Add(model);
            
            await WriteTodos(todos);
            
            return true;
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<bool> Delete(int id)
    {
        await _fileLock.WaitAsync();

        try
        {
            var todos = await ReadTodos();

            var taskToDelete = todos.FirstOrDefault(todo => todo.Id == id);

            if (taskToDelete is null)
            {
                return false;
            }
            
            todos.Remove(taskToDelete);
            
            await WriteTodos(todos);
            
            return true;
        }
        finally
        {
            _fileLock.Release();
        }
    }

    private async Task<List<TodoTask>> ReadTodos()
    {
        if (!File.Exists(_filePath))
        {
            var initTodos = GetInitialTodos();
            
            await WriteTodos(initTodos);
            
            return initTodos;
        }
        
        await using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.None);
        
        var todos = await JsonSerializer.DeserializeAsync<List<TodoTask>>(fileStream, _jsonSerializerOptions);
        
        return todos ?? [];
    }

    private async Task WriteTodos(List<TodoTask> todos)
    {
        var tmpFile = $"{_filePath},tmp";
        
        await using var fileStream = new FileStream(tmpFile, FileMode.Create, FileAccess.Write, FileShare.None);
        
        await JsonSerializer.SerializeAsync(fileStream, todos, _jsonSerializerOptions);
        
        File.Move(tmpFile, _filePath, overwrite: true);
    }

    private static List<TodoTask> GetInitialTodos()
    {
        return
        [
            new TodoTask
            {
                Id = 1,
                Name = "Buy groceries"
            },
            new TodoTask
            {
                Id = 2,
                Name = "Complete ASP.NET Core API project"
            },
            new TodoTask
            {
                Id = 3,
                Name = "Review pull request"
            },
            new TodoTask
            {
                Id = 4,
                Name = "Book dentist appointment"
            }
        ];
    }
}
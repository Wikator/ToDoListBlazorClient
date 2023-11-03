namespace ToDoListBlazorClient.Services.Base;

public class Response<T>
{
    public string? Message { get; set; }
    public bool IsSuccess { get; set; } = true;
    public T? Data { get; set; }
}

public class Response
{
    public string? Message { get; set; }
    public bool IsSuccess { get; set; } = true;
}

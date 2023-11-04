using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ISimpleHttpService<TResponse, in TBody>
{
    public Task<Response<IEnumerable<TResponse>>> SimpleGetAsync();
    public Task<Response<TResponse>> SimpleGetAsync(int id);
    public Task<Response<TResponse>> SimplePostAsync(TBody body);
    public Task<Response<TResponse>> SimplePutAsync(int id, TBody body);
    public Task<Response> SimpleDeleteAsync(int id);
}
using infrastructure.ApiResponse;

namespace infrastructure.Services;

public interface IServices<T>
{
    public Response<List<T>> GetAll();
    public Response<T> GetById(int id);
    public Response<string> Create(T entity);
    public Response<string> Update(T entity);
    public Response<string> Delete(int id);
}
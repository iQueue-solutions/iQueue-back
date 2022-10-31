namespace IQueueBL.Interfaces;

public interface ICrud<TModel> where TModel : class
{
    Task<IEnumerable<TModel>> GetAllAsync();

    Task<TModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(TModel model);

    Task UpdateAsync(TModel model);

    Task DeleteAsync(Guid modelId);
}
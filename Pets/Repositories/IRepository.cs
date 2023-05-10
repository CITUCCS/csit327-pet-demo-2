namespace Pets.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity, TEntity updatedEntity);
        void Delete(TEntity entity);
    }
}

using Microsoft.EntityFrameworkCore;
using Styria.Model.Music;
using Type = Styria.Model.Music.Type;

namespace Styria.API.Models.Repositories
{
    public interface ITypeRepository
    {
        Task<Type> GetTypeByName(string name);
        Task<Type> GetType(int id);
        Task<Type> AddType(Type Type);
        Task<Type?> UpdateType(Type Type);
        void DeleteType(int id);
    }

    public class TypeRepository : ITypeRepository
    {

        private readonly AppDBContext _dbContext;

        public TypeRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Type> AddType(Type Type)
        {
            var result = await _dbContext.Types.AddAsync(Type);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteType(int id)
        {
            var result = await _dbContext.Types.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _dbContext.Types.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Type> GetType(int id)
        {
            return await _dbContext.Types.FirstOrDefaultAsync(x => x.ID == id) ?? new Type();
        }

        public async Task<Type> GetTypeByName(string name)
        {
            return await _dbContext.Types.FirstOrDefaultAsync(x => x.Name == name) ?? new Type();
        }

        public async Task<Type?> UpdateType(Type Type)
        {
            var result = await _dbContext.Types.FirstOrDefaultAsync(e => e.ID != Type.ID);

            if (result != null)
            {
                result.Name = Type.Name;
                result.TypeGroupID = Type.TypeGroupID;

                return result;
            }
            return null;
        }
    }
}

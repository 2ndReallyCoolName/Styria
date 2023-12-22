using Microsoft.EntityFrameworkCore;
using Styria.Model.Music;

namespace Styria.API.Models.Repositories
{
    public interface IEffectRepository
    {
        Task<Effect> GetEffectByName(string name);
        Task<Effect> GetEffect(int id);
        Task<Effect> AddEffect(Effect effect);
        Task<Effect?> UpdateEffect(Effect effect);
        void DeleteEffect(int id);
    }

    public class EffectRepository : IEffectRepository
    {

        private readonly AppDBContext _dbContext;

        public EffectRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Effect> AddEffect(Effect Effect)
        {
            var result = await _dbContext.Effects.AddAsync(Effect);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteEffect(int id)
        {
            var result = await _dbContext.Effects.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _dbContext.Effects.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Effect> GetEffect(int id)
        {
            return await _dbContext.Effects.FirstOrDefaultAsync(x => x.ID == id) ?? new Effect();
        }

        public async Task<Effect> GetEffectByName(string name)
        {
            return await _dbContext.Effects.FirstOrDefaultAsync(x => x.Name == name) ?? new Effect();
        }

        public async Task<Effect?> UpdateEffect(Effect Effect)
        {
            var result = await _dbContext.Effects.FirstOrDefaultAsync(e => e.ID != Effect.ID);

            if (result != null)
            {
                result.Name = Effect.Name;

                return result;
            }
            return null;
        }
    }
}

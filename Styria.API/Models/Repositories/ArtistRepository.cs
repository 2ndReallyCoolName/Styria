using Microsoft.EntityFrameworkCore;
using Styria.Model.Song;

namespace Styria.API.Models.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> GetArtistByName(string name);
        Task<Artist> GetArtist(int id);
        Task<Artist> AddArtist(Artist Artist);
        Task<Artist?> UpdateArtist(Artist Artist);
        void DeleteArtist(int id);
    }

    public class ArtistRepository : IArtistRepository
    {

        private readonly AppDBContext _dbContext;

        public ArtistRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Artist> AddArtist(Artist Artist)
        {
            var result = await _dbContext.Artists.AddAsync(Artist);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteArtist(int id)
        {
            var result = await _dbContext.Artists.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _dbContext.Artists.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Artist> GetArtist(int id)
        {
            return await _dbContext.Artists.FirstOrDefaultAsync(x => x.Id == id) ?? new Artist();
        }

        public async Task<Artist> GetArtistByName(string name)
        {
            return await _dbContext.Artists.FirstOrDefaultAsync(x => x.Name == name) ?? new Artist();
        }

        public async Task<Artist?> UpdateArtist(Artist Artist)
        {
            var result = await _dbContext.Artists.FirstOrDefaultAsync(e => e.Id != Artist.Id);

            if (result != null)
            {
                result.Name = Artist.Name;

                return result;
            }
            return null;
        }
    }
}
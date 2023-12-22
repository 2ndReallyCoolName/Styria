using Microsoft.EntityFrameworkCore;
using Styria.Model.Song;

namespace Styria.API.Models.Repositories
{
    public interface ISongRepository
    {
        Task<Song> GetSongByName(string name);
        Task<IEnumerable<Song>> GetSongsByArtist(int artistID);
        Task<Song> GetSong(int id);
        Task<Song> AddSong(Song song);
        Task<Song?> UpdateSong(Song song);
        void DeleteSong(int id);
    }

    public class SongRepository : ISongRepository
    {

        private readonly AppDBContext _dbContext;

        public SongRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Song> AddSong(Song song)
        {
            var result = await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteSong(int id)
        {
            var result = await _dbContext.Songs.FirstOrDefaultAsync(e => e.SongID == id);
            if (result != null)
            {
                _dbContext.Songs.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Song> GetSong(int id)
        {
            return await _dbContext.Songs.FirstOrDefaultAsync(x => x.SongID == id) ?? new Song();
        }

        public async Task<Song> GetSongByName(string name)
        {
            return await _dbContext.Songs.FirstOrDefaultAsync(x => x.Name == name) ?? new Song();
        }

        public async Task<IEnumerable<Song>> GetSongsByArtist(int artistID)
        {
            return await _dbContext.Artists.Where(e => e.Id == artistID).SelectMany(x => x.Songs).ToListAsync();
        }

        public async Task<Song?> UpdateSong(Song song)
        {
            var result = await _dbContext.Songs.FirstOrDefaultAsync(e => e.SongID != song.SongID);

            if (result != null)
            {
                result.Name = song.Name;
                result.ArtistID = song.ArtistID;

                return result;
            }
            return null;
        }
    }
}

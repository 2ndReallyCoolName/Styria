using Microsoft.EntityFrameworkCore;
using Styria.Model.Music;

namespace Styria.API.Models.Repositories
{
    public interface ITimeSignatureRepository
    {
        Task<TimeSignature> GetTimeSignatureByAttrs(int beats, int notes);
        Task<TimeSignature> GetTimeSignature(int id);
        Task<TimeSignature> AddTimeSignature(TimeSignature TimeSignature);
        Task<TimeSignature?> UpdateTimeSignature(TimeSignature TimeSignature);
        void DeleteTimeSignature(int id);

        Task<bool> Exists(int id);
    }
    public class TimeSignatureRepository : ITimeSignatureRepository
    {
        private readonly AppDBContext _dbContext;
        public TimeSignatureRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<TimeSignature> AddTimeSignature(TimeSignature timeSignature)
        {
            var result = await _dbContext.TimeSignatures.AddAsync(timeSignature);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteTimeSignature(int id)
        {
            var result = await _dbContext.TimeSignatures.FirstOrDefaultAsync(e => e.TimeSignatureID == id);
            if (result != null)
            {
                _dbContext.TimeSignatures.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.TimeSignatures.AnyAsync(e => e.TimeSignatureID == id);
        }

        public async Task<TimeSignature> GetTimeSignature(int id)
        {
            return await _dbContext.TimeSignatures.FirstOrDefaultAsync(x => x.TimeSignatureID == id) ?? new TimeSignature();
        }

        public async Task<TimeSignature> GetTimeSignatureByAttrs(int beats, int notes)
        {
            return await _dbContext.TimeSignatures.FirstOrDefaultAsync(x => x.Beats == beats && x.NoteValue == notes) ?? new TimeSignature();
        }

        public async Task<TimeSignature?> UpdateTimeSignature(TimeSignature timeSignature)
        {
            var result = await _dbContext.TimeSignatures.FirstOrDefaultAsync(e => e.TimeSignatureID == timeSignature.TimeSignatureID);
            if (result != null)
            {
                result.NoteValue = timeSignature.NoteValue;
                result.Beats = timeSignature.Beats;

                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}

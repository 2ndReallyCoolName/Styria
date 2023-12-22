using Microsoft.EntityFrameworkCore;
using Styria.Model.Music;

namespace Styria.API.Models.Repositories
{
    public interface ITabNoteRepository
    {
        Task<IEnumerable<TabNote>> GetTabNotesByTabID(int tabId);
        Task<TabNote> GetTabNote(int id);
        Task<TabNote> AddTabNote(TabNote tabNote);
        Task<TabNote?> UpdateTabNote(TabNote tabNote);
        void DeleteTabNote(int id);
    }

    public class TabNoteRepository : ITabNoteRepository
    {

        private readonly AppDBContext _dbContext;

        public TabNoteRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<TabNote> AddTabNote(TabNote tabNote)
        {
            var result = await _dbContext.TabNotes.AddAsync(tabNote);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteTabNote(int id)
        {
            var result = await _dbContext.TabNotes.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _dbContext.TabNotes.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TabNote> GetTabNote(int id)
        {
            return await _dbContext.TabNotes.FirstOrDefaultAsync(x => x.ID == id) ?? new TabNote();
        }

        public async Task<IEnumerable<TabNote>> GetTabNotesByTabID(int tabId)
        {
            return await _dbContext.TabNotes.Where(e => e.TabID == tabId).OrderBy(e => e.Order).ToListAsync();
        }

        public async Task<TabNote?> UpdateTabNote(TabNote TabNote)
        {
            var result = await _dbContext.TabNotes.FirstOrDefaultAsync(e => e.ID != TabNote.ID);

            if (result != null)
            {
                result.Duration = TabNote.Duration;
                result.Order = TabNote.Order;
                result.EffectID = TabNote.EffectID;

                return result;
            }
            return null;
        }
    }
}

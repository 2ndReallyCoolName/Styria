using Microsoft.EntityFrameworkCore;
using Styria.Model.Intermediate;
using Styria.Model.Music;

namespace Styria.API.Models.Repositories
{
    public interface ITabRepository
    {
        Task<Tab> GetTabBySong(int songId);
        Task<Tab> GetTab(int id);
        Task<Tab> AddTab(Tab tab);
        Task<Tab?> UpdateTab(Tab tab);
        Task DeleteTab(int id);
        Task<bool> Exists(int id);
        Task<IEnumerable<TabNoteObject>> GetTabNotes(int id);
    }

    public class TabRepository : ITabRepository
    {

        private readonly AppDBContext _dbContext;

        public TabRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Tab> AddTab(Tab tab)
        {
            var result = await _dbContext.Tabs.AddAsync(tab);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteTab(int id)
        {
            var result = await _dbContext.Tabs.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _dbContext.Tabs.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Tabs.AnyAsync(e => e.ID == id);
        }

        public async Task<Tab> GetTab(int id)
        {
            return await _dbContext.Tabs.Include(x => x.TabNotes).Include(x => x.TimeSignature).FirstOrDefaultAsync(x => x.ID == id) ?? new Tab();
        }

        public async Task<Tab> GetTabBySong(int songId)
        {
            return await _dbContext.Tabs.Include(x => x.TabNotes).Include(x => x.TimeSignature).FirstOrDefaultAsync(x => x.SongID == songId) ?? new Tab();
        }
        
        public async Task<IEnumerable<TabNoteObject>> GetTabNotes(int id)
        {
            IEnumerable<int> tabNoteIDs = await _dbContext.TabNotes.Include(e => e.Effect).Where(e => e.TabID == id).Select(e => e.ID).ToListAsync() ?? new List<int>();


            List<TabNoteObject> tabNoteObjects = new List<TabNoteObject>(tabNoteIDs.Count());

            for(int i = 0; i < tabNoteIDs.Count(); i++)
            {
                tabNoteObjects.Add(
                    new TabNoteObject 
                    {
                        TabNoteID = tabNoteIDs.ElementAt(i), 
                        Notes = await _dbContext.TabNotes.Where(e => tabNoteIDs.ElementAt(i) == e.ID).Include(e => e.Notes).SelectMany(e => e.Notes).ToListAsync() ?? new List<Note>()
                    }   
                ); 
            }

            return tabNoteObjects;
        }

        public async Task<Tab?> UpdateTab(Tab tab)
        {
            var result = await _dbContext.Tabs.FirstOrDefaultAsync(e => e.ID != tab.ID);

            if (result != null)
            {
                result.TimeSignatureID = tab.TimeSignatureID;
                result.SongID = tab.SongID;

                return result;
            }
            return null;
        }
    }
}

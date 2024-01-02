using Microsoft.EntityFrameworkCore;
using Styria.Model.Intermediate;
using Styria.Model.Music;

namespace Styria.API.Models.Repositories
{
    public interface ITabNoteRepository
    {
        Task<IEnumerable<TabNote>> GetTabNotesByTabID(int tabId);
        Task<TabNote> GetTabNote(int id);
        Task<IEnumerable<Note?>> GetNotes(int id);
        Task<TabNote> AddTabNote(TabNoteCreateObject tabNoteCreateObject);
        Task<TabNote?> UpdateTabNote(TabNoteUpdateObject tabNoteUpdateObject);
        Task DeleteTabNote(int id);
        Task<bool> Exists(int id);
    }

    public class TabNoteRepository : ITabNoteRepository
    {

        private readonly AppDBContext _dbContext;

        public TabNoteRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<TabNote> AddTabNote(TabNoteCreateObject tabNoteCreateObject)
        {
            TabNote tabNote = new TabNote
            {
                Duration = tabNoteCreateObject.Duration,
                Order = tabNoteCreateObject.Order,
                EffectID = tabNoteCreateObject.EffectID,
                TabID = tabNoteCreateObject.TabID
            };

            foreach(int noteID in tabNoteCreateObject.NoteIDs)
            {
                tabNote.Notes.Add(await _dbContext.Notes.FirstAsync(e => e.ID  == noteID));
            }

            var result = await _dbContext.TabNotes.AddAsync(tabNote);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteTabNote(int id)
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
            return await _dbContext.TabNotes.Include(e => e.Effect).Include(e => e.Notes).FirstOrDefaultAsync(x => x.ID == id) ?? new TabNote();
        }

        public async Task<IEnumerable<TabNote>> GetTabNotesByTabID(int tabId)
        {
            return await _dbContext.TabNotes.Where(e => e.TabID == tabId).OrderBy(e => e.Order).ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.TabNotes.AnyAsync(x => x.ID == id);
        }


        public async Task<IEnumerable<Note?>> GetNotes(int id)
        {
            return await _dbContext.TabNotes.Where(e => e.ID == id).Include(e => e.Notes).SelectMany(e => e.Notes).ToListAsync() ?? new List<Note>();
        }

        public async Task<TabNote?> UpdateTabNote(TabNoteUpdateObject tabNoteUpdateObject)
        {
            var tabNote = await _dbContext.TabNotes.FirstOrDefaultAsync(e => e.ID == tabNoteUpdateObject.TabNoteID);

            if (tabNote != null)
            {
                tabNote.Duration = tabNoteUpdateObject.Duration;
                tabNote.Order = tabNoteUpdateObject.Order;
                tabNote.EffectID = tabNoteUpdateObject.EffectID;

                List<NoteTabNote> noteTabNotes = await _dbContext.NoteTabNote.Where(e => e.TabNoteID == tabNoteUpdateObject.TabNoteID).ToListAsync();

                foreach(NoteTabNote noteTabNote in noteTabNotes)
                {
                    _dbContext.NoteTabNote.Remove(noteTabNote);
                }

                foreach (int noteID in tabNoteUpdateObject.NoteIDs)
                {
                    tabNote.Notes.Add(await _dbContext.Notes.FirstAsync(e => e.ID == noteID));
                }

                await _dbContext.SaveChangesAsync();    

                return tabNote;
            }

            return null;

        }
    }
}

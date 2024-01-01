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
        Task<TabNote?> UpdateTabNote(TabNote tabNote);
        Task<TabNoteObject> UpdateNotes(TabNoteObject tabNoteObject);
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
                //IEnumerable<NoteTabNote> noteTabNotes = await _dbContext.NoteTabNotes.Where(e => e.TabNoteID == id).ToListAsync();

                //foreach(NoteTabNote noteTab in noteTabNotes)
                //{
                //    _dbContext.NoteTabNotes.Remove(noteTab);
                //}

                _dbContext.TabNotes.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TabNote> GetTabNote(int id)
        {
            return await _dbContext.TabNotes.Include(e => e.Effect).FirstOrDefaultAsync(x => x.ID == id) ?? new TabNote();
        }

        public async Task<IEnumerable<TabNote>> GetTabNotesByTabID(int tabId)
        {
            return await _dbContext.TabNotes.Where(e => e.TabID == tabId).OrderBy(e => e.Order).ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.TabNotes.AnyAsync(x => x.ID == id);
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

        public async Task<IEnumerable<Note?>> GetNotes(int id)
        {
            return await _dbContext.TabNotes.Where(e => e.ID == id).Include(e => e.Notes).SelectMany(e => e.Notes).ToListAsync() ?? new List<Note>();
        }

        public async Task< TabNoteObject> UpdateNotes(TabNoteObject tabNoteObject)
        {
            //IEnumerable<NoteTabNote> noteTabNotes = await _dbContext.TabN.Where(e => e.TabNoteID == tabNoteObject.TabNoteID).ToListAsync();

            //foreach (NoteTabNote noteTab in noteTabNotes)
            //{
            //    _dbContext.NoteTabNotes.Remove(noteTab);
            //}

            foreach(Note note in tabNoteObject.Notes)
            {
                Note _n = await _dbContext.Notes.FirstOrDefaultAsync(e => e.String == note.String && e.TypeID == note.TypeID && e.Fret == note.Fret) ?? new Note();
                //if(_n != null)
                //{
                //    await _dbContext.NoteTabNotes.AddAsync(new NoteTabNote { NoteID = _n.ID, TabNoteID = tabNoteObject.TabNoteID});
                //}
            }

            return new TabNoteObject();

        }
    }
}

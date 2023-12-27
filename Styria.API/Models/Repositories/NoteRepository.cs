using Microsoft.EntityFrameworkCore;
using Styria.Model.Music;

namespace Styria.API.Models.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetNoteByFretString(int fret, char _string);
        Task<Note> GetNote(int id);
        Task<IEnumerable<Note>> GetNotesByTabNoteID(int tabNoteID);
        Task<Note> AddNote(Note Note);
        Task<Note?> UpdateNote(Note Note);
        void DeleteNote(int id);
    }

    public class NoteRepository : INoteRepository
    {

        private readonly AppDBContext _dbContext;

        public NoteRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Note> AddNote(Note Note)
        {
            var result = await _dbContext.Notes.AddAsync(Note);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteNote(int id)
        {
            var result = await _dbContext.Notes.FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                _dbContext.Notes.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Note> GetNote(int id)
        {
            return await _dbContext.Notes.FirstOrDefaultAsync(x => x.ID == id) ?? new Note();
        }

        public async Task<Note> GetNoteByFretString(int fret, char _string)
        {
            return await _dbContext.Notes.FirstOrDefaultAsync(x => x.Fret == fret && x.String == _string) ?? new Note();
        }

        public async Task<IEnumerable<Note>> GetNotesByTabNoteID(int tabNoteID)
        {
            return await _dbContext.TabNotes.Where(e => e.ID == tabNoteID).Include(e => e.Notes).SelectMany(e => e.Notes).ToListAsync();
        }

        public async Task<Note?> UpdateNote(Note Note)
        {
            var result = await _dbContext.Notes.FirstOrDefaultAsync(e => e.ID != Note.ID);

            if (result != null)
            {
                result.String = Note.String;
                result.Fret = Note.Fret;
                result.InstrumentID = Note.InstrumentID;    
                result.SoundFilePath = Note.SoundFilePath;
                return result;
            }
            return null;
        }
    }
}

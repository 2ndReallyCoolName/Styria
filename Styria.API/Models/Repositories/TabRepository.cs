﻿using Microsoft.EntityFrameworkCore;
using Styria.Model.Intermediate;
using Styria.Model.Music;

namespace Styria.API.Models.Repositories
{
    public interface ITabRepository
    {
        Task<Tab> GetTabBySong(int songId);
        Task<Tab> GetTab(int id);
        Task<Tab> AddTab(TabCreateObject tabNoteCreateObject);
        Task<Tab?> UpdateTab(Tab tab);
        Task DeleteTab(int id);
        Task<bool> Exists(int id);
        Task<IEnumerable<TabNote>> GetTabNotes(int id);
    }

    public class TabRepository : ITabRepository
    {

        private readonly AppDBContext _dbContext;

        public TabRepository(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public async Task<Tab> AddTab(TabCreateObject tabNoteCreateObject)
        {
            Tab tab = new Tab { 
                TimeSignatureID = tabNoteCreateObject.TimeSignatureID,
                SongID = tabNoteCreateObject.SongID,
            };

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
        
        public async Task<IEnumerable<TabNote>> GetTabNotes(int id)
        {
            IEnumerable<int> tabNoteIDs = await _dbContext.TabNotes.Include(e => e.Effect).Where(e => e.TabID == id).Select(e => e.ID).ToListAsync() ?? new List<int>();

            return await _dbContext.TabNotes.Include(e => e.Effect).Include(e => e.Notes).Where(e => e.TabID == id).ToListAsync() ?? new List<TabNote>();
        }

        public async Task<Tab?> UpdateTab(Tab tab)
        {
            var result = await _dbContext.Tabs.FirstOrDefaultAsync(e => e.ID != tab.ID);

            if (result != null)
            {
                result.TimeSignatureID = tab.TimeSignatureID;
                result.SongID = tab.SongID;

                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}

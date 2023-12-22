namespace Styria.API.Models.Repositories
{
    using global::Styria.Model.Music;
    using global::Styria.Model.Song;
    using Microsoft.EntityFrameworkCore;

    namespace Styria.API.Models.Repositories
    {
        public interface IInstrumentRepository
        {
            Task<Instrument> GetInstrumentByName(string name);
            Task<Instrument> GetInstrument(int id);
            Task<Instrument> AddInstrument(Instrument instrument);
            Task<Instrument?> UpdateInstrument(Instrument instrument);
            void DeleteInstrument(int id);
        }

        public class InstrumentRepository : IInstrumentRepository
        {

            private readonly AppDBContext _dbContext;

            public InstrumentRepository(AppDBContext appDBContext)
            {
                _dbContext = appDBContext;
            }

            public async Task<Instrument> AddInstrument(Instrument instrument)
            {
                var result = await _dbContext.Instruments.AddAsync(instrument);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }

            public async void DeleteInstrument(int id)
            {
                var result = await _dbContext.Instruments.FirstOrDefaultAsync(e => e.Id == id);
                if (result != null)
                {
                    _dbContext.Instruments.Remove(result);
                    await _dbContext.SaveChangesAsync();
                }
            }

            public async Task<Instrument> GetInstrument(int id)
            {
                return await _dbContext.Instruments.FirstOrDefaultAsync(x => x.Id == id) ?? new Instrument();
            }

            public async Task<Instrument> GetInstrumentByName(string name)
            {
                return await _dbContext.Instruments.FirstOrDefaultAsync(x => x.Name == name) ?? new Instrument();
            }

            public async Task<Instrument?> UpdateInstrument(Instrument instrument)
            {
                var result = await _dbContext.Instruments.FirstOrDefaultAsync(e => e.Id != instrument.Id);

                if (result != null)
                {
                    result.Name = instrument.Name;

                    return result;
                }
                return null;
            }
        }
    }
}

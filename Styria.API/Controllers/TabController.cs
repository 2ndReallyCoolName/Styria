using Microsoft.AspNetCore.Mvc;
using Styria.API.Models.Repositories;
using Styria.Model.Intermediate;
using Styria.Model.Music;
using System.Runtime.CompilerServices;

namespace Styria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabController : ControllerBase
    {
        private readonly ITabRepository _tabRepository;
        private readonly ITabNoteRepository _tabNoteRepository;
        private readonly ITimeSignatureRepository _timeSignatureRepository;

        public TabController(ITabRepository tabRepository, ITabNoteRepository tabNoteRepository,
            INoteRepository noteRepository, ITimeSignatureRepository timeSignatureRepository)
        {
            _tabRepository = tabRepository;
            _tabNoteRepository = tabNoteRepository;
            _timeSignatureRepository = timeSignatureRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tab>> GetTab(int id)
        {
            try
            {
                var result = await _tabRepository.GetTab(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}/notes")]
        public async Task<ActionResult<IEnumerable<TabNoteObject>>> GetNotes(int id) {
            try
            {
                var result = await _tabRepository.GetTabNotes(id);

                return Ok(result);

            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Tab>> CreateTab(TabCreateObject tabCreateObject)
        {
            try
            {
                if(tabCreateObject == null) { return BadRequest(); }

                var result = await _tabRepository.AddTab(tabCreateObject);

                foreach (TabNoteCreateObject tabNoteCreateObject in tabCreateObject.TabNoteCreateObjects)
                {
                    tabNoteCreateObject.TabID = result.ID;
                    await _tabNoteRepository.AddTabNote(tabNoteCreateObject);
                }

                return CreatedAtAction(nameof(GetTab), new {ID = result.ID}, result);
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]   
        public async Task<ActionResult<Tab>> UpdateTab(int  id, Tab tab)
        {
            try
            {
                if(id != tab.ID) { return BadRequest(); }

                if (! await _tabRepository.Exists(id)) { return NotFound();  }

                var result = await _tabRepository.UpdateTab(tab);

                if(result == null) { return StatusCode(StatusCodes.Status500InternalServerError); }

                return result;


            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Tab>> DeleteTab(int id)
        {
            try
            {
                if (! await _tabRepository.Exists(id))
                {
                    return NotFound();
                }

                IEnumerable<TabNote> tabNotes = await _tabNoteRepository.GetTabNotesByTabID(id);

                foreach(TabNote tabNote in tabNotes)
                {
                    await _tabNoteRepository.DeleteTabNote(tabNote.ID);
                }

                await _tabRepository.DeleteTab(id);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }

}

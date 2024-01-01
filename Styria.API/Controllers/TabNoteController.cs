using Microsoft.AspNetCore.Mvc;
using Styria.API.Models.Repositories;
using Styria.Model.Intermediate;
using Styria.Model.Music;

namespace Styria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabNoteController : ControllerBase
    {
        private readonly ITabNoteRepository _tabNoteRepository;

        public TabNoteController(ITabNoteRepository tabNoteRepository)
        {
            _tabNoteRepository = tabNoteRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TabNote>> GetTabNote(int id)
        {
            try
            {
                var result = await _tabNoteRepository.GetTabNote(id);
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
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes(int id)
        {
            try
            {
                var result = await _tabNoteRepository.GetNotes(id);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TabNote>> CreateTabNote(TabNoteCreateObject tabNoteCreateObject)
        {
            try
            {
                if (tabNoteCreateObject == null) { return BadRequest(); }

                var result = await _tabNoteRepository.AddTabNote(tabNoteCreateObject);

                return CreatedAtAction(nameof(GetTabNote), new { ID = result.ID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TabNote>> UpdateTabNote(int id, TabNote tabNote)
        {
            try
            {
                if (id != tabNote.ID) { return BadRequest(); }

                if (!await _tabNoteRepository.Exists(id)) { return NotFound(); }

                var result = await _tabNoteRepository.UpdateTabNote(tabNote);

                if (result == null) { return StatusCode(StatusCodes.Status500InternalServerError); }

                return result;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id:int}/notes")]
        public async Task<ActionResult<TabNoteObject>> UpdateNotes(int id, TabNoteObject tabNote)
        {
            try
            {
                if (id != tabNote.TabNoteID && ! await _tabNoteRepository.Exists(id)) { return BadRequest(); }

                if (!await _tabNoteRepository.Exists(id)) { return NotFound(); }

                var result = await _tabNoteRepository.UpdateNotes(tabNote);

                if (result == null) { return StatusCode(StatusCodes.Status500InternalServerError); }

                return result;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}

using Core.Services.Contracts;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElearningPlatformAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TermsController : ControllerBase
    {
        private readonly ITermService _termService;

        public TermsController(ITermService termService)
        {
            _termService = termService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TermDto>>> GetTerms()
        {
            var terms = await _termService.GetAllTermsAsync();
            return Ok(terms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TermDto>> GetTerm(Guid id)
        {
            var term = await _termService.GetTermByIdAsync(id);
            if (term == null) return NotFound();
            return Ok(term);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTerm(TermDto termDto)
        {
            var term = new Term
            {
                TermId = Guid.NewGuid(),
                LevelId = termDto.LevelId,
                TermName = termDto.TermName,
                StartDate = termDto.StartDate,
                EndDate = termDto.EndDate
            };
            await _termService.AddTermAsync(term);
            return CreatedAtAction(nameof(GetTerm), new { id = term.TermId }, term);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTerm(Guid id, TermDto termDto)
        {
            var term = await _termService.GetTermByIdAsync(id);
            if (term == null) return NotFound();

            term.LevelId = termDto.LevelId;
            term.TermName = termDto.TermName;
            term.StartDate = termDto.StartDate;
            term.EndDate = termDto.EndDate;

            await _termService.UpdateTermAsync(term);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTerm(Guid id)
        {
            await _termService.DeleteTermAsync(id);
            return NoContent();
        }
    }
}

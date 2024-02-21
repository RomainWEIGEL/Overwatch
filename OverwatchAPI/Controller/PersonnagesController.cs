using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Models.EntityFramework;

namespace OverwatchAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnagesController : ControllerBase
    {
        private readonly MapDBContext _context;

        public PersonnagesController(MapDBContext context)
        {
            _context = context;
        }

        // GET: api/Personnages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personnage>>> GetPersonnages()
        {
          if (_context.Personnages == null)
          {
              return NotFound();
          }
            return await _context.Personnages.ToListAsync();
        }

        // GET: api/Personnages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personnage>> GetPersonnage(int id)
        {
          if (_context.Personnages == null)
          {
              return NotFound();
          }
            var personnage = await _context.Personnages.FindAsync(id);

            if (personnage == null)
            {
                return NotFound();
            }

            return personnage;
        }

        // GET: api/t_e_utilisateur_utl/5
        [HttpGet("Name")]
        public async Task<ActionResult<Personnage>> GetPersonnageByName(string nom)
        {
            if (_context.Personnages == null)
            {
                return NotFound();
            }

            var listutilisateur = await _context.Personnages.ToListAsync();

            foreach (Personnage ut in listutilisateur)
            {
                if (ut.Nom == nom)
                {
                    return ut;
                }
            }

            return NotFound();
        }

        // PUT: api/Personnages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonnage(int id, string nom, string prenom, string pays, int note)
        {
            Personnage personnage = new Personnage(id, nom, prenom, pays, note);
            if (id != personnage.PersonnageId)
            {
                return BadRequest();
            }

            _context.Entry(personnage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personnages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Personnage>> PostSerie(string nom, string prenom, string pays, int note)
        {
            Personnage perso = new Personnage(nom, prenom, pays, note);
            _context.Personnages.Add(perso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerso", new { id = perso.PersonnageId }, perso);
        }

        // DELETE: api/Personnages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonnage(int id)
        {
            if (_context.Personnages == null)
            {
                return NotFound();
            }
            var personnage = await _context.Personnages.FindAsync(id);
            if (personnage == null)
            {
                return NotFound();
            }

            _context.Personnages.Remove(personnage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonnageExists(int id)
        {
            return (_context.Personnages?.Any(e => e.PersonnageId == id)).GetValueOrDefault();
        }
    }
}

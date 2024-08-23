using EscolaDeIdiomas.Models;
using EscolaDeIdiomas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeIdiomas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        [HttpGet]
        [Route("Todas")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        public IEnumerable<Turma> GetAlunosTurmas()
        {
            return TurmaRepository.Turmas;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetTurmaPorId(int id)
        {
            if (id <= 0)
            { 
                return BadRequest($"A Id não pode ser 0 ou negativa"); 
            }

            var turma = TurmaRepository.Turmas.FirstOrDefault(n => n.Id == id);
            if (turma == null)
            {
                return NotFound($"Turma com a Id {id} não encontrado");
            }

            return Ok(turma);
        }

        [HttpGet("idioma/{idioma}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetTurmaPorIdioma(string idioma)
        {
            if (string.IsNullOrEmpty(idioma))
            {
                return BadRequest($"O idioma da turma não pode ser encontrado");
            }

            var turma = TurmaRepository.Turmas.FirstOrDefault(n => n.Idioma.Equals(idioma, StringComparison.InvariantCultureIgnoreCase));
            if (turma == null)
            {
                return NotFound($"A turma com a lição {idioma} não foi encontrado");
            }

            return Ok(turma);
        }

        [HttpGet("nivel/{nivel}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetTurmaPorNivel(string nivel)
        {
            if (string.IsNullOrEmpty(nivel))
            {
                return BadRequest($"O nivel da turma não pode ser encontrado");
            }

            var turma = TurmaRepository.Turmas.FirstOrDefault(n => n.Nivel.Equals(nivel, StringComparison.InvariantCultureIgnoreCase));
            if (turma == null)
            {
                return NotFound($"A(s) turma(s) com o nível {nivel} não foi(foram) encontrado(s)");
            }

            return Ok(turma);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<bool> DeleteTurma(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"A Id não pode ser 0 ou negativa");
            }

            var turma = TurmaRepository.Turmas.FirstOrDefault(n => n.Id == id);
            if (turma == null)
            { 
                return NotFound($"Turma com a Id {id} não encontrado"); 
            }

            TurmaRepository.Turmas.Remove(turma);

            return true;
        }
    }
}

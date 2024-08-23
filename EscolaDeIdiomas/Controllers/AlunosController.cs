using EscolaDeIdiomas.Models;
using EscolaDeIdiomas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeIdiomas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : Controller
    {
        [HttpGet]
        [Route("Todos")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Aluno>))]
        public ActionResult GetAlunos()
        {
            return Ok(AlunoRepository.Alunos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetAlunoPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"A Id não pode ser 0 ou negativa");
            }

            var aluno = AlunoRepository.Alunos.FirstOrDefault(n => n.Id == id);
            if (aluno == null)
            {
                return NotFound($"Aluno com a Id {id} não encontrado");
            }

            return Ok(aluno);
        }

        [HttpGet("{nome}")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetAlunoPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest($"O nome do aluno não pode ser vazio");
            }

            var aluno = AlunoRepository.Alunos.FirstOrDefault(n => n.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase));
            if (aluno == null)
            {
                return NotFound($"Aluno com o nome {nome} não foi encontrado");
            }

            return Ok(aluno);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<bool> DeleteAluno(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"A Id não pode ser 0 ou negativa");
            }

            var aluno = AlunoRepository.Alunos.FirstOrDefault(n => n.Id == id);
            if (aluno == null)
            {
                return NotFound($"Aluno com a Id {id} não encontrado");
            }

            AlunoRepository.Alunos.Remove(aluno);

            return true;
        }
    }
}

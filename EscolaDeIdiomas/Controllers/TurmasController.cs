using AutoMapper;
using EscolaDeIdiomas.Dto;
using EscolaDeIdiomas.Interfaces;
using EscolaDeIdiomas.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeIdiomas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : Controller
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMapper _mapper;

        public TurmasController(ITurmaRepository turmaRepository, IMapper mapper)
        {
            _turmaRepository = turmaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Todas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Turma>))]
        public IActionResult GetTodasTurmas() // Achar todas as Turmas existentes
        {
            var turmas = _mapper.Map<List<TurmaDto>>(_turmaRepository.GetTodasTurmas());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(turmas);
        }


        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetTurmaPorId(int id) // Achar a Turma pelo Id
        {
            if (!_turmaRepository.TurmaExiste(id))
            {
                return BadRequest("A Turma não existe");
            }

            var turma = _mapper.Map<TurmaDto>(_turmaRepository.GetTurmaPorId(id));

            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(turma);
        }

        [HttpGet("idioma/{idioma}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetTurmaPorIdioma(string idioma) // Achar a Turma pelo idioma
        {
            if (string.IsNullOrEmpty(idioma))
            {
                return BadRequest("Idioma da turma não existe");
            }

            var turma = _mapper.Map<List<TurmaDto>>(_turmaRepository.GetTurmaPorIdioma(idioma));

            if (turma == null)
            {
                return NotFound("Idioma da turma não encontrado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(turma);
        }

        [HttpGet("nivel/{nivel}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetTurmaPorNivel(string nivel) // Achar a Turma pelo nivel
        {
            if (string.IsNullOrEmpty(nivel))
            {
                return BadRequest("Nivel da turma não existe");
            }

            var turma = _mapper.Map<List<TurmaDto>>(_turmaRepository.GetTurmaPorNivel(nivel));

            if (turma == null)
            {
                return NotFound("Nivel da turma não encontrado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(turma);
        }

        [HttpGet("alunos/{turmaId}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetAlunosPorTurma(int turmaId) // Achar os alunos matriculados nessa turma
        {
            if (!_turmaRepository.TurmaExiste(turmaId))
            {
                return BadRequest("A Turma não existe");
            }

            var turma = _mapper.Map<List<AlunoDto>>(_turmaRepository.GetAlunoPorTurma(turmaId));

            if (turma == null)
            {
                return NotFound("A Turma não foi encontrada");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(turma);
        }

        [HttpPost("CriarTurma")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public IActionResult CreateTurma([FromBody] TurmaDto criarTurma) // Criar uma turma
        {
            if (criarTurma == null)
            {
                return BadRequest(ModelState);
            }

            var numero = _turmaRepository.GetTodasTurmas().FirstOrDefault(t => t.Numero == criarTurma.Numero);

            if (numero != null)
            {
                ModelState.AddModelError("", "Número da Turma já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var turmaMap = _mapper.Map<Turma>(criarTurma);

            if (!_turmaRepository.CreateTurma(turmaMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado durante o salvamento");
                return StatusCode(500, ModelState);
            }

            return Ok("Turma criada com sucesso");
        }

        [HttpPut("ModificarTurma/{turmaId}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTurma(int turmaId, [FromBody] TurmaDto turmaModificada) // Atualizar as informações da turma
        {
            if (turmaModificada == null)
            {
                return BadRequest(ModelState);
            }

            var numero = _turmaRepository.GetTodasTurmas().FirstOrDefault(t => t.Numero == turmaModificada.Numero);

            if (numero != null && numero.Id != turmaId)
            {
                ModelState.AddModelError("", "Número da Turma já existe");
                return StatusCode(422, ModelState);
            }

            if (turmaId != turmaModificada.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_turmaRepository.TurmaExiste(turmaId))
            {
                return NotFound("A Turma não existe");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var turmaMap = _mapper.Map<Turma>(turmaModificada);

            if (!_turmaRepository.UpdateTurma(turmaMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado");
                return StatusCode(500, ModelState);
            }

            return Ok("Turma modificada com sucesso");
        }

        [HttpDelete("DeletarTurma/{turmaId}")]
        [ProducesResponseType(200, Type = typeof(Turma))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTurma(int turmaId) // Deletar a turma que não tenha alunos
        {
            if (!_turmaRepository.TurmaExiste(turmaId))
            {
                return BadRequest("Turma não existe");
            }

            var deleteTurma = _turmaRepository.GetTurmaPorId(turmaId);
            var temAluno = _turmaRepository.TemAluno(turmaId);

            if (temAluno >= 1)
            {
                return BadRequest("Não pudemos deletar porque a turma tem aluno/s vinculados");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_turmaRepository.DeleteTurma(deleteTurma))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado");
            }

            return Ok("Turma deletada com sucesso");
        }
    }
}

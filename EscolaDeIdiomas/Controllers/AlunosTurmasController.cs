using AutoMapper;
using EscolaDeIdiomas.Dto;
using EscolaDeIdiomas.Interfaces;
using EscolaDeIdiomas.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeIdiomas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosTurmasController : Controller
    {
        private readonly IAlunosTurmasRepository _alunosTurmasRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMapper _mapper;

        public AlunosTurmasController(IAlunosTurmasRepository alunosTurmasRepository, IAlunoRepository alunoRepository, ITurmaRepository turmaRepository, IMapper mapper)
        {
            _alunosTurmasRepository = alunosTurmasRepository;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _mapper = mapper;
        }

        [HttpPost("AdicionarAlunoNaTurma")]
        [ProducesResponseType(200, Type = typeof(AlunosTurmas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public IActionResult CadastrarAlunoNaTurma(int alunoId, int turmaId) // Matricula o aluno na turma
        {
            if (!_alunoRepository.AlunoExiste(alunoId))
            {
                return NotFound("Aluno não existe");
            }

            if (!_turmaRepository.TurmaExiste(turmaId))
            {
                return BadRequest("Turma não existe");
            }

            if (_alunosTurmasRepository.VerificarMatricula(alunoId, turmaId))
            {
                return BadRequest("Aluno já está nesta turma");
            }

            var quantidade = _alunosTurmasRepository.QuantidadeDeAluno(turmaId);

            if (quantidade >= 5)
            {
                return BadRequest("Turma cheia, o máximo de alunos permitidos por turma é 5");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var matricula = new AlunosTurmasDto { AlunoId = alunoId, TurmaId = turmaId };

            if (matricula == null)
            {
                return BadRequest(ModelState);
            }

            var alunosTurmasMap = _mapper.Map<AlunosTurmas>(matricula);

            if (!_alunosTurmasRepository.MatricularAlunoNaTurma(alunosTurmasMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado");
                return StatusCode(500, ModelState);
            }

            return Ok("Aluno matriculado com sucesso");
        }

        [HttpDelete("DesvincularAlunoDaTurma")]
        [ProducesResponseType(200, Type = typeof(AlunosTurmas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DesvincularAlunoTurma(int alunoId, int turmaId) // Desvincula o aluno da turma
        {
            if (!_alunoRepository.AlunoExiste(alunoId))
            {
                return NotFound("Aluno não existe");
            }

            if (!_turmaRepository.TurmaExiste(turmaId))
            {
                return BadRequest("Turma não existe");
            }

            if (!_alunosTurmasRepository.VerificarMatricula(alunoId, turmaId))
            {
                return BadRequest("Aluno não está matriculado nesta turma");
            }

            var desvincularaluno = _alunoRepository.GetAlunoPorId(alunoId);
            var desvincularturma = _turmaRepository.GetTurmaPorId(turmaId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var desvincular = new AlunosTurmas { AlunoId = alunoId, TurmaId = turmaId };

            if (!_alunosTurmasRepository.DesvincularAlunoTurma(desvincular))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado");
            }

            return Ok("Aluno desvinculado com sucesso");
        }
    }
}

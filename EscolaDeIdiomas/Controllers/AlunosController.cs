using AutoMapper;
using EscolaDeIdiomas.Dto;
using EscolaDeIdiomas.Interfaces;
using EscolaDeIdiomas.Models;
using EscolaDeIdiomas.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeIdiomas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : Controller
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunosTurmasRepository _alunosTurmasRepository;
        private readonly IMapper _mapper;

        public AlunosController(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository, IAlunosTurmasRepository alunosTurmasRepository, IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _alunosTurmasRepository = alunosTurmasRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Todos")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Aluno>))]
        public IActionResult GetTodosAlunos() // Achar todos os alunos existentes
        {
            var alunos = _mapper.Map<List<AlunoDto>>(_alunoRepository.GetTodosAlunos());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(alunos);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAlunoPorId(int id) // Achar aluno pela Id
        {
            if (!_alunoRepository.AlunoExiste(id))
            {
                return BadRequest("O aluno não existe");
            }

            var aluno = _mapper.Map<AlunoDto>(_alunoRepository.GetAlunoPorId(id));

            if (aluno == null)
            {
                return NotFound("O aluno não foi encontrado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(aluno);
        }

        [HttpGet("nome/{nome}")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetAlunoPorNome(string nome) // Achar aluno pelo nome
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("O aluno não existe ou não foi encontrado, tente novamente com o nome completo do aluno");
            }

            var aluno = _mapper.Map<AlunoDto>(_alunoRepository.GetAlunoPorNome(nome));

            if (aluno == null)
            {
                return NotFound("O aluno não foi encontrado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(aluno);
        }

        [HttpGet("turmas/{alunoId}")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetAlunosPorTurma(int alunoId) // Achar as turmas que o aluno está matriculado
        {
            if (!_alunoRepository.AlunoExiste(alunoId))
            {
                return BadRequest("O aluno não existe");
            }

            var aluno = _mapper.Map<List<TurmaDto>>(_alunoRepository.GetTurmasMatriculadasDoAluno(alunoId));

            if (aluno == null)
            {
                return NotFound("O aluno não foi encontrado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(aluno);
        }

        [HttpPost("CadastrarAluno")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public IActionResult CreateAluno([FromQuery] int turmaId, [FromBody] AlunoDto criarAluno) // Criar um aluno e cadastra-lo a uma turma
        {
            if (criarAluno == null)
            {
                return BadRequest(ModelState);
            }

            if (!criarAluno.CPF.ValidaCPF())
            {
                return BadRequest("CPF Inválido, por favor digite um CPF Válido ou sem as pontuações. Ex: 12345678910");
            }

            if (!criarAluno.Email.ValidaEmail())
            {
                return BadRequest("Email Inválido");
            }

            var cpf = _alunoRepository.GetTodosAlunos().FirstOrDefault(t => t.CPF == criarAluno.CPF);
            var nome = _alunoRepository.GetTodosAlunos().FirstOrDefault(t => t.Nome.Trim().ToUpper() == criarAluno.Nome.Trim().ToUpper());
            var email = _alunoRepository.GetTodosAlunos().FirstOrDefault(t => t.Email.Trim().ToUpper() == criarAluno.Email.Trim().ToUpper());
            var quantidade = _alunosTurmasRepository.QuantidadeDeAluno(turmaId);

            if (quantidade >= 5)
            {
                return BadRequest("Turma cheia, o máximo de alunos permitidos por turma é 5");
            }

            if (cpf != null)
            {
                ModelState.AddModelError("", "Esse CPF já está cadastrado");
                return StatusCode(422, ModelState);
            }
            else if (nome != null)
            {
                ModelState.AddModelError("", "Esse Nome já está cadastrado");
                return StatusCode(422, ModelState);
            }
            else if (email != null)
            {
                ModelState.AddModelError("", "Esse Email já está cadastrado");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alunoMap = _mapper.Map<Aluno>(criarAluno);

            if (!_turmaRepository.TurmaExiste(turmaId))
            {
                ModelState.AddModelError("", "Turma não existe");
                return StatusCode(404, ModelState);
            }

            if (!_alunoRepository.CreateAluno(turmaId, alunoMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado durante o salvamento");
                return StatusCode(500, ModelState);
            }

            return Ok("Aluno cadastrado com sucesso");
        }

        [HttpPut("ModificarAluno/{alunoId}")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAluno(int alunoId, [FromBody] AlunoDto alunoModificado) // Atualizar as informações de Nome, CPF e Email do aluno
        {
            if (alunoModificado == null)
            {
                return BadRequest(ModelState);
            }
            if (!_alunoRepository.AlunoExiste(alunoId))
            {
                return NotFound("Aluno não existe");
            }

            if (!alunoModificado.CPF.ValidaCPF())
            {
                return BadRequest("CPF Inválido");
            }

            if (!alunoModificado.Email.ValidaEmail())
            {
                return BadRequest("Email Inválido");
            }

            if (alunoId != alunoModificado.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var AlunoMap = _mapper.Map<Aluno>(alunoModificado);

            if (!_alunoRepository.UpdateAluno(AlunoMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado");
                return StatusCode(500, ModelState);
            }

            return Ok("Aluno modificado com sucesso");
        }

        [HttpDelete("DeletarAluno/{alunoId}")]
        [ProducesResponseType(200, Type = typeof(Aluno))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAluno(int alunoId) // Deletar o aluno que esteja sem turma
        {
            if (!_alunoRepository.AlunoExiste(alunoId))
            {
                return BadRequest("Aluno não existe");
            }

            var deleteAluno = _alunoRepository.GetAlunoPorId(alunoId);
            var temTurma = _alunoRepository.TemTurma(alunoId);

            if (temTurma >= 1)
            {
                return BadRequest("Não pudemos deletar porque o aluno está cadastrado em uma turma");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_alunoRepository.DeleteAluno(deleteAluno))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado");
            }

            return Ok("Aluno deletado com sucesso");
        }
    }
}

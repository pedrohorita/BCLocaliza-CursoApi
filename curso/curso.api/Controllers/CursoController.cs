using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// Este serviço permite cadastrar um curso para o usuário autenticado.
        /// </summary>
        /// <param name="cursoViewModelInput"></param>
        /// <returns>Retorna 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            Curso curso = new Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;            
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.CodigoUsuario = codigoUsuario;

            _cursoRepository.Add(curso);
            _cursoRepository.Commit();

            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os cursos ativos do usuário
        /// </summary>
        /// <returns>Retorna status e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var cursos = new List<CursoViewModelOutput>();                      

            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            _cursoRepository.GetByUser(codigoUsuario).Select(c => new CursoViewModelOutput()
            {
                Nome = c.Nome,
                Descricao = c.Descricao,
                Login = c.Usuario.Login
            });


            return Ok(cursos);
        }
    }
}

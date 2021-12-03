using AutoBogus;
using curso.api.Models.Cursos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
    public class CursoControllerTests : UserControllerTests
    {
        public CursoControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
            :base(factory, output)
        {            
        }

        [Fact]
        public async Task Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso()
        {
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();

            StringContent content = new(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            var httpClientResponse = await _httpClient.PostAsync("api/v1/cursos", content);

            _output.WriteLine(await httpClientResponse.Content.ReadAsStringAsync());           
                        
            Assert.Equal(HttpStatusCode.Created, httpClientResponse.StatusCode);
            
        }

        [Fact]
        public async Task Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioNaoAutenticado_DeveRetornarSucesso()
        {
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();

            StringContent content = new(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            var httpClientResponse = await _httpClient.PostAsync("api/v1/cursos", content);

            _output.WriteLine(await httpClientResponse.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.Unauthorized, httpClientResponse.StatusCode);

        }

        [Fact]
        public async Task Obter_InformandoUmUsuarioAutenticado_DeveRetornarSucesso()
        {
            await Registrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            var httpClientResponse = await _httpClient.GetAsync("api/v1/cursos");

            var curso = JsonConvert.DeserializeObject<IList<CursoViewModelOutput>>(await httpClientResponse.Content.ReadAsStringAsync());

            _output.WriteLine(await httpClientResponse.Content.ReadAsStringAsync());    
            Assert.Equal(HttpStatusCode.OK, httpClientResponse.StatusCode);
            Assert.NotEmpty(curso);
        }
    }
}

using AutoBogus;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;
        private readonly ITestOutputHelper _output;
        protected RegisterViewModelInput RegisterViewModelInput;
        public UserControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
            _output = output;
        }
                

        public async Task InitializeAsync()
        {
            await Registrar_InformandoUsuarioESenha_DeveRetornarSucesso();
        }

        [Fact]
        public async void Logar_InformandoUsuarioESenhaExistentes_DeveRetornarSucesso()
        {
            var loginViewModelInput = new LoginViewModelInput
            {
                Login = RegisterViewModelInput.Login,
                Password = RegisterViewModelInput.Password
            };

            StringContent content = new(JsonConvert.SerializeObject(loginViewModelInput), Encoding.UTF8, "application/json");

            var httpClientResponse = await _httpClient.PostAsync("api/v1/users/login", content);

            var loginViewModelOutput = JsonConvert.DeserializeObject<LoginViewModelOutput>(await httpClientResponse.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, httpClientResponse.StatusCode);
            Assert.NotNull(loginViewModelOutput.Token);
            Assert.Equal(loginViewModelInput.Login, loginViewModelOutput.User.Login);
            _output.WriteLine(loginViewModelOutput.Token);
        }

        
        public async Task Registrar_InformandoUsuarioESenha_DeveRetornarSucesso()
        {
            RegisterViewModelInput = new AutoFaker<RegisterViewModelInput>()
                                            .RuleFor(p => p.Email, faker => faker.Person.Email);


            StringContent content = new(JsonConvert.SerializeObject(RegisterViewModelInput), Encoding.UTF8, "application/json");

            var httpClientResponse = await _httpClient.PostAsync("api/v1/users/register", content);

            _output.WriteLine(await httpClientResponse.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.Created, httpClientResponse.StatusCode);
        }


        public async Task DisposeAsync()
        {
            _httpClient.Dispose();
        }

    }
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebPrimeiraAlula.Models;
using WebPrimeiraAula.Models;

namespace WebPrimeiraAlula.Servico
{
    public class ApiToken
    {

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<loginRespostaModel> _loginRespostaModel;

        public ApiToken(IOptions<DadosBase> dadosBase, IOptions<loginRespostaModel> loginRespostaModel)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
        }

        private void ObterToken()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            loginRequisicaoModel loginRequisicaoModel = new loginRequisicaoModel();
            loginRequisicaoModel.Usuario = "UsuarioDevPratica";
            loginRequisicaoModel.Senha = "SenhaDevPratica";

            HttpResponseMessage response = client.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Login", loginRequisicaoModel).Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                loginRespostaModel loginRespostaModel = JsonConvert.DeserializeObject<loginRespostaModel>(conteudo); 

                if(loginRespostaModel.Autenticado == true)
                {
                    _loginRespostaModel.Value.Autenticado = loginRespostaModel.Autenticado;
                    _loginRespostaModel.Value.Usuario = loginRespostaModel.Usuario;
                    _loginRespostaModel.Value.DataExpiracao = loginRespostaModel.DataExpiracao;
                    _loginRespostaModel.Value.Token = loginRespostaModel.Token;
                }
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }
        }
        public string Obter()
        {
            if (_loginRespostaModel.Value.Autenticado == false)
            {
                ObterToken();
            }
            else
            {
                if(DateTime.Now >= _loginRespostaModel.Value.DataExpiracao)
                {
                    ObterToken();
                }
            }
            return _loginRespostaModel.Value.Token;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebPrimeiraAlula.Models;
using WebPrimeiraAlula.Servico;
using WebPrimeiraAula.Models;

namespace WebPrimeiraAlula.Controllers
{
    public class FuncionarioController : Controller
    {

        //readonly string url = "https://localhost:7023/api/Funcionario";

        private string mensagem = "";


        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<loginRespostaModel> _loginRespostaModel;
        public FuncionarioController(IOptions<DadosBase> dadosBase, IOptions<loginRespostaModel> loginRespostaModel)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
        }


        // GET: FuncionarioController
        public ActionResult Index(string mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;


            HttpClient funcionario = new HttpClient();
            funcionario.DefaultRequestHeaders.Accept.Clear();
            funcionario.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            funcionario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = funcionario.GetAsync($"{_dadosBase.Value.API_URL_BASE}Funcionario/ObterTodosFuncionarios").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<List<FuncionarioModel>>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }




            //List<FuncionarioModel> funcionarios = new FuncionarioDB().ObterTodosFuncionarios();
            //return View(funcionarios);

        }

        // GET: FuncionarioController/Details/5
        public ActionResult Details(string valor)
        {

            HttpClient funcionario = new HttpClient();
            funcionario.DefaultRequestHeaders.Accept.Clear();
            funcionario.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            funcionario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = funcionario.GetAsync($"{_dadosBase.Value.API_URL_BASE}Funcionario/ObterDadosFuncionarios?cpf={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<FuncionarioModel>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }


            //FuncionarioModel funcionario = new FuncionarioDB().ObterDadosFuncionarios(valor);
            //return View(funcionario);
        }

        //// GET: FuncionarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: FuncionarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] FuncionarioModel funcionarioModel)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    HttpClient funcionario = new HttpClient();
                    funcionario.DefaultRequestHeaders.Accept.Clear();
                    funcionario.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    funcionario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

                    HttpResponseMessage response = funcionario.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Funcionario", funcionarioModel).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro cadastraado!", sucesso = true });

                    }
                    else
                    {
                        throw new Exception("DEU ZICA!!!!");
                    }

                    //new FuncionarioDB().Inserir(funcionarioModel);
                    //return RedirectToAction(nameof(Index), new { mensagem = "Registro cadastrado!", sucesso = true });
                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando preenchimento";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["erro"] = "Algum erro aconteceu - " + ex.Message;
                return View();
            }
        }

        //// GET: FuncionarioController/Edit/5
        public ActionResult Edit(string valor)
        {

            HttpClient funcionario = new HttpClient();
            funcionario.DefaultRequestHeaders.Accept.Clear();
            funcionario.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            funcionario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = funcionario.GetAsync($"{_dadosBase.Value.API_URL_BASE}Funcionario/ObterDadosFuncionarios?cpf={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<FuncionarioModel>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }
            //FuncionarioModel funcionario = new FuncionarioDB().ObterDadosFuncionarios(valor);
            //return View(funcionario);
        }

        // POST: FuncionarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] FuncionarioModel funcionarioModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //new FuncionarioDB().Alterar(funcionarioModel);

                    HttpClient funcionario = new HttpClient();
                    funcionario.DefaultRequestHeaders.Accept.Clear();
                    funcionario.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    funcionario.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

                    HttpResponseMessage response = funcionario.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Funcionario", funcionarioModel).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });

                    }
                    else
                    {
                        throw new Exception("DEU ZICA!!!!");
                    }
                    //return RedirectToAction(nameof(Index), new { mensagem = "Registro editado!", sucesso = true });

                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando preenchimento";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["erro"] = "Algum erro aconteceu - " + ex.Message;
                return View();
            }
        }

        // get: funcionariocontroller/delete/5
        //public ActionResult Delete(string valor)
        //{

        //    HttpClient funcionario = new HttpClient();
        //    funcionario.DefaultRequestHeaders.Accept.Clear();
        //    funcionario.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage response = funcionario.DeleteAsync($"{_dadosBase.Value.API_URL_BASE}Cliente?cpf={valor}").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction(nameof(Index), new { mensagem = "Registro excluído!", sucesso = true });
        //    }
        //    else
        //    {
        //        throw new Exception("DEU ZICA!");
        //    }
        //}
    }
}

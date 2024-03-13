using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebPrimeiraAlula.Models;
using WebPrimeiraAlula.Servico;
using WebPrimeiraAula.Models;

namespace WebPrimeiraAlula.Controllers
{
    public class ClienteController : Controller
    {

        //private readonly string url = "https://localhost:7023/api/Cliente";
        private string mensagem = "";


        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<loginRespostaModel> _loginRespostaModel;
        public ClienteController(IOptions<DadosBase> dadosBase, IOptions<loginRespostaModel> loginRespostaModel)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
        }


        // GET: ClienteController
        public ActionResult Index(string mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["Sucesso"] = mensagem;
            else
                TempData["Erro"] = mensagem;

            /*            List<ClienteModel> clientes = new ClienteDB().ObterTodosClientes();
            */

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());
            HttpResponseMessage response = client.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterTodosClientes").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<List<ClienteModel>>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }



            /*return View(clientes);*/
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(string valor)
        {
            /*ClienteModel cliente = new ClienteDB().ObterDadosClientes(valor);
            return View(cliente);*/

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
               new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = client.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterDadosClientes?cpf={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<ClienteModel>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }


        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ClienteModel clienteModel)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    /*new ClienteDB().Inserir(clienteModel);*/

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
               new ApiToken(_dadosBase, _loginRespostaModel).Obter());

                    HttpResponseMessage response = client.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente", clienteModel).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro cadastraado!", sucesso = true });

                    }
                    else
                    {
                        throw new Exception("DEU ZICA!!!!");
                    }


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

        // GET: ClienteController/Edit/5
        public ActionResult Edit(string valor)
        {
            /* ClienteModel cliente = new ClienteDB().ObterDadosClientes(valor);
             return View(cliente);*/

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
               new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = client.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterDadosClientes?cpf={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<ClienteModel>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] ClienteModel clienteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*                    new ClienteDB().Alterar(clienteModel);
                    */

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente", clienteModel).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });

                    }
                    else
                    {
                        throw new Exception("DEU ZICA!!!!");
                    }


/*                    return RedirectToAction(nameof(Index), new { mensagem = "Registro editado!", sucesso = true });
*/                }
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

        public ActionResult Delete(string valor)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.DeleteAsync($"{_dadosBase.Value.API_URL_BASE}Clientes?cpf={valor}").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), new { mensagem = "Registro excluído!", sucesso = true });
            }
            else
            {
                throw new Exception("DEU ZICA!");
            }
        }



        // GET: ClienteController/Delete/5
        /* public ActionResult Delete(int id)
         {
             return View();
         }

         // POST: ClienteController/Delete/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Delete(int id, IFormCollection collection)
         {
             try
             {
                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }*/
    }
}



using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebPrimeiraAlula.Models;
using WebPrimeiraAlula.Servico;
using WebPrimeiraAula.Models;

namespace WebPrimeiraAlula.Controllers
{

    public class FornecedorController : Controller
    {

        //readonly string url = "https://localhost:7023/api/Fornecedor";

        // GET: FornecedorController
        private string mensagem = "";

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<loginRespostaModel> _loginRespostaModel;
        public FornecedorController(IOptions<DadosBase> dadosBase, IOptions<loginRespostaModel> loginRespostaModel)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;  
        }


        public ActionResult Index(string mensagem = null, bool sucesso = true)
        {

            if (sucesso)
                TempData["Sucesso"] = mensagem;
            else
                TempData["Erro"] = mensagem;



            HttpClient fornecedor = new HttpClient();
            fornecedor.DefaultRequestHeaders.Accept.Clear();
            fornecedor.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            fornecedor.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = fornecedor.GetAsync($"{_dadosBase.Value.API_URL_BASE}Fornecedor/ObterTodosFornecedores").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<List<FornecedorModel>>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }


            //List<FornecedorModel> fornecedores = new FornecedorDB().ObterTodosFornecedores();
            //return View(fornecedores);

        }

        //GET: FornecedorController/Details/5
        public ActionResult Details(string valor)
        {

            HttpClient fornecedor = new HttpClient();
            fornecedor.DefaultRequestHeaders.Accept.Clear();
            fornecedor.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            fornecedor.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = fornecedor.GetAsync($"{_dadosBase.Value.API_URL_BASE}Fornecedor/ObterDadosFornecedores?cnpj={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<FornecedorModel>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }


            //FornecedorModel fornecedor = new FornecedorDB().ObterDadosFornecedores(valor);
            //return View(fornecedor);

        }

        //// GET: FornecedorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FornecedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] FornecedorModel fornecedorModel)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    //new FornecedorDB().Inserir(fornecedorModel);
                    //return RedirectToAction(nameof(Index), new { mensagem = "Registro Cadastrado!", sucesso = true });

                    HttpClient fornecedor = new HttpClient();
                    fornecedor.DefaultRequestHeaders.Accept.Clear();
                    fornecedor.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    fornecedor.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

                    HttpResponseMessage response = fornecedor.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Fornecedor", fornecedorModel).Result;

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

        // GET: FornecedorController/Edit/5
        public ActionResult Edit(string valor)
        {
            //FornecedorModel fornecedores = new FornecedorDB().ObterDadosFornecedores(valor);
            //return View(fornecedores);

            HttpClient fornecedor = new HttpClient();
            fornecedor.DefaultRequestHeaders.Accept.Clear();
            fornecedor.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            fornecedor.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = fornecedor.GetAsync($"{_dadosBase.Value.API_URL_BASE}Fornecedor/ObterDadosFornecedores?cnpj={valor}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<FornecedorModel>(conteudo));
            }
            else
            {
                throw new Exception("DEU ZICA!!!!");
            }

        }

        // POST: FornecedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] FornecedorModel fornecedorModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //new FornecedorDB().Alterar(fornecedorModel);
                    //return RedirectToAction(nameof(Index), new { mensagem = "Registro editado!", sucesso = true });

                    HttpClient fornecedor = new HttpClient();
                    fornecedor.DefaultRequestHeaders.Accept.Clear();
                    fornecedor.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    fornecedor.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                new ApiToken(_dadosBase, _loginRespostaModel).Obter());

                    HttpResponseMessage response = fornecedor.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Fornecedor", fornecedorModel).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });

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

        //// GET: FornecedorController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: FornecedorController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

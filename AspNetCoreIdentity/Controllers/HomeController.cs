﻿using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentity.Extensions;
using KissLog;
using System;

namespace AspNetCoreIdentity.Controllers
{
    /*
    * Working with Kisslogs
    * 
    * WARNING!!
    * Microsoft and Kisslog hava a same name to ILogger
    */

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Trace("Usuário acessou a Home");
            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Gestor")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimEscrever()
        {
            return View("Secret");
        }

        [ClaimsAuthorize("Produtos", "Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    modelError.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                    modelError.Titulo = "Ocorreu um erro";
                    modelError.ErrorCode = id;
                    _logger.Error(modelError);
                    break;

                case 404:
                    modelError.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato" +
                                          "com o nosso suporte.";
                    modelError.Titulo = "Ops! Página não encontrada.";
                    modelError.ErrorCode = id;
                    _logger.Error(modelError);
                    break;

                case 403:
                    modelError.Mensagem = "Você não tem permissão para fazer isso.";
                    modelError.Titulo = "Acesso Negado";
                    modelError.ErrorCode = id;
                    _logger.Error(modelError);
                    break;

                default:
                    return StatusCode(404);
            }
            return View("Error", modelError);
        }
    }
}

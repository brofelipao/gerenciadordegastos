using ESenarPlataformaEvento.Dominio.Logica.Interface.MVC;
using ESenarPlataformaEvento.Dominio.Logica.Interface.Repositorio.PlataformaEvento;
using ESenarPlataformaEvento.Dominio.Logica.ViewModel.Promocao;
using ESenarPlataformaEvento.Dominio.Logica.ViewModel.Reports;
using ESenarPlataformaEvento.MVC.Arquitetura;
using ESenarPlataformaEvento.Utils;
using ESenarPlataformaEvento.Utils.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Reporting.NETCore;
using System.Drawing;
using System.Drawing.Printing;

namespace ESenarPlataformaEvento.MVC.Controllers
{
    public class PromocaoController : BaseController
    {
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IInscricaoPublicaService _inscricaoPublicaService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PromocaoController> _logger;

        public PromocaoController(IEventoRepositorio eventoRepositorio, IInscricaoPublicaService inscricaoPublicaService, IWebHostEnvironment webHostEnvironment, ILogger<PromocaoController> logger) =>
            (_eventoRepositorio, _inscricaoPublicaService, _webHostEnvironment, _logger) = (eventoRepositorio, inscricaoPublicaService, webHostEnvironment, logger);

        [HttpGet]
        [Route("Promocao/{url}")]
        public async Task<ActionResult> Index(string url)
        {
            //var evento = _eventoRepositorio.ObterSomente(x => !x.isfinalizado).FirstOrDefault();

            try
            {
                var evento = await _eventoRepositorio.ObterUnicoPorUrl(url);
                if (evento is null) return NotFound();

                return View(new PromocaoViewModel(evento.id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(PromocaoViewModel model)
        {
            try
            {
                var evento_Participante = await _inscricaoPublicaService.CadastrarEGerarCodigoPromocao(model);

                //using var localReport = new WebReportRenderer(Path.Combine(_webHostEnvironment.ContentRootPath, "Reports/Promocao.rdlc"), WebReportRenderer.EFormatoRelatorio.PDF);

                //localReport.ReportInstance.DataSources.Add(new ReportDataSource("CodigoPromocao", new List<CodigoPromocaoViewModel>() { new() { Codigo = codigoPromocao.ToString() } }));

                TempData.Put("codigoPromocao", evento_Participante.codigopromocao.ToString());
                TempData.Put("hash", evento_Participante.hash.ToString());
                return Ok();

                //Response.Headers.Add("Content-Disposition", $"filename=CodigoPromocao.pdf");

                //return File(localReport.RenderToBytes(), "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Promocao/Download")]
        public async Task<IActionResult> CodigoPromocao()
        {
            var data = TempData.Get<string>("codigoPromocao");
            return View(new PromocaoViewModel() { Codigo = data });
        }

        [HttpGet]
        [Route("Promocao/Teste")]
        public async Task<IActionResult> Teste(string id)
        {

            return View("CodigoPromocao", new PromocaoViewModel() { Codigo = id });
        }
    }
}

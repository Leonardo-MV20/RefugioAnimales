using AutoMapper;
using RefugioAnimal.Application.Features.HistorialCuidado;
using RefugioAnimal.Application.Features.HistorialCuidado.Contratos;
using RefugioAnimal.Web.ViewModels.HistorialCuidado;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RefugioAnimal.Web.Controllers
{
    public class HistorialCuidadoController : Controller
    {
        private readonly IHistorialCuidadoService _historialCuidadoService;
        private readonly IMapper _mapper;

        public HistorialCuidadoController(IHistorialCuidadoService historialCuidadoService, IMapper mapper)
        {
            _historialCuidadoService = historialCuidadoService;
            _mapper = mapper;
        }

        // GET: HistorialCuidado/Index?mascotaId=...
        public async Task<ActionResult> Index(Guid mascotaId)
        {
            var dtos = await _historialCuidadoService.GetByMascota(mascotaId);
            var viewModel = _mapper.Map<IEnumerable<HistorialCuidadoViewModel>>(dtos);

            // Pasamos también el MascotaId a la vista por si se necesita para el botón de "Crear nuevo"
            ViewBag.MascotaId = mascotaId;
            return View(viewModel);
        }

        // GET: HistorialCuidado/Crear?mascotaId=...
        public ActionResult Crear(Guid mascotaId)
        {
            var model = new CrearHistorialCuidadoFormModel { MascotaId = mascotaId };
            return View(model);
        }

        // POST: HistorialCuidado/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearHistorialCuidadoFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Mapeamos el ViewModel al Request que espera el servicio
            var request = _mapper.Map<HistorialCuidadoRequest>(model);

            // Ejecutamos la inserción que ya incluye validaciones de negocio y de FluentValidation
            var validationResult = await _historialCuidadoService.Insert(request);

            if (!validationResult.IsValid)
            {
                // Si el servicio o el validador devuelven errores, los inyectamos en el ModelState de MVC
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }

            return RedirectToAction("Index", new { mascotaId = model.MascotaId });
        }
    }
}
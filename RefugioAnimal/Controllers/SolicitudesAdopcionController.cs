using AutoMapper;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.Adoptante.Contratos;
using RefugioAnimal.Application.Features.Mascota.Contratos;
using RefugioAnimal.Application.Features.SolicitudAdopcion;
using RefugioAnimal.Application.Features.SolicitudAdopcion.Contratos;
using RefugioAnimal.Web.ViewModels.SolicitudAdopcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RefugioAnimal.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class SolicitudesAdopcionController : Controller
    {
        private readonly ISolicitudAdopcionService _service;
        private readonly IAdoptanteService _adoptanteService;
        private readonly IMascotaService _mascotaService;
        private readonly IMapper _mapper;

        public SolicitudesAdopcionController(
            ISolicitudAdopcionService service,
            IAdoptanteService adoptanteService,
            IMascotaService mascotaService,
            IMapper mapper)
        {
            this._service = service;
            this._adoptanteService = adoptanteService;
            this._mascotaService = mascotaService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var solicitudes = await _service.GetAll();

            var model =
                _mapper.Map<IEnumerable<SolicitudAdopcionViewModel>>(solicitudes);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detalle(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(
                    400,
                    "Debe indicar la solicitud.");
            }

            var solicitud = await _service.GetById(id.Value);

            if (solicitud == null)
            {
                return HttpNotFound("La solicitud no existe.");
            }

            var model =
                _mapper.Map<SolicitudAdopcionViewModel>(solicitud);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Crear()
        {
            var model = new CrearSolicitudAdopcionFormModel
            {
                FechaSolicitud = DateTime.Today,
                Estado = "Pendiente"
            };

            await CargarOpciones(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(
            CrearSolicitudAdopcionFormModel model)
        {
            if (!ModelState.IsValid)
            {
                await CargarOpciones(model);

                return View(model);
            }

            try
            {
                var request =
                    _mapper.Map<SolicitudAdopcionRequest>(model);

                var resultado = await _service.Insert(request);

                if (!resultado.IsValid)
                {
                    AgregarErrores(resultado);

                    await CargarOpciones(model);

                    return View(model);
                }

                TempData["Exito"] =
                    "La solicitud de adopción fue registrada correctamente.";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Ocurrió un error al registrar la solicitud.";

                await CargarOpciones(model);

                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Editar(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(
                    400,
                    "Debe indicar la solicitud.");
            }

            var solicitud = await _service.GetById(id.Value);

            if (solicitud == null)
            {
                return HttpNotFound("La solicitud no existe.");
            }

            var model =
                _mapper.Map<EditarSolicitudAdopcionFormModel>(solicitud);

            await CargarOpciones(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(
            Guid id,
            EditarSolicitudAdopcionFormModel model)
        {
            if (id != model.Id)
            {
                return new HttpStatusCodeResult(
                    400,
                    "El identificador de la solicitud no coincide.");
            }

            if (!ModelState.IsValid)
            {
                await CargarOpciones(model);

                return View(model);
            }

            try
            {
                var solicitud = await _service.GetById(id);

                if (solicitud == null)
                {
                    return HttpNotFound("La solicitud no existe.");
                }

                var request =
                    _mapper.Map<SolicitudAdopcionRequest>(model);

                var resultado = await _service.Update(request);

                if (!resultado.IsValid)
                {
                    AgregarErrores(resultado);

                    await CargarOpciones(model);

                    return View(model);
                }

                TempData["Exito"] =
                    "La solicitud de adopción fue actualizada correctamente.";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Ocurrió un error al actualizar la solicitud.";

                await CargarOpciones(model);

                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(
                    400,
                    "Debe indicar la solicitud.");
            }

            var solicitud = await _service.GetById(id.Value);

            if (solicitud == null)
            {
                return HttpNotFound("La solicitud no existe.");
            }

            var model =
                _mapper.Map<SolicitudAdopcionViewModel>(solicitud);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarEliminar(Guid id)
        {
            try
            {
                var resultado = await _service.Delete(id);

                if (!resultado.IsValid)
                {
                    TempData["Error"] =
                        resultado.Errors[0].ErrorMessage;

                    return RedirectToAction("Index");
                }

                TempData["Exito"] =
                    "La solicitud fue eliminada correctamente.";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Ocurrió un error al eliminar la solicitud.";

                return RedirectToAction("Index");
            }
        }

        private async Task CargarOpciones(
            SolicitudAdopcionFormModelBase model)
        {
            var adoptantes = await _adoptanteService.GetAll();

            model.AdoptanteOptions = adoptantes.Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.NombreCompleto
                });

            var mascotas = await _mascotaService.GetAll();

            model.MascotaOptions = mascotas.Select(
                m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                });
        }

        private void AgregarErrores(ValidationResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }
        }
    }
}
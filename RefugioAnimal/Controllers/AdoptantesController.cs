using AutoMapper;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.Adoptante;
using RefugioAnimal.Application.Features.Adoptante.Contratos;
using RefugioAnimal.Web.ViewModels.Adoptante;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RefugioAnimal.Web.Controllers
{
    public class AdoptantesController : Controller
    {
        private readonly IAdoptanteService _service;
        private readonly IMapper _mapper;

        public AdoptantesController(
            IAdoptanteService service,
            IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var adoptantes = await _service.GetAll();

            var model = _mapper.Map<IEnumerable<AdoptanteViewModel>>(adoptantes);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detalle(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(
                    400,
                    "Debe indicar el adoptante.");
            }

            var adoptante = await _service.GetById(id.Value);

            if (adoptante == null)
            {
                return HttpNotFound("El adoptante no existe.");
            }

            var model = _mapper.Map<AdoptanteViewModel>(adoptante);

            return View(model);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View(new CrearAdoptanteFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearAdoptanteFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request = _mapper.Map<AdoptanteRequest>(model);

                var resultado = await _service.Insert(request);

                if (!resultado.IsValid)
                {
                    AgregarErrores(resultado);

                    return View(model);
                }

                TempData["Exito"] = "El adoptante fue registrado correctamente.";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al registrar el adoptante.";

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
                    "Debe indicar el adoptante.");
            }

            var adoptante = await _service.GetById(id.Value);

            if (adoptante == null)
            {
                return HttpNotFound("El adoptante no existe.");
            }

            var model = _mapper.Map<EditarAdoptanteFormModel>(adoptante);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(
            Guid id,
            EditarAdoptanteFormModel model)
        {
            if (id != model.Id)
            {
                return new HttpStatusCodeResult(
                    400,
                    "El identificador del adoptante no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var adoptante = await _service.GetById(id);

                if (adoptante == null)
                {
                    return HttpNotFound("El adoptante no existe.");
                }

                var request = _mapper.Map<AdoptanteRequest>(model);

                var resultado = await _service.Update(request);

                if (!resultado.IsValid)
                {
                    AgregarErrores(resultado);

                    return View(model);
                }

                TempData["Exito"] = "El adoptante fue actualizado correctamente.";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al actualizar el adoptante.";

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
                    "Debe indicar el adoptante.");
            }

            var adoptante = await _service.GetById(id.Value);

            if (adoptante == null)
            {
                return HttpNotFound("El adoptante no existe.");
            }

            var model = _mapper.Map<AdoptanteViewModel>(adoptante);

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
                    TempData["Error"] = resultado.Errors[0].ErrorMessage;

                    return RedirectToAction("Index");
                }

                TempData["Exito"] = "El adoptante fue eliminado correctamente.";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al eliminar el adoptante.";

                return RedirectToAction("Index");
            }
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
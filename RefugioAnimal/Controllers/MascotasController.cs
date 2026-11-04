using AutoMapper;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.Mascota;
using RefugioAnimal.Application.Features.Mascota.Contratos;
using RefugioAnimal.Web.ViewModels.Mascota;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RefugioAnimal.Web.Controllers
{
    public class MascotasController : Controller
    {
        private static readonly HashSet<string> CamposFormulario = new HashSet<string>
        {
            "Nombre",
            "Especie",
            "Raza",
            "EdadAproximada",
            "Sexo",
            "Estado",
            "FechaIngreso",
            "Descripcion",
            "EstaActivo"
        };

        private readonly IMascotaService _service;
        private readonly IMapper _mapper;

        public MascotasController(
            IMascotaService service,
            IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var mascotas = await _service.GetAll();
            var model = _mapper.Map<IEnumerable<MascotaViewModel>>(mascotas);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Detalle(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var mascota = await _service.GetById(id.Value);
            if (mascota == null)
            {
                return HttpNotFound("Mascota not found.");
            }

            var model = _mapper.Map<MascotaViewModel>(mascota);
            return View(model);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            var model = new CrearMascotaFormModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearMascotaFormModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(400, "Bad Request: model is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request = _mapper.Map<MascotaRequest>(model);
                var resultado = await _service.Insert(request);

                if (!resultado.IsValid)
                {
                    AgregarErrores(resultado);
                    return View(model);
                }

                TempData["Exito"] = "La mascota se creó correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la mascota.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Editar(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var mascota = await _service.GetById(id.Value);
            if (mascota == null)
            {
                return HttpNotFound("Mascota not found.");
            }

            var model = _mapper.Map<EditarMascotaFormModel>(mascota);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Guid id, EditarMascotaFormModel model)
        {
            if (id == Guid.Empty || model == null || id != model.Id)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID mismatch.");
            }

            if (await _service.GetById(id) == null)
            {
                return HttpNotFound("Mascota not found.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var request = _mapper.Map<MascotaRequest>(model);
                var resultado = await _service.Update(request);

                if (!resultado.IsValid)
                {
                    AgregarErrores(resultado);
                    return View(model);
                }

                TempData["Exito"] = "La mascota se actualizó correctamente.";
                return RedirectToAction("Detalle", new { id = model.Id });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar la mascota.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            var mascota = await _service.GetById(id.Value);
            if (mascota == null)
            {
                return HttpNotFound("Mascota not found.");
            }

            var model = _mapper.Map<MascotaViewModel>(mascota);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarEliminar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(400, "Bad Request: ID is required.");
            }

            try
            {
                var resultado = await _service.Delete(id);

                if (!resultado.IsValid)
                {
                    TempData["Error"] = string.Join(" ", resultado.Errors.Select(e => e.ErrorMessage));
                    return RedirectToAction("Index");
                }

                TempData["Exito"] = "La mascota se eliminó correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al eliminar la mascota.";
                return RedirectToAction("Index");
            }
        }
        private void AgregarErrores(ValidationResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                string campo = error.PropertyName;

                if (!CamposFormulario.Contains(campo))
                {
                    campo = string.Empty;
                }

                ModelState.AddModelError(campo, error.ErrorMessage);
            }
        }
    }
}
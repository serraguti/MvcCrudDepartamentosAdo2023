using Microsoft.AspNetCore.Mvc;
using MvcCrudDepartamentosAdo.Models;
using MvcCrudDepartamentosAdo.Repositories;

namespace MvcCrudDepartamentosAdo.Controllers
{
    public class DepartamentosController : Controller
    {
        private RepositoryDepartamentos repo;

        public DepartamentosController()
        {
            this.repo = new RepositoryDepartamentos();
        }

        public IActionResult Index()
        {
            List<Departamento> departamentos =
                this.repo.GetDepartamentos();
            return View(departamentos);
        }

        public IActionResult Details(int iddepartamento)
        {
            Departamento departamento =
                this.repo.FindDepartamento(iddepartamento);
            return View(departamento);
        }
    }
}

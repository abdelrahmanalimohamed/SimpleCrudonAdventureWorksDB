using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simpleCrudonAdventureWorksDB.DB;
using simpleCrudonAdventureWorksDB.Interfaces;
using simpleCrudonAdventureWorksDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleCrudonAdventureWorksDB.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository _repository;

        public DepartmentController(AppDBContext dBContext, IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Departments>>> GetAll()
        {
            var data = await _repository.SelectAll<Departments>();

            return data;
        }

        [HttpGet]
        [Route("getsingledepart")]

        public async Task<ActionResult<Departments>> GetDepartment(long id)
        {
            var data = await _repository.SelectById<Departments>(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }


        [HttpPut]
        [Route("updateDept")]
        public async Task<ActionResult<Departments>> UpdateDepartment(long id , Departments department)
        {
            if (id != department.DepartmentID)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync<Departments>(department);

          

            return NoContent();
        }


        [HttpPost]
        [Route("InsertDept")]
        public async Task<ActionResult<Departments>> InsertDepartment(Departments department)
        {
            await _repository.CreateAsync<Departments>(department);

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentID }, department);
        }

        [HttpDelete]

        public async Task<ActionResult<Departments>> DeleteDepartment(long id)
        {
            var selected_dept = await _repository.SelectById<Departments>(id);


            if (selected_dept == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync<Departments>(selected_dept);
            return selected_dept;
        }
        


        }
}

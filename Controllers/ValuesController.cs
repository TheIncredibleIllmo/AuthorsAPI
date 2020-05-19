using AuthorsAPI.Data;
using AuthorsAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AuthorsAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ValuesController
    {
        private readonly IValuesRepository _repo;

        public ValuesController(IValuesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<List<Value>> Get()
        {
            return await _repo.GetAll();
        }

    }
}

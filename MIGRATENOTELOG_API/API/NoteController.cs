using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIGRATENOTELOG_API.Models;
using MIGRATENOTELOG_API.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MIGRATENOTELOG_API.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IServiceProvider _service;
        public NoteController(IServiceProvider service)
        {
            _service = service;
        }

        [HttpGet("All", Name = "GetAllNote")]
        public async Task<List<NoteModel>> GetAllNote()
        {
            var Service = _service.GetService<INoteService>();
            return await Service.GetAllNote();
        }

        [HttpPost("GetFilterNote", Name = "CareandTel")]
        public async Task<List<NoteModel>> GetFilterNote([FromBody] Resource resource)
        {
            var Service = _service.GetService<INoteService>();
            return await Service.GetFilterNote(resource);
        }
    }
}
using MIGRATENOTELOG_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIGRATENOTELOG_API.Services.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteModel>> GetAllNote();
        Task<List<NoteModel>> GetFilterNote(Resource resource);
        Task AddNote(List<NoteModel> models);
    }

}

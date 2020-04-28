using MIGRATENOTELOG_API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIGRATENOTELOG_API.DataAccess.Interfaces
{
    public interface INoteDbContext
    {
        IMongoCollection<NoteModel> LogNote { get; }
    }
}

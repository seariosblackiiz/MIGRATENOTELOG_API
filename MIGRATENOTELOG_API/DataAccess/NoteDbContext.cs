using MIGRATENOTELOG_API.DataAccess.Interfaces;
using MIGRATENOTELOG_API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIGRATENOTELOG_API.DataAccess
{
    public class NoteDbContext : INoteDbContext
    {
        private readonly IMongoDatabase _db;
        public NoteDbContext(IMongoClient client, string dbName)
        {
            _db = client.GetDatabase(dbName);
        }
        public IMongoCollection<NoteModel> LogNote => _db.GetCollection<NoteModel>("TS_TimeLine");
    }
}

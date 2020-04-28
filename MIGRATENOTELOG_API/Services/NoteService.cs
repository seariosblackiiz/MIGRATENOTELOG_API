using MIGRATENOTELOG_API.DataAccess.Interfaces;
using MIGRATENOTELOG_API.Models;
using MIGRATENOTELOG_API.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MIGRATENOTELOG_API.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteDbContext _context;

        public NoteService(INoteDbContext context)
        {
            _context = context;
        }

        public async Task<List<NoteModel>> GetAllNote()
        {
            try
            {
                return await _context.LogNote.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<NoteModel>> GetFilterNote(Resource resource)
        {
            try
            {
                var model = new NoteModel();
                Type type = model.GetType();
                PropertyInfo[] props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var _filter = Builders<NoteModel>.Filter;
                var _filters = new List<FilterDefinition<NoteModel>>();
                BsonDocument bson = new BsonDocument();
                FilterDefinition<NoteModel> cmdSearch = null;

                if (resource.actionCode != null && resource.resultCode != null) // btn Caseful and Telesale
                {
                    if (!string.IsNullOrEmpty(resource.columnName) && !string.IsNullOrEmpty(resource.txtValue))
                    {
                        cmdSearch = _filter.And(
                        _filter.In(f => f.actionCode, resource.actionCode),
                        _filter.In(f => f.resultCode, resource.resultCode),
                        _filter.Regex(resource.columnName, resource.txtValue)
                        );
                    }
                    else if (string.IsNullOrEmpty(resource.columnName) && !string.IsNullOrEmpty(resource.txtValue))
                    {
                        for (int i = 0; i < props.Length; i++)
                        {
                            _filters.Add(_filter.Regex(props[i].Name, resource.txtValue));
                        }

                        cmdSearch = _filter.And(
                           _filter.In(f => f.actionCode, resource.actionCode),
                           _filter.In(f => f.resultCode, resource.resultCode),
                           _filter.Or(_filters)
                           );
                    }
                    else
                    {
                        cmdSearch = _filter.And(
                            _filter.In(f => f.actionCode, resource.actionCode),
                            _filter.In(f => f.resultCode, resource.resultCode)
                            );
                    }
                }
                else if (resource.actionCode != null && resource.resultCode == null)   // btn Action
                {
                    cmdSearch = _filter.And(
                        _filter.In(f => f.actionCode, resource.actionCode)
                                        );
                }
                else if (resource.resultCode != null && resource.actionCode == null) // btn Result
                {
                    cmdSearch = _filter.And(
                                _filter.In(f => f.resultCode, resource.resultCode)
                                );
                }
                else
                {
                    if (!string.IsNullOrEmpty(resource.columnName) && resource.txtValue != null)
                    {
                        //var dateConvert = new DateTime();
                        if (DateTime.TryParseExact(resource.txtValue, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime dateConvert))
                        {
                            DateTime StartDateTime = DateTime.ParseExact(dateConvert.ToString("yyyy-MM-dd"), "yyyy-MM-dd",
                                CultureInfo.CreateSpecificCulture("en-US"));   // รับค่าจาก String txtValue >> format ปี/เดือน/วัน 
                            cmdSearch = bson.Add(resource.columnName, new BsonDocument()
                                .Add("$gte", new BsonDateTime(StartDateTime.AddHours(7)))
                                .Add("$lte", new BsonDateTime(StartDateTime.AddHours(7)))
                                );
                        }
                        else
                        {
                            cmdSearch = bson.Add("$where", $"/.*{resource.txtValue}.*/.test(this.{resource.columnName})");
                        }

                    }
                    else if (string.IsNullOrEmpty(resource.columnName) && !string.IsNullOrEmpty(resource.txtValue))
                    {
                        List<string> listRegex = new List<string>();

                        for (int i = 0; i < props.Length; i++)
                        {
                            listRegex.Add($"/.*{resource.txtValue}.*/.test(this.{props[i].Name})");
                        }
                        bson.Add("$where", string.Join(" || ", listRegex));

                        cmdSearch = _filter.Or(bson);
                    }
                    else
                    {
                        throw new Exception(); // ไม่มีเงื่อนไขที่ถูกต้อง ส่ง exception ออกไป .
                    }
                }


                return await _context.LogNote.Find(cmdSearch).Skip(0).Limit(30).ToListAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task AddNote(List<NoteModel> models)
        {
            try
            {
                await _context.LogNote.InsertManyAsync(models);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIGRATENOTELOG_API.Models
{
    public class NoteModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { set; get; }
        public int idNote { set; get; }
        public string csnNo { set; get; }
        public string contractNo { set; get; }
        public DateTime? noteDate { set; get; }
        public string actionCode { set; get; }
        public string personCode { set; get; }
        public string resultCode { set; get; }
        public string noteDescription { set; get; }
        public DateTime? ppDate { set; get; }
        public DateTime? pdDate { set; get; }
        public DateTime? recallDate { set; get; }
        public double ppAmt { set; get; }
        public double alreadyPaidAmt { set; get; }
        public string ppChannel { set; get; }
        public string callCenter { set; get; }
        public string telType { set; get; }
        public string telNo { set; get; }
        public string callType { set; get; }
        public string contactTo { set; get; }
        public string systemBy { set; get; }
        public string noteFlag { set; get; }
        public string recordStatus { set; get; }
        public DateTime? createDate { set; get; }
        public string createBy { set; get; }
        public DateTime? updateDate { set; get; }
        public string updateBy { set; get; }
    }

    public class Resource
    {
        public List<string> actionCode { get; set; }
        public List<string> resultCode { get; set; }
        public string columnName { get; set; }
        public string txtValue { get; set; }
        public int skipRows { get; set; }
    }
}

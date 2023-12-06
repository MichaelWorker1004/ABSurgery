using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Examinations
{
    public class AccommodationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccommodationID { get; set; }
        public int? DocumentId { get; set; }
        public int? ExamID { get; set; }
        public IFormFile File { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}

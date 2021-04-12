using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public bool IsExpired => DateTime.Now >= ExpireDateTime;
        public DateTime CreateDateTime { get; set; }
        public string CreatedByIp { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
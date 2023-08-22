using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDay1.Models {
    public class UserConnectionId {
         public int Id { get; set; }

        public string? ConnectionId { get;set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}

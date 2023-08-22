using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDay1.Models {
    public class UserGroup {
        public int Id { get; set; }
        [ForeignKey(nameof(group))]
        public int? GroupId { get; set; }
        public virtual Group group { get; set; }
        [ForeignKey(nameof(Sender))]
        public string? SenderId { get; set; }
        public virtual IdentityUser Sender { get; set; }
        [ForeignKey(nameof(Reciver))]
        public string? RecivedId { get; set; }
        public virtual IdentityUser Reciver { get; set; }
    }
}

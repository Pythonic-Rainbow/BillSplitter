using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Server.Models
{
    [Index(nameof(DiscordId), IsUnique = true)]
    public class Account
    {
        public int Id { get; set; }

        public ulong? DiscordId { get; set; }
    }
}

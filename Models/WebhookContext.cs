using Microsoft.EntityFrameworkCore;

namespace ACRPhone.Webhook.Models
{
    public class WebhookContext : DbContext
    {
        public WebhookContext(DbContextOptions<WebhookContext> options) : base(options)
        {
            
        }

        public DbSet<Recording> Recordings { get; set; }

    }
}

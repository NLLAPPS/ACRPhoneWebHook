using ACRPhone.Webhook.Models;

namespace ACRPhone.Webhook.Repositories
{
    public class RecordingRepository : RepositoryBase<Recording, long>, IRecordingRepository
    {
        public RecordingRepository(WebhookContext dbContext) : base(dbContext)
        {
        }
    }
}

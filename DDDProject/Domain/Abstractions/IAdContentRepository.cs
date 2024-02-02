using Microsoft.EntityFrameworkCore;

namespace DDDProject.Domain.Abstractions;

public interface IAdContentRepository
{
    Task<AdContent> GetAdContentById(Guid id, CancellationToken ct = default);
    Task<IEnumerable<AdContent>> GetAdContentsByAccountId(long id, CancellationToken ct = default);
    Task<List<AdContent>> GetPendingAds(DateTime from, CancellationToken ct = default);
    void Add(AdContent adContent);
    void Update(AdContent adContent);
}

public class AdContentRepository : IAdContentRepository
{
    private readonly ApplicationDbContext context;

    public AdContentRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Add(AdContent adContent)
    {
        throw new NotImplementedException();
    }

    public Task<AdContent> GetAdContentById(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AdContent>> GetAdContentsByAccountId(long id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AdContent>> GetPendingAds(DateTime from, CancellationToken ct = default)
    {
         return await context.AdContents.Where(
            ad => ad.ModifiedAt > from &&
                  ad.ReviewStatus == Enums.EReviewStatus.Pending)
            .ToListAsync(ct);
    }

    public void Update(AdContent adContent)
    {
        throw new NotImplementedException();
    }
}

public class ApplicationDbContext
{
    public DbSet<AdContent> AdContents {get;set;}
}
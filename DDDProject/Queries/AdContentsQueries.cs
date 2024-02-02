using DDDProject.Domain;
using DDDProject.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DDDProject.Queries;

public class AdContentsQueries
{
    private readonly DbContext _context;

    public AdContentsQueries(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AdContent>> GetAdContents(AdContentsFilter filter) =>
        await _context.AdContents
            .WhereDateTypeBetween(filter.From, filter.To, filter.FilterDateType)
            .Where(ad =>
                filter.Ids.IsAll() && filter.Ids.Contains(ad.Id) ||
                filter.AdStatuses.IsAll() && filter.AdStatuses.Contains(ad.Status) ||
                filter.ReviewStatuses.IsAll() && filter.ReviewStatuses.Contains(ad.ReviewStatus) ||
                filter.Creators.IsAll() && filter.Creators.Contains(ad.Creator.Id))
            .ToListAsync();
}

public static class QueryExtensions
{
    public static IQueryable<AdContent> WhereDateTypeBetween(this IQueryable<AdContent> query,
        DateTime from,
        DateTime to,
        EFilterDateType filterType) => filterType switch
    {
        EFilterDateType.CreatedAt =>
            query.Where(ad => ad.CreatedAt >= from && ad.CreatedAt < to),
        EFilterDateType.ModifiedAt =>
            query.Where(ad => ad.ModifiedAt >= from && ad.ModifiedAt < to),
        EFilterDateType.ReviewedAt =>
            query.Where(ad => ad.ReviewedAt >= from && ad.ReviewedAt < to),
        _ => query
    };
}

public static class FilterExtensions
{
    public static bool IsAll<T>(this IEnumerable<T>? enumerable) =>
        enumerable is null || !enumerable.Any();
}

public record AdContentsFilter(
    DateTime From,
    DateTime To,
    EFilterDateType FilterDateType,
    IEnumerable<Guid> Ids,
    IEnumerable<Guid> Creators,
    IEnumerable<ERegulation> Regulations,
    IEnumerable<EReviewStatus> ReviewStatuses,
    IEnumerable<EAdContentStatus> AdStatuses);

public enum EFilterDateType
{
    CreatedAt,
    ModifiedAt,
    ReviewedAt
}

public abstract class DbContext
{
    public DbSet<AdContent> AdContents;
}
    
namespace DDDProject.Domain;

public record Campaign(long Id, long ProviderId, string Name, IEnumerable<Country> Countries);
namespace DDDProject.Domain;

public record Comment(Guid CommenterId, DateTime CreatedAt, string Text)
{
    public Guid Id { get; } = Guid.NewGuid(); 
    
    public static Comment? Create(Guid commenterId, DateTime createdAt, string? text)
        => text is null ? null : new Comment(commenterId, createdAt, text);
}
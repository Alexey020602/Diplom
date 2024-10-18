namespace Model;

public record Paging<T>(int Count, int Skip, int Take, IReadOnlyList<T> Data);
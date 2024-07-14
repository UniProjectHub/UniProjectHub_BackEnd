public class Pagination<TEntity>
{
    public Pagination(List<TEntity> items, int totalItemCount, int pageIndex, int pageSize)
    {
        Items = items;
        TotalItemCount = totalItemCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public List<TEntity> Items { get; set; }
    public int TotalItemCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

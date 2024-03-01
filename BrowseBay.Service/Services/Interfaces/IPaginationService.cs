namespace BrowseBay.Service.Services.Interfaces;

public interface IPaginationService<T> where T : class
{
    /// <summary>
    /// Current page to display
    /// </summary>
    int ActivePage { get; set; }

    /// <summary>
    /// No of pages available based from the <see cref="Activepage"/> and <see cref="PageSize"/>.
    /// </summary>
    int NoOfPages { get; }

    /// <summary>
    /// No of items per page
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// Sets items of type <typeparamref name="T"/> to the whole book
    /// </summary>
    /// <param name="items"></param>
    void SetItems(IEnumerable<T> items);

    /// <summary>
    /// Get the items of the current page
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> Get();
}

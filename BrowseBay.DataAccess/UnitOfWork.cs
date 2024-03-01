using BrowseBay.DataAccess.Repositories;
using BrowseBay.Models;

namespace BrowseBay.DataAccess;

public class UnitOfWork : IDisposable
{
    private readonly AppDbContext _context;
    private bool _isDisposed = false;
    private RepoManager<Product> _productManager = default!;
    private RepoManager<Category> _categoryManager = default!;
    private RepoManager<ProductCategory> _productCategoryManager = default!;
    public RepoManager<Product> ProductManager => _productManager ??= new RepoManager<Product>(_context);
    public RepoManager<Category> CategoryManager => _categoryManager ??= new RepoManager<Category>(_context);
    public RepoManager<ProductCategory> ProductCategoryManager => _productCategoryManager ??= new RepoManager<ProductCategory>(_context);

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed && disposing)
        {
            _context.Dispose();
        }

        _isDisposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

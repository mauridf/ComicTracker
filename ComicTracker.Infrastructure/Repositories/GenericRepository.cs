using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Repositories;

public class GenericRepository<T> where T : class
{
    protected readonly ComicTrackerDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ComicTrackerDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // Implementar métodos CRUD básicos
}

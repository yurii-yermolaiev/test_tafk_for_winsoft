using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Infrastructure;
using Test.Repositories.Interfaces;

namespace Test.Repositories
{
    public class DocumentReposetory : IRepository<Document>
    {
        private readonly ApplicationDbContext _context;

        public DocumentReposetory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Document item)
        {
            await _context.Documents.AddAsync(item);
            await _context.DateFields.AddRangeAsync(item.DateFields);
            await _context.StringFields.AddRangeAsync(item.StringFields);
            await _context.LongFields.AddRangeAsync(item.LongFields);
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.Documents
                .Include(d => d.LongFields)
                .Include(d => d.DateFields)
                .Include(d => d.StringFields)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (item != null)
            {
                _context.Remove(item);
            }
        }

        public async Task<Document?> GetAsync(long id)
        {
            return await _context.Documents
                .Include(d => d.LongFields)
                .Include(d => d.DateFields)
                .Include(d => d.StringFields)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _context.Documents
                .Include(d => d.LongFields)
                .Include(d => d.DateFields)
                .Include(d => d.StringFields)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document item)
        {
            _context.Update(item);
        }
    }
}

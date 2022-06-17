using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Infrastructure;
using Test.Repositories.Interfaces;

namespace Test.Repositories
{
    public class DocumentTemplateReposetory : IRepository<DocumentTemplate>
    {
        private readonly ApplicationDbContext _context;

        public DocumentTemplateReposetory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(DocumentTemplate item)
        {
            await _context.DocumentTemplates.AddAsync(item);
            await _context.Fields.AddRangeAsync(item.DateFields);
            await _context.Fields.AddRangeAsync(item.StringFields);
            await _context.Fields.AddRangeAsync(item.LongFields);
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.DocumentTemplates
                .Include(d => d.LongFields)
                .Include(d => d.DateFields)
                .Include(d => d.StringFields)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (item != null)
            {
                _context.Remove(item);
            }
        }

        public async Task<DocumentTemplate?> GetAsync(long id)
        {
            return await _context.DocumentTemplates
                .Include(d => d.LongFields)
                .Include(d => d.DateFields)
                .Include(d => d.StringFields)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DocumentTemplate>> GetAllAsync()
        {
            return await _context.DocumentTemplates
                .Include(d => d.LongFields)
                .Include(d => d.DateFields)
                .Include(d => d.StringFields)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocumentTemplate item)
        {
            _context.Update(item);
        }
    }
}

using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly ApplicationDbContext _context;

        public BlockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Block>> GetAllAsync()
        {
            return await _context.Blocks.ToListAsync();
        }
    }
}
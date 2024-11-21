using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class PartRepository : IPartRepository
    {
        private readonly ApplicationDbContext _context;
        public PartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Part> AddPart(Part part)
        {
            await _context.Parts.AddAsync(part);
            await _context.SaveChangesAsync();
            return part;
        }

        public async Task<List<Part>> GetAllParts()
        {
            return await _context.Parts.ToListAsync();
        }

        public Task<List<Part>> GetPartsByExamId(string examId)
        {
            return _context.Parts.Where(x => x.ExamId == examId).ToListAsync();
        }

        public Task<Part> GetPartById(string id)
        {
            return _context.Parts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Part> UpdatePart(Part part)
        {
            _context.Parts.Update(part);
            await _context.SaveChangesAsync();
            return part;
        }
    }
}
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
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Exam> CreateExam(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task<List<Exam>> GetAllExams()
        {
            return await _context.Exams.ToListAsync();
        }

        public async Task<Exam> GetExamById(string id)
        {
            return await _context.Exams.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
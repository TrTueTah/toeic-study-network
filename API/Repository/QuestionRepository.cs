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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Question> AddQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionById(string id)
        {
            return await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Question>> GetQuestionsByPartId(string partId)
        {
            return _context.Questions.Where(x => x.PartId == partId).ToListAsync();
        }
    }
}
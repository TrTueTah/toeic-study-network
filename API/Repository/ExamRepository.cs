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
            return await _context.Exams.Include(e => e.QuestionGroups)
                .ThenInclude(qg => qg.Questions)
                .ToListAsync();
        }

        public async Task<Exam> GetExamById(string id)
        {
            var exam = await _context.Exams.Include(e => e.QuestionGroups)
                .ThenInclude(qg => qg.Questions)
                .FirstOrDefaultAsync(x => x.Id == id);

            return exam;
        }

        public List<Exam> GetExamsByExamSeries(string examSeriesId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateExam(Exam exam)
        {
            _context.Exams.Update(exam);
            return Save();
        }

        public bool DeleteExam(string id)
        {
            var exam = _context.Exams.FirstOrDefault(e => e.Id == id);
            if (exam == null)
            {
                return false;
            }

            _context.Exams.Remove(exam);
            return Save();
        }

        public bool IsExamExist(string id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
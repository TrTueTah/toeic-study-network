using API.Data;
using API.Dtos.ExamDto;
using API.Dtos.ExamSeriesDto;
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

        public async Task<List<GetAllExamDto>> GetAllExams()
        {
            var exams = _context.Exams
                .Include(e => e.QuestionGroups)
                .ThenInclude(qg => qg.Questions)
                .Include(es => es.ExamSeries)
                .Select(e => new GetAllExamDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    CreatedAt = e.CreatedAt,
                    AudioFilesUrl = e.AudioFilesUrl,
                    QuestionGroups = e.QuestionGroups,
                    ExamSeries = new GetExamSeriesDto
                    {
                        Id = e.ExamSeries.Id,
                        Name = e.ExamSeries.Name
                    },
                })
                .ToList();
            return exams;
        }

        public async Task<GetAllExamDto> GetExamById(string id)
        {
            var exam = await _context.Exams.Include(e => e.QuestionGroups)
                .ThenInclude(qg => qg.Questions)
                .Include(es => es.ExamSeries)
                .Select(e => new GetAllExamDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    CreatedAt = e.CreatedAt,
                    AudioFilesUrl = e.AudioFilesUrl,
                    QuestionGroups = e.QuestionGroups,
                    ExamSeries = new GetExamSeriesDto
                    {
                        Id = e.ExamSeries.Id,
                        Name = e.ExamSeries.Name
                    },
                })
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
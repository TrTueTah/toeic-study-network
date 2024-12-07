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

        public GetAllExamDto GetExamById(string id)
        {
            var exam = _context.Exams
                .Include(e => e.QuestionGroups)
                .ThenInclude(qg => qg.Questions)
                .Include(e => e.ExamSeries)
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
                .FirstOrDefault(x => x.Id == id);

            return exam;
        }

        public GetExamByPartDto GetExamByPart(string examId, List<int> partNumbers)
        {
            var exam = _context.Exams
                .Include(e => e.QuestionGroups)
                .ThenInclude(qg => qg.Questions)
                .Where(e => e.Id == examId)
                .Select(e => new
                {
                    e.Id,
                    e.Title,
                    e.CreatedAt,
                    e.ExamSeries,
                    FilteredQuestionGroups = e.QuestionGroups
                        .Where(qg => partNumbers.Contains(qg.PartNumber))
                        .OrderBy(qg => qg.Questions.FirstOrDefault().QuestionNumber)
                        .ToList()
                })
                .FirstOrDefault();
            

            if (exam == null)
            {
                throw new Exception("Exam not found");
            }
            
            var result = new GetExamByPartDto
            {
                ExamId = exam.Id,
                Title = exam.Title,
                CreatedAt = exam.CreatedAt,
                PartNumbers = partNumbers,
                QuestionGroups = exam.FilteredQuestionGroups,
                ExamSeries = new GetExamSeriesDto
                {
                    Id = exam.ExamSeries.Id,
                    Name = exam.ExamSeries.Name
                },
            };

            return result;
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
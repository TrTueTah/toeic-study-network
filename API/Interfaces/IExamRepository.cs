using API.Dtos.ExamDto;
using API.Models;

namespace API.Interfaces
{
    public interface IExamRepository
    {
        Task<List<GetAllExamDto>> GetAllExams();
        Task<Exam> CreateExam(Exam exam);
        Task<GetAllExamDto?> GetExamById(string id);
        List<Exam> GetExamsByExamSeries(string examSeriesId);
        bool UpdateExam(Exam exam);
        bool DeleteExam(string id);
        bool IsExamExist(string id);
    }
}
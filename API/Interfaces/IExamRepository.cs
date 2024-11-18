using API.Models;

namespace API.Interfaces
{
    public interface IExamRepository
    {
        Task<List<Exam>> GetAllExams();
        Task<Exam> CreateExam(Exam exam);
        Task<Exam?> GetExamById(string id);
    }
}
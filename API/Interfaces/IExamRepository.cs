using API.Dtos.ExamDto;
using API.Models;

namespace API.Interfaces
{
    public interface IExamRepository
    {
        Task<List<GetAllExamDto>> GetAllExams();
        Task<Exam> CreateExam(Exam exam);
        GetAllExamDto? GetExamById(string id);
        GetExamByPartDto GetExamByPart(string examId, List<int> partNumbers);
        Exam UpdateExam(Exam exam);
        bool DeleteExam(string id);
        bool IsExamExist(string id);
    }
}
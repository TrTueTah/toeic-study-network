using API.Models;

namespace API.Interfaces;

public interface IExamSeriesRepository
{
    List<ExamSeries> GetAllExamSeries();
    ExamSeries GetExamSeriesById(string Id);
    ExamSeries CreateExamSeries(ExamSeries examSeries);
    ExamSeries UpdateExamSeries(ExamSeries examSeries);
    bool DeleteExamSeries(string Id);
    bool IsExamSeriesExist(string Id);
}
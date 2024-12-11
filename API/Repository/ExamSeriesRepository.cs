using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class ExamSeriesRepository : IExamSeriesRepository
{
    private readonly ApplicationDbContext _context;
    public ExamSeriesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<ExamSeries> GetAllExamSeries()
    {
        return _context.ExamSeries
            .Include(es => es.Exams)
            .ToList();
    }

    public ExamSeries GetExamSeriesById(string Id)
    {
        return _context.ExamSeries.Include(es => es.Exams)
            .FirstOrDefault(es => es.Id == Id);
    }

    public ExamSeries CreateExamSeries(ExamSeries examSeries)
    {
        _context.ExamSeries.Add(examSeries);
        _context.SaveChanges();
        return examSeries;
    }

    public ExamSeries UpdateExamSeries(ExamSeries examSeries)
    {
        _context.ExamSeries.Update(examSeries);
        _context.SaveChanges();
        return examSeries;
    }

    public bool DeleteExamSeries(string id)
    {
        var examSeries = _context.ExamSeries.Find(id);

        if (examSeries == null)
        {
            return false;
        }

        _context.ExamSeries.Remove(examSeries);
        return Save();
    }

    public bool IsExamSeriesExist(string id)
    {
        return _context.ExamSeries.Any(c => c.Id == id);
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}
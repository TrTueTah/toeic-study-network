using API.Data;
using API.Interfaces;
using API.Models;

namespace API.Repository;

public class QuestionGroupRepository : IQuestionGroupRepository
{
    private readonly ApplicationDbContext _context;

    public QuestionGroupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public QuestionGroup CreateQuestionGroup(QuestionGroup questionGroup)
    {
        _context.QuestionGroups.Add(questionGroup);
        _context.SaveChanges();
        return questionGroup;
    }

    public QuestionGroup GetQuestionGroupById(string id)
    {
        return _context.QuestionGroups
            .Where(qg => qg.Id == id)
            .FirstOrDefault();
    }

    public List<QuestionGroup> GetAllQuestionGroupsForExam(string examId)
    {
        return _context.QuestionGroups
            .Where(qg => qg.ExamId == examId)
            .ToList();
    }

    public QuestionGroup UpdateQuestionGroup(QuestionGroup questionGroup)
    {
        var existingGroup = _context.QuestionGroups
            .FirstOrDefault(qg => qg.Id == questionGroup.Id);

        if (existingGroup == null)
        {
            return null;
        }

        existingGroup.PartNumber = questionGroup.PartNumber;
        existingGroup.ImageFilesUrl = questionGroup.ImageFilesUrl;
        existingGroup.AudioFilesUrl = questionGroup.AudioFilesUrl;
        existingGroup.Questions = questionGroup.Questions;

        _context.SaveChanges();
        return existingGroup;
    }

    public bool DeleteQuestionGroup(string id)
    {
        var group = _context.QuestionGroups
            .FirstOrDefault(qg => qg.Id == id);

        if (group == null)
        {
            return false;
        }

        _context.QuestionGroups.Remove(group);
        _context.SaveChanges();
        return true;
    }

    public bool QuestionGroupExists(string id)
    {
        return _context.QuestionGroups.Any(q => q.Id == id);
    }
}
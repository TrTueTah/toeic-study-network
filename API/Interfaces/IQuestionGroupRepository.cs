using API.Models;

namespace API.Interfaces;

public interface IQuestionGroupRepository
{
    QuestionGroup CreateQuestionGroup(QuestionGroup questionGroup);
    QuestionGroup GetQuestionGroupById(string id);
    List<QuestionGroup> GetAllQuestionGroupsForExam(string examId);
    bool UpdateQuestionGroup(QuestionGroup questionGroup);
    bool DeleteQuestionGroup(string id);
    bool QuestionGroupExists(string id);
}
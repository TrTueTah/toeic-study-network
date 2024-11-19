using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestions();
        Task<Question> GetQuestionById(string id);
        Task<Question> AddQuestion(Question question);
        Task<List<Question>> GetQuestionsByPartId(string partId);
        List<Question> ExtractQuestionsFromReading(List<string> lines);
        List<Question> ExtractQuestionsFromListening(List<string> lines);
    }
}
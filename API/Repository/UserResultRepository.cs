using API.Data;
using API.Interfaces;
using API.Models;
using API.Dtos.ResultDto;

namespace API.Repository;

public class UserResultRepository : IUserResultRepository
{
    private readonly ApplicationDbContext _context;

    public UserResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserResult CalculateAndSaveResult(SubmitResultDto submission)
    {
        // var questions = _context.Questions
        //     .Where(q => q.ExamId == submission.ExamId)
        //     .ToList();

        // if (!questions.Any())
        //     throw new Exception("Exam questions not found.");
        //
        // int readingCorrect = 0;
        // int listeningCorrect = 0;
        //
        // var detailResults = new List<DetailResult>();
        //
        // foreach (var question in questions)
        // {
        //     if (submission.Answers.TryGetValue(question.QuestionNumber, out string userAnswer))
        //     {
        //         bool isCorrect = string.Equals(userAnswer, question.CorrectAnswer, StringComparison.OrdinalIgnoreCase);
        //         
        //         if (question.QuestionNumber <= 100)
        //         {
        //             if (isCorrect) readingCorrect++;
        //         }
        //         else
        //         {
        //             if (isCorrect) listeningCorrect++;
        //         }
        //         
        //         detailResults.Add(new DetailResult
        //         {
        //             UserResultId = submission.UserId,
        //             QuestionNumber = question.QuestionNumber,
        //             UserAnswer = userAnswer,
        //             IsCorrect = isCorrect
        //         });
        //     }
        // }
        
        // int totalScore = CalculateToeicScore(readingCorrect, listeningCorrect);
        //
        // var userResult = new UserResult
        // {
        //     UserId = submission.UserId,
        //     ExamId = submission.ExamId,
        //     Score = totalScore,
        //     TimeTaken = submission.TimeTaken,
        //     Type = "Toeic"
        // };
        //
        // _context.UserResults.Add(userResult);
        // _context.DetailResults.AddRange(detailResults);
        // SaveChanges();
        //
        // return userResult;
        throw new NotImplementedException();
    }

    private int CalculateToeicScore(int readingCorrect, int listeningCorrect)
    {
        const int maxScorePerSection = 495;

        int readingScore = (readingCorrect * maxScorePerSection) / 100;
        int listeningScore = (listeningCorrect * maxScorePerSection) / 100;

        return readingScore + listeningScore;
    }

    private bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }
}

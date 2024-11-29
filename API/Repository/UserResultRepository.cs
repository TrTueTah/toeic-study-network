using API.Data;
using API.Interfaces;
using API.Models;
using API.Dtos.ResultDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Repository;
public class ScoreConfig
{
    public List<ScoreEntry> ReadingScores { get; set; }
    public List<ScoreEntry> ListeningScores { get; set; }
}

public class ScoreEntry
{
    public int Score { get; set; }
    public int Points { get; set; }
}

public class UserResultRepository : IUserResultRepository
{
    private readonly ApplicationDbContext _context;
    private const string ScoreFilePath = "Data/toeic_scores.json";

    public UserResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserResult CalculateAndSaveResult(SubmitResultDto submission)
    {
        var questionGroups = _context.QuestionGroups
            .Where(qg => qg.ExamId == submission.ExamId)
            .Include(qg => qg.Questions)
            .ToList();

        if (!questionGroups.Any())
            throw new Exception("Exam questions not found.");
        
        int readingCorrect = 0;
        int listeningCorrect = 0;
        
        var detailResults = new List<DetailResult>();

        var questions = questionGroups.SelectMany(qg => qg.Questions).ToList();

        foreach (var question in questions)
        {
            if (submission.Answers.TryGetValue(question.QuestionNumber, out string userAnswer))
            {
                bool isCorrect = string.Equals(userAnswer, question.CorrectAnswer, StringComparison.OrdinalIgnoreCase);

                if (question.QuestionNumber <= 100)
                {
                    if (isCorrect) readingCorrect++;
                }
                else
                {
                    if (isCorrect) listeningCorrect++;
                }

                // Ensure CorrectAnswer is stored in DetailResult
                detailResults.Add(new DetailResult
                {
                    UserResultId = submission.UserId,
                    QuestionNumber = question.QuestionNumber,
                    UserAnswer = userAnswer,
                    IsCorrect = isCorrect,
                    CorrectAnswer = question.CorrectAnswer
                });
            }
        }
        
        int totalScore = CalculateToeicScore(readingCorrect, listeningCorrect);
        
        var userResult = new UserResult
        {
            UserId = submission.UserId,
            ExamId = submission.ExamId,
            Score = totalScore,
            TimeTaken = submission.TimeTaken,
            CorrectAnswerAmount = readingCorrect + listeningCorrect,
            Type = submission.Type,
            DetailResults = detailResults
        };

        _context.UserResults.Add(userResult);
        _context.DetailResults.AddRange(detailResults);
        
        if (SaveChanges())
        {
            return userResult;
        }
        else
        {
            throw new Exception("Error saving results.");
        }
    }
    private ScoreConfig LoadToeicScores()
    {
        var json = File.ReadAllText(ScoreFilePath);
        return JsonConvert.DeserializeObject<ScoreConfig>(json);
    }

    private int CalculateToeicScore(int readingCorrect, int listeningCorrect)
    {
        var scoreConfig = LoadToeicScores();
        
        int readingScore = scoreConfig.ReadingScores
            .Where(x => x.Score <= readingCorrect)
            .OrderByDescending(x => x.Score)
            .FirstOrDefault()?.Points ?? 0;
        
        int listeningScore = scoreConfig.ListeningScores
            .Where(x => x.Score <= listeningCorrect)
            .OrderByDescending(x => x.Score)
            .FirstOrDefault()?.Points ?? 0;

        return readingScore + listeningScore;
    }
    
    private bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }
    public async Task<UserResultDto> GetDetailsResultAsync(string userResultId)
    {
        var userResult = await _context.UserResults
            .Where(ur => ur.UserResultId == userResultId)
            .Include(ur => ur.DetailResults)
            .FirstOrDefaultAsync();

        if (userResult == null)
        {
            throw new Exception("User result not found.");
        }

        var resultDto = new UserResultDto
        {
            UserResultId = userResult.UserResultId,
            UserId = userResult.UserId,
            ExamId = userResult.ExamId,
            Score = userResult.Score,
            Type = userResult.Type,
            CorrectAnswerAmount = userResult.CorrectAnswerAmount,
            TimeTaken = userResult.TimeTaken,
            DetailResults = userResult.DetailResults
                .OrderBy(dr => dr.QuestionNumber)
                .Select(dr => new DetailResultDto
                {
                    QuestionNumber = dr.QuestionNumber,
                    UserAnswer = dr.UserAnswer,
                    IsCorrect = dr.IsCorrect,
                    CorrectAnswer = dr.CorrectAnswer
                }).ToList()
        };

        return resultDto;
    }
}

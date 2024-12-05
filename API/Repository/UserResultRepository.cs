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

                detailResults.Add(new DetailResult
                {
                    UserResultId = submission.UserId,
                    QuestionNumber = question.QuestionNumber,
                    UserAnswer = userAnswer,
                    IsCorrect = isCorrect,
                    CorrectAnswer = question.CorrectAnswer
                });
            }
            else
            {
                detailResults.Add(new DetailResult
                {
                    UserResultId = submission.UserId,
                    QuestionNumber = question.QuestionNumber,
                    UserAnswer = null,
                    IsCorrect = false,
                    CorrectAnswer = question.CorrectAnswer
                });
            }
        }
        
        int totalScore = CalculateToeicScore(readingCorrect, listeningCorrect);
        int ReadingScore = GetReadingScore(readingCorrect);
        int ListeningScore = GetListeningScore(readingCorrect);
        
        var userResult = new UserResult
        {
            UserId = submission.UserId,
            ExamId = submission.ExamId,
            Score = totalScore,
            ReadingScore = ReadingScore,
            ListeningScore = ListeningScore,
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
        int readingScore = GetReadingScore(readingCorrect);
        int listeningScore = GetListeningScore(listeningCorrect);
        
        return readingScore + listeningScore;
    }

    private int GetReadingScore(int readingCorrect)
    {
        var scoreConfig = LoadToeicScores();
        
        int readingScore = scoreConfig.ReadingScores
            .Where(x => x.Score <= readingCorrect)
            .OrderByDescending(x => x.Score)
            .FirstOrDefault()?.Points ?? 0;
        
        return readingScore;
    }

    private int GetListeningScore(int listeningCorrect)
    {
        var scoreConfig = LoadToeicScores();
        
        int listeningScore = scoreConfig.ListeningScores
            .Where(x => x.Score <= listeningCorrect)
            .OrderByDescending(x => x.Score)
            .FirstOrDefault()?.Points ?? 0;
        
        return listeningScore;
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
        
        var readingCorrect = userResult.DetailResults
            .Count(dr => dr.QuestionNumber > 100 && dr.IsCorrect);

        var listeningCorrect = userResult.DetailResults
            .Count(dr => dr.QuestionNumber <= 100 && dr.IsCorrect);

        var unansweredCount = userResult.DetailResults
            .Count(dr => string.IsNullOrEmpty(dr.UserAnswer));

        var incorrectCount = 200 - userResult.CorrectAnswerAmount;

        var exam = _context.Exams
            .Where(ex => ex.Id == userResult.ExamId)
            .FirstOrDefault();
        
        var resultDto = new UserResultDto
        {
            UserResultId = userResult.UserResultId,
            UserId = userResult.UserId,
            ExamId = userResult.ExamId,
            ExamName = exam.Title,
            Score = userResult.Score,
            ReadingScore = userResult.ReadingScore,
            ListeningScore = userResult.ListeningScore,
            ReadingCorrectAnswerAmount = readingCorrect,
            ListeningCorrectAnswerAmount = listeningCorrect,
            Type = userResult.Type,
            CorrectAnswerAmount = userResult.CorrectAnswerAmount,
            IncorrectAnswerAmount = incorrectCount,
            WithoutAnswerAmount = unansweredCount,
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

    public List<UserResult> GetAllUserResultsByUserId(string userId)
    {
        return _context.UserResults
            .Where(ur => ur.UserId == userId)
            .ToList();
    }

    public QuestionDetailResultDto GetQuestionDetailResult(string detailResultId)
    {
        var detailResult = _context.DetailResults
            .Where(ur => ur.DetailResultId == detailResultId)
            .FirstOrDefault();
        
        if (detailResult == null)
        {
            throw new Exception("Detail result not found");
        }
        
        var userResult = _context.UserResults
            .Where(ur => ur.UserResultId == detailResult.UserResultId)
            .FirstOrDefault();
        
        if (userResult == null)
        {
            throw new Exception("User result not found");
        }
        
        var exam = _context.Exams
            .Where(ex => ex.Id == userResult.ExamId)
            .FirstOrDefault();
        
        if (exam == null)
        {
            throw new Exception("Exam not found");
        }
        
        var question = _context.Questions
            .Where(q => q.QuestionNumber == detailResult.QuestionNumber && q.Group.ExamId == exam.Id)
            .FirstOrDefault();

        if (question == null)
        {
            throw new Exception("Question not found");
        }
        
        var questionGroup = _context.QuestionGroups
            .Where(qg => qg.Id == question.GroupId)
            .FirstOrDefault();

        if (questionGroup == null)
        {
            throw new Exception("Question group not found");
        }

        
        var resultDto = new QuestionDetailResultDto()
        {
            DetailResultId = detailResultId,
            ExamName = exam.Title,
            UserAnswer = detailResult.UserAnswer,
            IsCorrect = detailResult.IsCorrect,
            Title = question.Title,
            AnswerA = question.AnswerA,
            AnswerB = question.AnswerB,
            AnswerC = question.AnswerC,
            AnswerD =  question.AnswerD,
            CorrectAnswer = question.CorrectAnswer,
            QuestionNumber = question.QuestionNumber,
            ImageFilesUrl = questionGroup.ImageFilesUrl,
            AudioFilesUrl = questionGroup.AudioFilesUrl,
        };

        return resultDto;
    }
}

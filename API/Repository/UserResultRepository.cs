using API.Data;
using API.Interfaces;
using API.Models;
using API.Dtos.ResultDto;
using API.Migrations;
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
        
        var questionCounts = new Dictionary<string, int>
        {
            { "Full Test", 200 },
            { "Part 1", 6 },
            { "Part 2", 25 },
            { "Part 3", 39 },
            { "Part 4", 30 },
            { "Part 5", 30 },
            { "Part 6", 16 },
            { "Part 7", 54 },
        };
        
        var types = submission.Type.Split(",");
        int totalQuestions = 0;

        foreach (var type in types)
        {
            var trimmedType = type.Trim();
            if (questionCounts.ContainsKey(trimmedType))
            {
                totalQuestions += questionCounts[trimmedType];
                        
            }
        }
        
        var validTypes = submission.Type.Split(",").Select(t => t.Trim()).ToList();
        
        var typeRanges = new Dictionary<string, (int Start, int End)>
        {
            { "Part 1", (1, 6) },
            { "Part 2", (7, 31) },
            { "Part 3", (32, 70) },
            { "Part 4", (71, 100) },
            { "Part 5", (101, 130) },
            { "Part 6", (131, 146) },
            { "Part 7", (147, 200) }
        };

        var allQuestions = _context.Questions
            .Where(q => q.Group.ExamId == submission.ExamId)
            .ToList();

        var filteredQuestions = allQuestions
            .Where(q =>
                submission.Type.Equals("Full Test", StringComparison.OrdinalIgnoreCase) ||
                validTypes.Any(type =>
                    typeRanges.ContainsKey(type) &&
                    q.QuestionNumber >= typeRanges[type].Start &&
                    q.QuestionNumber <= typeRanges[type].End
                )).ToList();
        
        var detailResults = new List<DetailResult>();

        foreach (var question in filteredQuestions)
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
        
        int? totalScore = null;
        int? ReadingScore = null;
        int? ListeningScore = null;
        
        if (submission.Type.Equals("Full Test", StringComparison.OrdinalIgnoreCase))
        {
            totalScore = CalculateToeicScore(readingCorrect, listeningCorrect);
            ReadingScore = GetReadingScore(readingCorrect);
            ListeningScore = GetListeningScore(readingCorrect);
        }
        
        var userResult = new UserResult
        {
            UserId = submission.UserId,
            ExamId = submission.ExamId,
            Score = totalScore,
            ReadingScore = ReadingScore,
            ListeningScore = ListeningScore,
            TimeTaken = 7200 - submission.TimeTaken,
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
            .Where(ur => ur.Id == userResultId)
            .Include(ur => ur.DetailResults)
            .FirstOrDefaultAsync();

        if (userResult == null)
        {
            throw new Exception("User result not found.");
        }
        
        var questionCounts = new Dictionary<string, int>
        {
            { "Full Test", 200 },
            { "Part 1", 6 },
            { "Part 2", 25 },
            { "Part 3", 39 },
            { "Part 4", 30 },
            { "Part 5", 30 },
            { "Part 6", 16 },
            { "Part 7", 54 },
        };
        
        var types = userResult.Type.Split(",");
        int totalQuestions = 0;

        foreach (var type in types)
        {
            var trimmedType = type.Trim();
            if (questionCounts.ContainsKey(trimmedType))
            {
                totalQuestions += questionCounts[trimmedType];
            }
        }
        
        var readingCorrect = userResult.DetailResults
            .Count(dr => dr.QuestionNumber > 100 && dr.IsCorrect);

        var listeningCorrect = userResult.DetailResults
            .Count(dr => dr.QuestionNumber <= 100 && dr.IsCorrect);

        var unansweredCount = userResult.DetailResults
            .Count(dr => string.IsNullOrEmpty(dr.UserAnswer));

        var incorrectCount = totalQuestions - userResult.CorrectAnswerAmount - unansweredCount;

        var exam = _context.Exams
            .FirstOrDefault(ex => ex.Id == userResult.ExamId);
        
        var resultDto = new UserResultDto
        {
            Id = userResult.Id,
            UserId = userResult.UserId,
            ExamId = userResult.ExamId,
            ExamName = exam.Title,
            Score = userResult.Score,
            ReadingScore = userResult.ReadingScore,
            ListeningScore = userResult.ListeningScore,
            ReadingCorrectAnswerAmount = readingCorrect,
            ListeningCorrectAnswerAmount = listeningCorrect,
            Type = userResult.Type,
            TotalQuestion = totalQuestions,
            CorrectAnswerAmount = userResult.CorrectAnswerAmount,
            IncorrectAnswerAmount = incorrectCount,
            WithoutAnswerAmount = unansweredCount,
            TimeTaken = userResult.TimeTaken,
            DetailResults = userResult.DetailResults
                .OrderBy(dr => dr.QuestionNumber)
                .Select(dr => new DetailResultDto
                {
                    Id = dr.Id,
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
            .FirstOrDefault(ur => ur.Id == detailResultId);
        
        if (detailResult == null)
        {
            throw new Exception("Detail result not found");
        }
        
        var userResult = _context.UserResults
            .FirstOrDefault(ur => ur.Id == detailResult.UserResultId);
        
        if (userResult == null)
        {
            throw new Exception("User result not found");
        }
        
        var exam = _context.Exams
            .FirstOrDefault(ex => ex.Id == userResult.ExamId);
        
        if (exam == null)
        {
            throw new Exception("Exam not found");
        }
        
        var question = _context.Questions
            .FirstOrDefault(q => q.QuestionNumber == detailResult.QuestionNumber && q.Group.ExamId == exam.Id);

        if (question == null)
        {
            throw new Exception("Question not found");
        }
        
        var questionGroup = _context.QuestionGroups
            .FirstOrDefault(qg => qg.Id == question.GroupId);

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

    public AnalysisUserResultDto GetAnalysisUserResult(string userId, string timeRange)
    {
        DateTime filterDate = DateTime.UtcNow;

        switch (timeRange.ToLower())
        {
            case "today":
                filterDate = DateTime.UtcNow.Date;
                break;
            case "3days":
                filterDate = DateTime.UtcNow.AddDays(-3);
                break;
            case "7days":
                filterDate = DateTime.UtcNow.AddDays(-7);
                break;
            case "1month":
                filterDate = DateTime.UtcNow.AddMonths(-1);
                break;
            case "6months":
                filterDate = DateTime.UtcNow.AddMonths(-6);
                break;
            case "1year":
                filterDate = DateTime.UtcNow.AddYears(-1);
                break;
            default:
                filterDate = DateTime.MinValue;
                break;
        }
        
        var userResults = _context.UserResults
            .Where(ur => ur.UserId == userId && ur.CreatedAt >= filterDate)
            .Include(ur => ur.DetailResults)
            .ToList();
        
        var totalTimeTakenList = userResults
            .Select(ur => ur.TimeTaken)
            .ToList();
        
        var totalTime = totalTimeTakenList.Count > 0 
            ? totalTimeTakenList.Sum()
            : 0;
        
        var averageTime = totalTimeTakenList.Count > 0 
            ? totalTimeTakenList.Average() 
            : 0;

        var totalScoreList = userResults
            .Select(ur => ur.Score)
            .ToList();

        var averageScore = totalScoreList.Count > 0
            ? Math.Round((double)(totalScoreList.Average() / 5.0)) * 5
            : 0;
        
        var maxScore = totalScoreList.Count > 0 
            ? totalScoreList.Max() 
            : 0;
        
        var totalReadingScore = userResults
            .Select(ur => ur.ReadingScore)
            .ToList();
        
        var averageReadingScore = totalReadingScore.Count > 0
            ? Math.Round((double)(totalReadingScore.Average() / 5.0)) * 5
            : 0;
        
        var maxReadingScore = totalReadingScore.Count > 0 
            ? totalReadingScore.Max() 
            : 0;
            
        var totalListeningScore = userResults
        .Select(ur => ur.ListeningScore)
        .ToList();
        
        var averageListeningScore = totalListeningScore.Count > 0
            ? Math.Round((double)(totalListeningScore.Average() / 5.0)) * 5
            : 0;
        
        var maxLísteningScore = totalListeningScore.Count > 0 
            ? totalListeningScore.Max() 
            : 0;
        
        var resultDto = new AnalysisUserResultDto()
        {
            TotalExamTaken = userResults.Count,
            TotalTimeTaken = totalTime,
            AverageTimeTaken = averageTime,
            AverageScore = averageScore,
            AverageReadingScore = averageReadingScore,
            AverageListeningScore = averageListeningScore,
            HighestScore = maxScore,
            HighestListeningScore = maxLísteningScore,
            HighestReadingScore = maxReadingScore,
        };

        return resultDto;
    }

    public List<UserResult> GetAllUserResultsByExamId(string userId, string examId)
    {
        return _context.UserResults
            .Where(ur => ur.UserId == userId && ur.ExamId == examId)
            .ToList();
    }
}

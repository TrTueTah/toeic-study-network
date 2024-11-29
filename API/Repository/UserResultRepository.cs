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

namespace API.Dtos.ResultDto;

public class AnalysisUserResultDto
{
    public int? TotalExamTaken { get; set; }
    public double? TotalTimeTaken { get; set; }
    public double? AverageTimeTaken { get; set; }
    public double? AverageScore { get; set; }
    public double? AverageReadingScore { get; set; }
    public double? AverageListeningScore { get; set; }
    public int? HighestScore { get; set; }
    public int? HighestReadingScore { get; set; }
    public int? HighestListeningScore { get; set; }
}
﻿namespace API.Dtos.ResultDto;

public class UserResultDto
{
    public string UserResultId { get; set; }
    public string UserId { get; set; }
    public string ExamId { get; set; }
    public int Score { get; set; }
    public int CorrectAnswerAmount { get; set; }
    public float TimeTaken { get; set; }
    public string Type { get; set; }
    public List<DetailResultDto> DetailResults { get; set; }
}
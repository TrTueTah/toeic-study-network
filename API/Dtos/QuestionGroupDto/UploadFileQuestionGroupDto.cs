namespace API.Dtos.QuestionGroupDto;

public class UploadFileQuestionGroupDto
{
    public string QuestionGroupId { get; set; }
    public ICollection<IFormFile> Files { get; set; }
}
namespace ToeicStudyNetwork.Dtos;

public class ToggleLikeResponse
{
    public string Status { get; set; }
    public bool IsLiked { get; set; }
    public string PostId { get; set; }
    public string UserId { get; set; }
}

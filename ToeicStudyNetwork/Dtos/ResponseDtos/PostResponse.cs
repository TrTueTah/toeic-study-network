using ToeicStudyNetwork.Models;
using ToeicStudyNetwork.ViewModels.Forum;

namespace ToeicStudyNetwork.Dtos;

public class PostResponse
{
    public List<PostViewModel> Results { get; set; } = new();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}

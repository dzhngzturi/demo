namespace API.DTO_s
{
    public class PhotoForApprovalDto
    {
        public int photoId { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public bool isApproved { get; set; }
    }
}

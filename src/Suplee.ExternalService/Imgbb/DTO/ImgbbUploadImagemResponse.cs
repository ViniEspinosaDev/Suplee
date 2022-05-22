namespace Suplee.ExternalService.Imgbb.DTO
{
    public class ImgbbUploadImageResponse
    {
        public ImgbbUploadImageDataResponse data { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
    }

    public class ImgbbUploadImageDataResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url_viewer { get; set; }
        public string Url { get; set; }
        public string Display_url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public int Time { get; set; }
        public int Expiration { get; set; }
        public ImgbbUploadImageImageResponse Image { get; set; }
        public ImgbbUploadImageThumbResponse Thumb { get; set; }
        public string Delete_url { get; set; }
    }

    public class ImgbbUploadImageImageResponse
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public string Mime { get; set; }
        public string Extension { get; set; }
        public string Url { get; set; }
    }

    public class ImgbbUploadImageThumbResponse
    {
        public string Filename { get; set; }
        public string Name { get; set; }
        public string Mime { get; set; }
        public string Extension { get; set; }
        public string Url { get; set; }
    }
}

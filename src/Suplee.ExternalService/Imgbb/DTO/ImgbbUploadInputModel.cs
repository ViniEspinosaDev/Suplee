namespace Suplee.ExternalService.Imgbb.DTO
{
    public class ImgbbUploadInputModel
    {
        public ImgbbUploadInputModel(string image, string name)
        {
            this.image = image;
            this.name = name;
        }

        /// <summary>
        /// Base 64
        /// </summary>
        public string image { get; set; } // TODO: Adicionar rótulo nessa óprra
        public string name { get; set; }
    }
}

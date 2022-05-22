namespace Suplee.ExternalService.Imgbb.DTO
{
    public class ImgbbConfiguracao
    {
        /// <summary>
        /// URL da API do Imgbb
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Tempo de expiração do request
        /// </summary>
        public string Expiration { get; set; }

        /// <summary>
        /// Chave necessária para Upload
        /// </summary>
        public string Key { get; set; }
    }
}

using System.Collections.Generic;

namespace Suplee.ExternalService.Imgbb.DTO
{
    public class ResponseApi<TResponse>
    {
        public bool Success { get; set; }
        public TResponse Data { get; set; }
        public List<string> Errors { get; set; }
    }
}

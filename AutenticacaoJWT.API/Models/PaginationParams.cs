using System.ComponentModel.DataAnnotations;

namespace AutenticacaoJWT.API.Models
{
    public class PaginationParams
    {
        public int PageNumber { get; set; }

        [Range(0, 50, ErrorMessage = "O máximo de itens por página é 50.")]
        public int PageSize { get; set; }
    }
}

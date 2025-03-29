using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Entities.DTO
{
    public record RegisterRequest(
        string? Email,
        String? Password,
        string? PersonName,
        GenderOptions Gender
        );

}

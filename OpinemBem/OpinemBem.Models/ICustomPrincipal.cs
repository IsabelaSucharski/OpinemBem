using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OpinemBem.Models
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Id { get; set; }
        string Nome { get; set; }
        string Email { get; set; }
    }
}

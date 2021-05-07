using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public interface ISelectList
    {
        Task<SelectList> GetSelectList();
        Task<SelectList> GetSelectList(object selected);
    }
}

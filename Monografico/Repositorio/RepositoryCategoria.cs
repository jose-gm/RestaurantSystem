using Microsoft.AspNetCore.Mvc.Rendering;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryCategoria : RepositoryBase<Categoria>, ISelectList
    {

        public RepositoryCategoria(Contexto contexto): base(contexto)
        {
        }

        public async Task<CategoriaViewModel> BuscarCategoriaViewModel(int id)
        {
            var categoria = await _contexto.Producto.FindAsync(id);
            return new CategoriaViewModel()
            {
                IdCategoria = categoria.IdCategoria,
                Descripcion = categoria.Descripcion,
                ImagenEncoded = categoria.Imagen,
            };
        }

        public async Task<SelectList> GetSelectList()
        {
            var list = await GetList(x => true);
            return new SelectList(list, "IdCategoria", "Descripcion");
        }

        public Task<SelectList> GetSelectList(object selected)
        {
            throw new NotImplementedException();
        }
    }
}

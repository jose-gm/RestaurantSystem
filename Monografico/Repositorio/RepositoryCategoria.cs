using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            var categoria = await _contexto.Categoria.FindAsync(id);
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

        public async Task<List<Categoria>> GetAllWithProductos()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                lista = await _contexto.Categoria.Include(x => x.Productos).AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public async Task<Categoria> FindWithProductos(int id)
        {
            Categoria categoria = null;
            try
            {
                categoria = await _contexto.Categoria.Include(x => x.Productos).AsNoTracking().SingleOrDefaultAsync(x => x.IdCategoria == id);
            }
            catch (Exception)
            {

                throw;
            }

            return categoria;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Models;
using Monografico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryIngrediente : RepositoryBase<Ingrediente>
    {

        public RepositoryIngrediente(Contexto contexto) : base(contexto)
        {
        }

        public async Task<Ingrediente> FindWithInventarioAsync(int id)
        {
            Ingrediente ingrediente = null;
            try
            {
                ingrediente = await _contexto.Ingrediente.Include(x => x.Inventario).SingleOrDefaultAsync(c => c.IdIngrediente == id);
            }
            catch (Exception)
            { throw; }
            return ingrediente;
        }

        public async Task<IngredienteViewModel> BuscarIngredienteViewModel(int id)
        {
            var ingrediente = await Find(id);

            return new IngredienteViewModel() { 
                IdIngrediente = ingrediente.IdIngrediente,
                Descripcion = ingrediente.Descripcion,
                Costo = ingrediente.Costo,
                TieneInventario = ingrediente.LlevaInventario
            };
        }

        public async Task<bool> EliminarConInventario(int id)
        {
            bool paso = false;
            try
            {
                var entity = _contexto.Ingrediente.Include(x => x.Inventario).SingleOrDefault(c => c.IdIngrediente == id);
                _contexto.Ingrediente.Remove(entity);

                await _contexto.SaveChangesAsync();
                paso = true;

                _contexto.Dispose();
            }
            catch (Exception)
            { throw; }
            return paso;
        }

        public async Task<List<Ingrediente>> GetAllWithInventory(Expression<Func<Ingrediente, bool>> expression)
        {
            List<Ingrediente> lista = new List<Ingrediente>();
            try
            {
                lista = await _contexto.Ingrediente.Include(i => i.Inventario).Where(expression).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }
    }
}

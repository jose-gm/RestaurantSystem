using Microsoft.EntityFrameworkCore;
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
    public class RepositoryIngrediente : RepositoryBase<Ingrediente>
    {

        public RepositoryIngrediente(Contexto contexto) : base(contexto)
        {
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
    }
}

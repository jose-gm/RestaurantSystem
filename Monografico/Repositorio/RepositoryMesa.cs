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
    public class RepositoryMesa : RepositoryBase<Mesa>
    {
        private readonly Contexto _contexto;

        public RepositoryMesa(Contexto contexto): base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<Mesa> FindWithCuentasAsync(int id)
        {
            Mesa mesa = null;
            try
            {
                mesa = await _contexto.Mesa.Include(x => x.Cuentas).AsNoTracking().SingleOrDefaultAsync(x => x.IdMesa == id);
            }
            catch (Exception)
            {

                throw;
            }
            return mesa;
        }
        
        public async Task<MesaViewModel> FindAsViewModel(int id)
        {
            MesaViewModel model = null;
            try
            {
                var mesa = await _contexto.Mesa.Include(x => x.Cuentas).AsNoTracking().SingleOrDefaultAsync(x => x.IdMesa == id);
                var zona = await _contexto.Zona.FindAsync(mesa.IdZona);

                model = new MesaViewModel()
                {
                    IdMesa = mesa.IdMesa,
                    IdZona = mesa.IdZona,
                    Asientos = mesa.Asientos,
                    Numero = mesa.Numero,
                    Zona = zona
                };

            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public async Task<List<MesaViewModel>> GetAllAsViewModel()
        {
            List<MesaViewModel> list = new List<MesaViewModel>();
            try
            {
                var mesas = await _contexto.Mesa.AsNoTracking().ToListAsync();

                foreach (var item in mesas)
                {
                    var zona = await _contexto.Zona.FindAsync(item.IdZona);

                    list.Add(new MesaViewModel() {
                        IdMesa = item.IdMesa,
                        IdZona = item.IdZona,
                        Asientos = item.Asientos,
                        Numero = item.Numero,
                        Zona = zona
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }
    }
}

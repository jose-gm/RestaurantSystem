using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class RepositoryZona : RepositoryBase<Zona>, ISelectList
    {
        private readonly Contexto _contexto;

        public RepositoryZona(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Zona>> GetAllWithMesa()
        {
            List<Zona> lista = null;
            try
            {
                lista = await _contexto.Zona.Include(x => x.Mesas).ThenInclude(y => y.Cuentas).AsNoTracking().ToListAsync();
                lista.ForEach(a => a.Mesas.ForEach(b => b.Cuentas.RemoveAll(c => c.Activa == false)));
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public async Task<Zona> FindWithMesasAsync(int id)
        {
            Zona zona = null;
            try
            {
                zona = await _contexto.Zona.Include(x => x.Mesas).AsNoTracking().SingleOrDefaultAsync(x => x.IdZona == id);
            }
            catch (Exception)
            {

                throw;
            }
            return zona;
        }
        
        public async Task<List<MesaViewModel>> GetAllMesas(Expression<Func<Zona, bool>> expression)
        {
            List<MesaViewModel> model = new List<MesaViewModel>();
            try
            {
                var zonas = await _contexto.Zona.Include(x => x.Mesas).ThenInclude(x => x.Cuentas).Where(expression).AsNoTracking().ToListAsync();
                zonas.ForEach(a => a.Mesas.ForEach(b => b.Cuentas.RemoveAll(c => c.Activa == false)));

                foreach (var item in zonas)
                {
                    foreach (var mesa in item.Mesas)
                    {
                        model.Add(new MesaViewModel()
                        {
                            IdMesa = mesa.IdMesa,
                            Asientos = mesa.Asientos,
                            IdZona = item.IdZona,
                            Numero = mesa.Numero,
                            ZonaDescripcion = item.Descripcion,
                            Estado = (mesa.Cuentas.Count > 0) ? "Ocupado" : "Libre"
                        }); ;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }
 
        public async Task<SelectList> GetSelectList()
        {
            var list = await GetList(x => true);
            return new SelectList(list, "IdZona", "Descripcion");
        }

        public Task<SelectList> GetSelectList(object selected)
        {
            throw new NotImplementedException();
        }
    }
}

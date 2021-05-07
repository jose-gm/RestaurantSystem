using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryConfiguracion : RepositoryBase<Configuracion>
    {
        public RepositoryConfiguracion(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async override Task<bool> Update(Configuracion entity)
        {
            bool paso = false;
            try
            {
                if(_contexto.Configuracion.Count() <= 0)
                {
                    _contexto.Configuracion.Add(entity);
                }
                else
                {
                    _contexto.Configuracion.Update(entity);
                }
                await _contexto.SaveChangesAsync();
                paso = true;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        
        public async Task<Configuracion> Get()
        {
            Configuracion configuracion = null;
            try
            {
                configuracion = _contexto.Configuracion.Where(x => true).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return configuracion;
        }
    }
}

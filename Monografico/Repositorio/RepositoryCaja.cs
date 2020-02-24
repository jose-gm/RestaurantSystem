using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Migrations;
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
    public class RepositoryCaja : RepositoryBase<Caja>
    {
        public RepositoryCaja(Contexto contexto) : base(contexto)
        {
        }

        public async Task<bool> AbrirCaja(AperturaCaja apertura)
        {
            var paso = false;
            try
            {
                Caja caja = new Caja() { 
                    Fecha = apertura.Fecha,
                    Monto = apertura.MontoInicial,
                    Estado = "Abierta",
                    IdUsario = apertura.IdUsurio
                };

                apertura.Caja = caja;

                _contexto.AperturaCaja.Add(apertura);
                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }
        
        public async Task<bool> CerrarCaja(Usuario usuario, CierreCaja cierre)
        {
            var paso = false;
            try
            {
                var caja = await _contexto.Caja.Where(x => (x.IdUsario == usuario.Id) && (x.Estado.Equals("Abierta"))).AsNoTracking().FirstOrDefaultAsync();
                caja.Estado = "Cerrado";
                cierre.IdCaja = caja.IdCaja;
                cierre.Cuadre = (string.IsNullOrEmpty(cierre.Cuadre)) ? "POSITIVO" : cierre.Cuadre;
                caja.Cierre = cierre;

                _contexto.Caja.Update(caja);
                await _contexto.SaveChangesAsync();

                paso = true;
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public async Task<bool> IsCajaAbierta(Usuario usuario)
        {
            var flag = false;
            try
            {
                var caja = await _contexto.Caja.Where(x => (x.IdUsario == usuario.Id) && (x.Estado.Equals("Abierta"))).AsNoTracking().FirstOrDefaultAsync();
                flag = (caja == null) ? false : true;
            }
            catch (Exception)
            {

                throw;
            }
            return flag;
        }
        
        public async Task<bool> IsCajaAbierta()
        {
            var flag = false;
            try
            {
                var caja = await _contexto.Caja.Where(x => x.Estado.Equals("Abierta")).AsNoTracking().FirstOrDefaultAsync();
                flag = (caja == null) ? false : true;
            }
            catch (Exception)
            {

                throw;
            }
            return flag;
        }

        public async Task<AperturaCaja> GetAperturaCaja(Usuario usuario)
        {
            AperturaCaja apertura = null;
            try
            {
                apertura = await _contexto.AperturaCaja
                                    .Include(x => x.Caja)
                                    .Where(x => (x.Caja.Estado.Equals("Abierta")))
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return apertura;
        }

        public async Task<List<CajaViewModel>> GetAllCajaViewModelAsync(Expression<Func<Caja, bool>> expression)
        {
            List<CajaViewModel> list = new List<CajaViewModel>();
            try
            {
                var cajas = await _contexto.Caja.Include(x => x.Apertura).Include(x => x.Cierre).Where(expression).AsNoTracking().ToListAsync();

                foreach (var caja in cajas)
                {
                    var usuario = await _contexto.Usuario.FindAsync(caja.Apertura.IdUsurio);

                    list.Add(new CajaViewModel() { 
                        FechaApertura = caja.Apertura.Fecha,
                        Usuario = usuario.Nombre + usuario.Apellido,
                        MontoInicial = caja.Apertura.MontoInicial,
                        Efectivo = (caja.Cierre == null) ? 0 : caja.Cierre.Efectivo,
                        Tarjeta = (caja.Cierre == null) ? 0 : caja.Cierre.Tarjeta,
                        Cheque = (caja.Cierre == null) ? 0 : caja.Cierre.Cheque,
                        Diferencia = (caja.Cierre == null) ? 0 : caja.Cierre.Diferencia,
                        Cuadre = (caja.Cierre == null) ? "" : caja.Cierre.Cuadre,
                        Estado = caja.Estado,
                        FechaCierre = (caja.Cierre == null) ? "No ha sido cerrada" : caja.Cierre.Fecha.Date.ToString(),
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

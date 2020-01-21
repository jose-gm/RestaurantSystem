using Microsoft.EntityFrameworkCore;
using Monografico.Data;
using Monografico.Migrations;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                caja.Cierre = new CierreCaja() { 
                    IdCaja = caja.IdCaja,
                    Fecha = cierre.Fecha,
                    MontoCaja = cierre.MontoCaja,
                    TotalContado = cierre.TotalContado,
                    Diferencia = cierre.Diferencia,
                    Cuadre = (string.IsNullOrEmpty(cierre.Cuadre)) ? "POSITIVO" : cierre.Cuadre
                };

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

        public async Task<AperturaCaja> GetAperturaCaja(Usuario usuario)
        {
            AperturaCaja apertura = null;
            try
            {
                apertura = await _contexto.AperturaCaja
                                    .Include(x => x.Caja)
                                    .Where(x => (x.IdUsurio == usuario.Id) && (x.Caja.Estado.Equals("Abierta")))
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return apertura;
        }
    }
}

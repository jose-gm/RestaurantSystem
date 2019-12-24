using Microsoft.AspNetCore.Identity;
using Monografico.Data;
using Monografico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Repositorio
{
    public class RepositoryWrapper
    {
        private readonly Contexto _contexto;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private RepositoryIngrediente _ingrediente;
        private RepositoryCategoria _categoria;
        private RepositoryInventario _inventario;
        private RepositoryCuenta _cuenta;
        private RepositoryOrden _orden;
        private RepositoryFactura _factura;
        private RepositoryProducto _producto;
        private RepositoryZona _zona;
        private RepositoryMesa _mesa;
        private RepositoryUsuario _usuario;
        private RepositoryRol _rol;
        private RepositoryAjusteInventario _ajusteinventario;

        public RepositoryWrapper(Contexto contexto)
        {
            _contexto = contexto;
        }

        public RepositoryIngrediente Ingrediente
        {
            get
            {
                if (_ingrediente == null)
                    _ingrediente = new RepositoryIngrediente(_contexto);
                return _ingrediente;
            }
        }
        public RepositoryCategoria Categoria
        {
            get
            {
                if (_categoria == null)
                    _categoria = new RepositoryCategoria(_contexto);
                return _categoria;
            }
        }

        public RepositoryInventario Inventario
        {
            get
            {
                if (_inventario == null)
                    _inventario = new RepositoryInventario(_contexto);
                return _inventario;
            }
        }

        public RepositoryOrden Orden
        {
            get
            {
                if (_orden == null)
                    _orden = new RepositoryOrden(_contexto);
                return _orden;
            }
        }

        public RepositoryFactura Factura
        {
            get
            {
                if (_factura == null)
                    _factura = new RepositoryFactura(_contexto);
                return _factura;
            }
        }

        public RepositoryProducto Producto
        {
            get
            {
                if (_producto == null)
                    _producto = new RepositoryProducto(_contexto);
                return _producto;
            }
        }

        public RepositoryUsuario Usuario
        {
            get
            {
                if (_usuario == null)
                    _usuario = new RepositoryUsuario(_userManager, _roleManager);
                return _usuario;
            }
        }

        public RepositoryRol Rol
        {
            get
            {
                if (_rol == null)
                    _rol = new RepositoryRol(_roleManager);
                return _rol;
            }
        }
        public RepositoryAjusteInventario AjusteInventario
        {
            get
            {
                if (_ajusteinventario == null)
                    _ajusteinventario = new RepositoryAjusteInventario(_contexto);
                return _ajusteinventario;
            }
        }

        public RepositoryCuenta Cuenta
        {
            get
            {
                if (_cuenta == null)
                    _cuenta = new RepositoryCuenta(_contexto);
                return _cuenta;
            }
        }

        public RepositoryZona Zona
        {
            get
            {
                if (_zona == null)
                    _zona = new RepositoryZona(_contexto);
                return _zona;
            }
        }

        public RepositoryMesa Mesa
        {
            get
            {
                if (_mesa == null)
                    _mesa = new RepositoryMesa(_contexto);
                return _mesa;
            }
        }

        public async Task<bool> CheckConnectionAsync()
        {
            return await _contexto.Database.CanConnectAsync();
        }
    }
}

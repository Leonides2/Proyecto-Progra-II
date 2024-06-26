﻿using Proyecto_Progra_II.Models;

namespace Services.Interfaces
{
    public interface ISucursalService
    {
        Task<List<Sucursal>> GetSucursales();
        Task<Sucursal> GetSucursales(int id);

        Task<Sucursal> PostSucursal(Sucursal sucursal);

        Task<Sucursal> PutSucursal(int id, Sucursal sucursal);
        Task<Sucursal> DeleteSucursal(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VeterinariaGenesis.Domain.Entities;

namespace VeterinariaGenesis.Client.Services
{
    public class AppStateService
    {
        private readonly HttpClient _http;

        // Caché local (se refrescan al cargar cada módulo)
        public List<Cliente> Clientes { get; private set; } = new();
        public List<Mascota> Mascotas { get; private set; } = new();
        public List<Trabajador> Trabajadores { get; private set; } = new();
        public List<Proveedor> Proveedores { get; private set; } = new();
        public List<Producto> Productos { get; private set; } = new();
        public List<Factura> Facturas { get; private set; } = new();

        public bool IsLoggedIn { get; private set; } = false;

        public event Action? OnChange;

        public AppStateService(HttpClient http)
        {
            _http = http;
        }

        // ============== AUTH ==============
        public bool Login(string username, string password)
        {
            if (username == "Administrador" && password == "123456")
            {
                IsLoggedIn = true;
                NotifyStateChanged();
                return true;
            }
            return false;
        }

        public void Logout()
        {
            IsLoggedIn = false;
            NotifyStateChanged();
        }

        // ============== CLIENTES ==============
        public async Task LoadClientesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Cliente>>("api/clientes");
            Clientes = result ?? new();
            NotifyStateChanged();
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            var response = await _http.PostAsJsonAsync("api/clientes", cliente);
            var saved = await response.Content.ReadFromJsonAsync<Cliente>();
            if (saved != null) Clientes.Add(saved);
            NotifyStateChanged();
        }

        // ============== MASCOTAS ==============
        public async Task LoadMascotasAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Mascota>>("api/mascotas");
            Mascotas = result ?? new();
            NotifyStateChanged();
        }

        public async Task AddMascotaAsync(Mascota mascota)
        {
            var response = await _http.PostAsJsonAsync("api/mascotas", mascota);
            var saved = await response.Content.ReadFromJsonAsync<Mascota>();
            if (saved != null) Mascotas.Add(saved);
            NotifyStateChanged();
        }

        // ============== TRABAJADORES ==============
        public async Task LoadTrabajadoresAsync()
        {
            try 
            {
                var result = await _http.GetFromJsonAsync<List<Trabajador>>("api/trabajadores");
                Trabajadores = result ?? new();
                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando trabajadores: {ex.Message}");
            }
        }

        public async Task<bool> AddTrabajadorAsync(Trabajador trabajador)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/trabajadores", trabajador);
                if (response.IsSuccessStatusCode)
                {
                    var saved = await response.Content.ReadFromJsonAsync<Trabajador>();
                    if (saved != null) Trabajadores.Add(saved);
                    NotifyStateChanged();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando trabajador: {ex.Message}");
                return false;
            }
        }

        // ============== PRODUCTOS ==============
        public async Task LoadProductosAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Producto>>("api/productos");
            Productos = result ?? new();
            NotifyStateChanged();
        }

        public async Task AddProductoAsync(Producto producto)
        {
            var response = await _http.PostAsJsonAsync("api/productos", producto);
            var saved = await response.Content.ReadFromJsonAsync<Producto>();
            if (saved != null) Productos.Add(saved);
            NotifyStateChanged();
        }

        // ============== FACTURAS ==============
        public async Task LoadFacturasAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Factura>>("api/facturas");
            Facturas = result ?? new();
            NotifyStateChanged();
        }

        public async Task AddFacturaAsync(Factura factura)
        {
            var response = await _http.PostAsJsonAsync("api/facturas", factura);
            var saved = await response.Content.ReadFromJsonAsync<Factura>();
            if (saved != null) Facturas.Add(saved);
            NotifyStateChanged();
        }

        // ============== PROVEEDORES ==============
        public async Task LoadProveedoresAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Proveedor>>("api/proveedores");
            Proveedores = result ?? new();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

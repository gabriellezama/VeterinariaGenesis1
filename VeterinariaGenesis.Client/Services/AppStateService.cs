using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Application.DTOs;

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
        public string LastErrorMessage { get; private set; } = string.Empty;

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
            try
            {
                var result = await _http.GetFromJsonAsync<List<Cliente>>("api/Clientes");
                if (result != null)
                {
                    Clientes = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando clientes: {ex.Message}");
            }
        }

        public async Task<bool> AddClienteAsync(Cliente cliente)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Clientes", cliente);
                if (response.IsSuccessStatusCode)
                {
                    await LoadClientesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando cliente: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateClienteAsync(Cliente cliente)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Clientes/{cliente.Id}", cliente);
                if (response.IsSuccessStatusCode)
                {
                    await LoadClientesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando cliente: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteClienteAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Clientes/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadClientesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando cliente: {ex.Message}");
                return false;
            }
        }

        // ============== MASCOTAS ==============
        public async Task LoadMascotasAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Mascota>>("api/Mascotas");
                if (result != null)
                {
                    Mascotas = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando mascotas: {ex.Message}");
            }
        }

        public async Task<bool> AddMascotaAsync(Mascota mascota)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Mascotas", mascota);
                if (response.IsSuccessStatusCode)
                {
                    await LoadMascotasAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando mascota: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateMascotaAsync(Mascota mascota)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Mascotas/{mascota.Id}", mascota);
                if (response.IsSuccessStatusCode)
                {
                    await LoadMascotasAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando mascota: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteMascotaAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Mascotas/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadMascotasAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando mascota: {ex.Message}");
                return false;
            }
        }

        // ============== TRABAJADORES ==============
        public async Task LoadTrabajadoresAsync()
        {
            try 
            {
                var result = await _http.GetFromJsonAsync<List<Trabajador>>("api/Trabajadores");
                if (result != null)
                {
                    Trabajadores = result;
                    NotifyStateChanged();
                }
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
                var response = await _http.PostAsJsonAsync("api/Trabajadores", trabajador);
                if (response.IsSuccessStatusCode)
                {
                    // En lugar de intentar leer la respuesta (que puede fallar si hay problemas de formato),
                    // simplemente refrescamos la lista completa del servidor.
                    await LoadTrabajadoresAsync();
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

        public async Task<bool> UpdateTrabajadorAsync(Trabajador trabajador)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Trabajadores/{trabajador.Id}", trabajador);
                if (response.IsSuccessStatusCode)
                {
                    await LoadTrabajadoresAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando trabajador: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteTrabajadorAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Trabajadores/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadTrabajadoresAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando trabajador: {ex.Message}");
                return false;
            }
        }

        // ============== PRODUCTOS ==============
        public async Task LoadProductosAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Producto>>("api/Productos");
                if (result != null)
                {
                    Productos = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando productos: {ex.Message}");
            }
        }

        public async Task<bool> AddProductoAsync(Producto producto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Productos", producto);
                if (response.IsSuccessStatusCode)
                {
                    await LoadProductosAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando producto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProductoAsync(Producto producto)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Productos/{producto.Id}", producto);
                if (response.IsSuccessStatusCode)
                {
                    await LoadProductosAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando producto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProductoAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Productos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadProductosAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando producto: {ex.Message}");
                return false;
            }
        }

        // ============== FACTURAS ==============
        public async Task LoadFacturasAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Factura>>("api/Facturas");
                if (result != null)
                {
                    Facturas = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando facturas: {ex.Message}");
            }
        }

        public async Task<bool> AddFacturaAsync(Factura factura)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Facturas", factura);
                if (response.IsSuccessStatusCode)
                {
                    await LoadFacturasAsync();
                    return true;
                }
                
                LastErrorMessage = await response.Content.ReadAsStringAsync();
                return false;
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                Console.WriteLine($"Error guardando factura: {ex.Message}");
                return false;
            }
        }

        // ============== PROVEEDORES ==============
        public async Task LoadProveedoresAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Proveedor>>("api/Proveedores");
                if (result != null)
                {
                    Proveedores = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando proveedores: {ex.Message}");
            }
        }

        public async Task<bool> AddProveedorAsync(Proveedor proveedor)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Proveedores", proveedor);
                if (response.IsSuccessStatusCode)
                {
                    await LoadProveedoresAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando proveedor: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProveedorAsync(Proveedor proveedor)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Proveedores/{proveedor.Id}", proveedor);
                if (response.IsSuccessStatusCode)
                {
                    await LoadProveedoresAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando proveedor: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProveedorAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Proveedores/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadProveedoresAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando proveedor: {ex.Message}");
                return false;
            }
        }

        // ============== GESTIÓN MÉDICA ==============
        public async Task<List<LineaTiempoItemDto>> GetHistorialClinicoAsync(Guid mascotaId)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<LineaTiempoItemDto>>($"api/GestionMedica/historial/{mascotaId}") ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando historial: {ex.Message}");
                return new();
            }
        }

        public async Task<bool> AddEventoMedicoAsync(object request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/GestionMedica/evento", request);
                if (response.IsSuccessStatusCode)
                {
                    NotifyStateChanged();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando evento médico: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateEventoMedicoAsync(Guid id, object request)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/GestionMedica/evento/{id}", request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando evento médico: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteEventoMedicoAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/GestionMedica/evento/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando evento médico: {ex.Message}");
                return false;
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

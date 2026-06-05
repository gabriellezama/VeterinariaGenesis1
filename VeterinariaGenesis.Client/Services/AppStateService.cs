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
        public List<Cita> Citas { get; private set; } = new();
        public List<NotificacionWhatsappLog> NotificacionLogs { get; private set; } = new();
        public List<Gasto> Gastos { get; private set; } = new();

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

        public async Task<bool> DeleteFacturaAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Facturas/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadFacturasAsync();
                    await LoadProductosAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando factura: {ex.Message}");
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

        // ============== CITAS ==============
        public async Task LoadCitasAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Cita>>("api/Citas");
                if (result != null)
                {
                    Citas = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando citas: {ex.Message}");
            }
        }

        public async Task<bool> AddCitaAsync(Cita cita)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Citas", cita);
                if (response.IsSuccessStatusCode)
                {
                    await LoadCitasAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando cita: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCitaAsync(Cita cita)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Citas/{cita.Id}", cita);
                if (response.IsSuccessStatusCode)
                {
                    await LoadCitasAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando cita: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCitaAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Citas/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadCitasAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando cita: {ex.Message}");
                return false;
            }
        }

        public async Task LoadNotificacionLogsAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<NotificacionWhatsappLog>>("api/Citas/notificaciones-log");
                if (result != null)
                {
                    NotificacionLogs = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando logs de notificaciones: {ex.Message}");
            }
        }

        public async Task<bool> SendManualNotificationAsync(Guid citaId)
        {
            try
            {
                var response = await _http.PostAsync($"api/Citas/{citaId}/notificar-manual", null);
                if (response.IsSuccessStatusCode)
                {
                    await LoadCitasAsync();
                    await LoadNotificacionLogsAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando notificación manual: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteNotificacionLogAsync(Guid logId)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Citas/notificaciones-log/{logId}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadNotificacionLogsAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando recordatorio: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ClearNotificacionLogsAsync()
        {
            try
            {
                var response = await _http.DeleteAsync("api/Citas/notificaciones-log");
                if (response.IsSuccessStatusCode)
                {
                    await LoadNotificacionLogsAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error limpiando recordatorios: {ex.Message}");
                return false;
            }
        }

        // ============== GASTOS ==============
        public async Task LoadGastosAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Gasto>>("api/Gastos");
                if (result != null)
                {
                    Gastos = result;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando gastos: {ex.Message}");
            }
        }

        public async Task<bool> AddGastoAsync(Gasto gasto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Gastos", gasto);
                if (response.IsSuccessStatusCode)
                {
                    await LoadGastosAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error guardando gasto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteGastoAsync(Guid id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Gastos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    await LoadGastosAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando gasto: {ex.Message}");
                return false;
            }
        }

        // ============== RESET / LIMPIEZA DE DATOS ==============

        /// <summary>
        /// Borra facturas, gastos, citas, historial médico, clientes y mascotas.
        /// Conserva trabajadores y catálogo de productos.
        /// </summary>
        public async Task<bool> ResetDatosOperativosAsync()
        {
            try
            {
                var response = await _http.DeleteAsync("api/Reset/datos-operativos");
                if (response.IsSuccessStatusCode)
                {
                    // Limpiar caché local
                    Facturas = new();
                    Gastos = new();
                    Citas = new();
                    Clientes = new();
                    Mascotas = new();
                    NotificacionLogs = new();
                    NotifyStateChanged();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en reset operativo: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Borra absolutamente todo: facturas, gastos, clientes, mascotas,
        /// trabajadores, productos, proveedores, citas e historial médico.
        /// </summary>
        public async Task<bool> ResetTotalAsync()
        {
            try
            {
                var response = await _http.DeleteAsync("api/Reset/todo");
                if (response.IsSuccessStatusCode)
                {
                    Facturas = new();
                    Gastos = new();
                    Citas = new();
                    Clientes = new();
                    Mascotas = new();
                    Trabajadores = new();
                    Productos = new();
                    Proveedores = new();
                    NotificacionLogs = new();
                    NotifyStateChanged();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en reset total: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SeedDemoDataAsync()

        {
            try
            {
                // 1. Cargar datos actuales
                await LoadClientesAsync();
                await LoadTrabajadoresAsync();
                await LoadProductosAsync();
                await LoadCitasAsync();
                await LoadGastosAsync();

                // Si ya hay clientes, no sembramos de nuevo para evitar duplicidad
                if (Clientes.Count > 0)
                {
                    return false;
                }

                // A. Registrar Trabajadores
                var trab1 = new Trabajador { Id = Guid.NewGuid(), Nombres = "Melvin", Apellidos = "Martínez", Telefono = "8888-1111", Rol = "Veterinario", Especialidad = "Cirugía", SalarioBase = 18000m };
                var trab2 = new Trabajador { Id = Guid.NewGuid(), Nombres = "Gabriel", Apellidos = "Lezama", Telefono = "8888-2222", Rol = "Veterinario", Especialidad = "Vacunas", SalarioBase = 15000m };
                var trab3 = new Trabajador { Id = Guid.NewGuid(), Nombres = "Carolina", Apellidos = "Solís", Telefono = "8888-3333", Rol = "Asistente", Especialidad = "Estética", SalarioBase = 10000m };
                
                await _http.PostAsJsonAsync("api/Trabajadores", trab1);
                await _http.PostAsJsonAsync("api/Trabajadores", trab2);
                await _http.PostAsJsonAsync("api/Trabajadores", trab3);

                // B. Registrar Clientes
                var cli1 = new Cliente { Id = Guid.NewGuid(), Nombres = "Katherine", Apellidos = "Dávila", Identificacion = "001-121295-0002A", Telefono = "8888-9999", Email = "katherine@example.com", Direccion = "Semaforos de la cañada 2c al sur, Managua" };
                var cli2 = new Cliente { Id = Guid.NewGuid(), Nombres = "Yasser", Apellidos = "Lezama", Identificacion = "001-200590-0005C", Telefono = "7846-6021", Email = "yasser@example.com", Direccion = "Semaforos de la cañada 10 vara al este, Managua" };
                var cli3 = new Cliente { Id = Guid.NewGuid(), Nombres = "Gabriela", Apellidos = "Montenegro", Identificacion = "161-050492-0001B", Telefono = "8666-5555", Email = "gabriela@example.com", Direccion = "Reparto San Juan, León" };

                await _http.PostAsJsonAsync("api/Clientes", cli1);
                await _http.PostAsJsonAsync("api/Clientes", cli2);
                await _http.PostAsJsonAsync("api/Clientes", cli3);

                // C. Registrar Mascotas
                var mas1 = new Mascota { Id = Guid.NewGuid(), ClienteId = cli1.Id, Nombre = "Popper", Especie = "Gato", Raza = "Angora", Edad = 3, Sexo = "Hembra", Peso = 4.2m, Color = "Blanco" };
                var mas2 = new Mascota { Id = Guid.NewGuid(), ClienteId = cli1.Id, Nombre = "Luna", Especie = "Gato", Raza = "Siamés", Edad = 2, Sexo = "Hembra", Peso = 3.8m, Color = "Gris y Marrón" };
                var mas3 = new Mascota { Id = Guid.NewGuid(), ClienteId = cli2.Id, Nombre = "Rocky", Especie = "Perro", Raza = "Golden Retriever", Edad = 5, Sexo = "Macho", Peso = 32.0m, Color = "Dorado" };
                var mas4 = new Mascota { Id = Guid.NewGuid(), ClienteId = cli2.Id, Nombre = "Michi", Especie = "Gato", Raza = "Mestizo", Edad = 1, Sexo = "Macho", Peso = 4.0m, Color = "Atigrado" };
                var mas5 = new Mascota { Id = Guid.NewGuid(), ClienteId = cli3.Id, Nombre = "Toby", Especie = "Perro", Raza = "Poodle", Edad = 4, Sexo = "Macho", Peso = 8.0m, Color = "Blanco" };

                await _http.PostAsJsonAsync("api/Mascotas", mas1);
                await _http.PostAsJsonAsync("api/Mascotas", mas2);
                await _http.PostAsJsonAsync("api/Mascotas", mas3);
                await _http.PostAsJsonAsync("api/Mascotas", mas4);
                await _http.PostAsJsonAsync("api/Mascotas", mas5);

                // D. Registrar Productos
                var prod1 = new Producto { Id = Guid.NewGuid(), Nombre = "Vacuna Triple Felina", Descripcion = "Vacuna felina contra rinotraqueitis, calicivirus y panleucopenia.", PrecioCompra = 200m, PrecioVenta = 450m, Stock = 25 };
                var prod2 = new Producto { Id = Guid.NewGuid(), Nombre = "Vacuna Rabia Canina", Descripcion = "Inmunización contra la rabia para caninos.", PrecioCompra = 150m, PrecioVenta = 350m, Stock = 30 };
                var prod3 = new Producto { Id = Guid.NewGuid(), Nombre = "Alimento Gato Premium 1kg", Descripcion = "Alimento seco premium para gato adulto.", PrecioCompra = 180m, PrecioVenta = 280m, Stock = 15 };
                var prod4 = new Producto { Id = Guid.NewGuid(), Nombre = "Shampoo Antipulgas 500ml", Descripcion = "Shampoo para eliminar pulgas y garrapatas.", PrecioCompra = 120m, PrecioVenta = 220m, Stock = 20 };
                var prod5 = new Producto { Id = Guid.NewGuid(), Nombre = "Consulta Médica General", Descripcion = "Revisión clínica completa.", PrecioCompra = 0m, PrecioVenta = 400m, Stock = 999 };

                await _http.PostAsJsonAsync("api/Productos", prod1);
                await _http.PostAsJsonAsync("api/Productos", prod2);
                await _http.PostAsJsonAsync("api/Productos", prod3);
                await _http.PostAsJsonAsync("api/Productos", prod4);
                await _http.PostAsJsonAsync("api/Productos", prod5);

                // E. Registrar Facturas / Ventas
                // Factura 1
                var fact1 = new Factura { Id = Guid.NewGuid(), ClienteId = cli1.Id, TrabajadorId = trab2.Id, FechaEmision = DateTime.Now.AddDays(-1) };
                fact1.Detalles = new List<DetalleFactura> {
                    new DetalleFactura { Id = Guid.NewGuid(), FacturaId = fact1.Id, ProductoId = prod1.Id, Cantidad = 1, PrecioUnitario = 450m, DescripcionItem = "Vacuna Triple Felina" },
                    new DetalleFactura { Id = Guid.NewGuid(), FacturaId = fact1.Id, ProductoId = prod3.Id, Cantidad = 1, PrecioUnitario = 280m, DescripcionItem = "Alimento Gato Premium 1kg" }
                };
                await _http.PostAsJsonAsync("api/Facturas", fact1);

                // Factura 2
                var fact2 = new Factura { Id = Guid.NewGuid(), ClienteId = cli2.Id, TrabajadorId = trab1.Id, FechaEmision = DateTime.Now };
                fact2.Detalles = new List<DetalleFactura> {
                    new DetalleFactura { Id = Guid.NewGuid(), FacturaId = fact2.Id, ProductoId = prod5.Id, Cantidad = 1, PrecioUnitario = 400m, DescripcionItem = "Consulta Médica General" },
                    new DetalleFactura { Id = Guid.NewGuid(), FacturaId = fact2.Id, ProductoId = prod4.Id, Cantidad = 1, PrecioUnitario = 220m, DescripcionItem = "Shampoo Antipulgas 500ml" }
                };
                await _http.PostAsJsonAsync("api/Facturas", fact2);

                // F. Registrar Citas
                var cita1 = new Cita { Id = Guid.NewGuid(), ClienteId = cli1.Id, MascotaId = mas1.Id, TrabajadorId = trab3.Id, FechaHora = DateTime.Today.AddHours(15), Motivo = "Grooming (Estética/Baño)", Notas = "Cortar uñas y baño de espuma", Estado = "Completada", NotificadoWhatsapp = true, FechaNotificacion = DateTime.Now.AddHours(-1) };
                var cita2 = new Cita { Id = Guid.NewGuid(), ClienteId = cli1.Id, MascotaId = mas2.Id, TrabajadorId = trab2.Id, FechaHora = DateTime.Today.AddDays(1).AddHours(10), Motivo = "Vacunación", Notas = "Toca Triple Felina refuerzo", Estado = "Programada", NotificadoWhatsapp = false };
                var cita3 = new Cita { Id = Guid.NewGuid(), ClienteId = cli2.Id, MascotaId = mas3.Id, TrabajadorId = trab1.Id, FechaHora = DateTime.Today.AddDays(2).AddHours(14), Motivo = "Consulta Médica", Notas = "Revisión de cojera en pata trasera", Estado = "Programada", NotificadoWhatsapp = false };

                await _http.PostAsJsonAsync("api/Citas", cita1);
                await _http.PostAsJsonAsync("api/Citas", cita2);
                await _http.PostAsJsonAsync("api/Citas", cita3);

                // H. Registrar Gastos de demostración
                var g1 = new Gasto { Id = Guid.NewGuid(), Descripcion = "Alquiler de clínica - Mes Actual", Monto = 8000m, Fecha = DateTime.Today.AddDays(-15), Categoria = "Alquiler" };
                var g2 = new Gasto { Id = Guid.NewGuid(), Descripcion = "Recibo de energía eléctrica Enatrel", Monto = 1200m, Fecha = DateTime.Today.AddDays(-10), Categoria = "Servicios Públicos" };
                var g3 = new Gasto { Id = Guid.NewGuid(), Descripcion = "Insumos médicos de limpieza e higiene", Monto = 550m, Fecha = DateTime.Today.AddDays(-5), Categoria = "Suministros" };
                var g4 = new Gasto { Id = Guid.NewGuid(), Descripcion = "Campaña publicitaria en Redes Sociales", Monto = 1500m, Fecha = DateTime.Today.AddDays(-2), Categoria = "Marketing" };

                await _http.PostAsJsonAsync("api/Gastos", g1);
                await _http.PostAsJsonAsync("api/Gastos", g2);
                await _http.PostAsJsonAsync("api/Gastos", g3);
                await _http.PostAsJsonAsync("api/Gastos", g4);

                // G. Refrescar listas
                await LoadClientesAsync();
                await LoadTrabajadoresAsync();
                await LoadProductosAsync();
                await LoadCitasAsync();
                await LoadGastosAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
                return false;
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

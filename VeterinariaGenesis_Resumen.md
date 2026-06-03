# Veterinaria Génesis - Resumen de Proyecto y Estado Actual

## 📌 Información General
- **Tecnología:** Blazor WebAssembly (.NET 10) + MongoDB.
- **Hosting:** 
  - Frontend: GitHub Pages (`https://gabriellezama.github.io/VeterinariaGenesis1/`)
  - Backend: Railway.
- **Ruta del Logo:** `wwwroot/images/logo.png`

## 🏥 Datos de la Clínica
- **Nombre:** Veterinaria Génesis.
- **Dirección:** Semaforos de la cañada 10 vara al este.
- **Teléfono:** 8878-2492.

## 🛠️ Funcionalidades Implementadas (CRUD Completo)
1. **Clientes:** Registro, edición y eliminación.
2. **Mascotas:** Vinculación con clientes, historial médico individual.
3. **Historial Clínico:**
   - Registro de Eventos Médicos (Consultas, Vacunas, Cirugías, etc.).
   - Edición y Eliminación de eventos.
   - Generación de **Recetas** y **Expedientes** en PDF con logo y datos actuales.
4. **Facturación (Punto de Venta):**
   - Selección de cliente y trabajador.
   - Cálculo automático de Subtotal, IVA (15%) y Total.
   - Descuento de stock en tiempo real.
   - Impresión de facturas profesionales con logo y dirección.
5. **Inventario:** Gestión de productos y stock.
6. **Contabilidad:** Reporte de utilidades y balance general (preparado para impresión).

## 🔧 Detalles Técnicos de Serialización (MongoDB)
- Todas las entidades que usan `Guid` deben tener el atributo: `[BsonRepresentation(MongoDB.Bson.BsonType.String)]`.
- Las propiedades calculadas (Subtotal, Impuestos, Total) en `Factura` y `DetalleFactura` deben estar marcadas con `[BsonIgnore]`.
- La propiedad `FechaVieja` en `Factura` debe tener un `get => null;` para evitar errores de acceso en MongoDB.

## 🚀 Cómo continuar
Cuando quieras añadir algo nuevo, solo pídelo. El sistema está preparado para escalar en:
- Recordatorios de vacunas por WhatsApp.
- Reportes de ventas por rango de fechas.
- Gestión de citas y calendario.

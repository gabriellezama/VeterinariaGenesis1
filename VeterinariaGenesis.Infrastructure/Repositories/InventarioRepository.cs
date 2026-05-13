using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class ProveedorRepository
    {
        private readonly IMongoCollection<Proveedor> _collection;

        public ProveedorRepository(MongoDbContext context)
        {
            _collection = context.Proveedores;
        }

        public async Task<List<Proveedor>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Proveedor proveedor)
            => await _collection.InsertOneAsync(proveedor);

        public async Task UpdateAsync(Guid id, Proveedor proveedor)
            => await _collection.ReplaceOneAsync(p => p.Id == id, proveedor);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(p => p.Id == id);
    }

    public class ProductoRepository
    {
        private readonly IMongoCollection<Producto> _collection;

        public ProductoRepository(MongoDbContext context)
        {
            _collection = context.Productos;
        }

        public async Task<List<Producto>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task<Producto> GetByIdAsync(Guid id)
            => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Producto producto)
            => await _collection.InsertOneAsync(producto);

        public async Task UpdateAsync(Guid id, Producto producto)
            => await _collection.ReplaceOneAsync(p => p.Id == id, producto);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(p => p.Id == id);

        public async Task UpdateStockAsync(Guid id, decimal newStock)
        {
            var update = Builders<Producto>.Update.Set(p => p.Stock, newStock);
            await _collection.UpdateOneAsync(p => p.Id == id, update);
        }
    }

    public class FacturaRepository
    {
        private readonly IMongoCollection<Factura> _collection;

        public FacturaRepository(MongoDbContext context)
        {
            _collection = context.Facturas;
        }

        public async Task<List<Factura>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Factura factura)
            => await _collection.InsertOneAsync(factura);
    }
}

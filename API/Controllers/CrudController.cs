using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGSPCSI.API.Repositories;

namespace SGSPCSI.API.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class CrudController<TEntity, TKey> : ControllerBase
        where TEntity : class
        where TKey : notnull
    {
        protected readonly IRepository<TEntity> Repository;

        protected CrudController(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            return Ok(await Repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(TKey id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity)
        {
            await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();

            var id = GetEntityId(entity);
            return CreatedAtAction(nameof(GetById), new { id }, entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(TKey id, [FromBody] TEntity entity)
        {
            SetEntityId(entity, id);

            var existing = await Repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            Repository.Update(entity);
            await Repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            Repository.Remove(entity);
            await Repository.SaveChangesAsync();
            return NoContent();
        }

        private static object? GetEntityId(TEntity entity)
        {
            var type = typeof(TEntity);
            var expectedName = $"{type.Name}Id";
            var property = type.GetProperty(expectedName, BindingFlags.Instance | BindingFlags.Public)
                ?? type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.Ordinal));

            return property?.GetValue(entity);
        }

        private static void SetEntityId(TEntity entity, TKey id)
        {
            var type = typeof(TEntity);
            var expectedName = $"{type.Name}Id";
            var property = type.GetProperty(expectedName, BindingFlags.Instance | BindingFlags.Public)
                ?? type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.Ordinal));

            if (property == null || !property.CanWrite)
                return;

            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            var convertedValue = Convert.ChangeType(id, targetType);
            property.SetValue(entity, convertedValue);
        }
    }
}
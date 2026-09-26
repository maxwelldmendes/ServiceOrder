using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Data;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Repositories
{
    public class SystemUserRepository : IRepository<SystemUser>
    {
        private readonly AppDbContext _context;
        public SystemUserRepository(AppDbContext context)
        {
            _context = context;
        }

        //Adiciona Usuario ao banco de dados
        public async Task AddAsync(SystemUser entity)
        {
            try
            {
                await _context.SystemUser.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Relança a exceção original
            }
        }

        // Funcao para deletar usuario pelo Id numerico
        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        // Funcao para deletar o usuario pelo Id string
        public async Task DeleteAsync(string id)
        {
            var systemUser = await _context.SystemUser.FindAsync(id);

            if (systemUser == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _context.SystemUser.Remove(systemUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //Funcao par retornar lista de usuarios
        public async Task<IEnumerable<SystemUser>> GetAllAsync()
        {
            return await _context.SystemUser.ToListAsync();
        }

        //Funcao para retornar usario baseado no Id numerico
        public async Task<SystemUser> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        //Funcao para retornar o usuario baseado no Id string
        public async Task<SystemUser> GetAsync(string id)
        {
            try
            {
                var systemUser = await _context.SystemUser.FindAsync(id);

                if (systemUser == null)
                {
                    throw new KeyNotFoundException();
                }

                return systemUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task UpdateAsync(SystemUser entity)
        {
            try
            {
                _context.SystemUser.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

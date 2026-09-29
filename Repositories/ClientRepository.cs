using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Data;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        // Return All Clients
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Client
                        .Include(c => c.CompanyAddress)
                        .Include(c => c.MailAddress)
                        .ToListAsync();
        }

        // Add Client to database
        public async Task AddAsync(Client entity)
        {
            try
            {
                await _context.Client.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Relança a exceção original
            }
        }

        // Delete Client from database
        public async Task DeleteAsync(int id)
        {
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Delete Client from database
        public async Task DeleteAsync(string id)
        {
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Return one Client by Id
        public async Task<Client> GetAsync(int id)
        {
            try
            {
                var client = await _context.Client
                                .Include(c => c.CompanyAddress)
                                .Include(c => c.MailAddress)
                                .FirstOrDefaultAsync(c => c.Id == id);

                if (client == null)
                {
                    throw new KeyNotFoundException();
                }

                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Client> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Client entity)
        {
            try
            {
                _context.Client.Update(entity);
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

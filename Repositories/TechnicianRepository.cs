using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Data;
using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;
using ServiceOrderManager.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceOrderManager.Repositories
{
    public class TechnicianRepository : IRepository<Technician>, ITechnicianRepository
    {
        private readonly AppDbContext _context;
        public TechnicianRepository(AppDbContext context)
        {
            _context = context;
        }

        // Return All Technician
        public async Task<IEnumerable<Technician>> GetAllAsync()
        {              
            
            // Busca na tabela de Técnicos, incluindo os dados do Usuário do Sistema
            return await _context.Technician
                .Include(t => t.User)
                .Where(t => t.User != null && t.User.UserRole == "Technician")
                .ToListAsync();
        }

        // Add Technician to database
        public async Task AddAsync(Technician entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                await _context.Technician.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Relança a exceção original
            }
        }

        // Delete Technician from database
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Technician.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _context.Technician.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Delete Technician from database
        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Technician.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _context.Technician.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Return one Technician by Id
        public async Task<Technician> GetAsync(int id)
        {
            try
            {
                var entity = await _context.Technician
                        .Include(t => t.User)
                        .Where(t => t.User != null && t.User.UserRole == "Technician")
                        .FirstOrDefaultAsync();

                if (entity == null)
                {
                    throw new KeyNotFoundException();
                }

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task UpdateAsync(Technician entity)
        {
            try
            {
                _context.Technician.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<SelectListItem>> GetAvailableUsersAsync()
        {
            var model = await _context.SystemUser
                .Where(u => u.Enabled && u.UserRole == "Technician" && u.Technician == null)
                .OrderBy(u => u.FirstName)
                .Select(u => new SelectListItem()
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();

            return model;
        }

        // Retorna os detalhes do usuário para criar um perfil de técnico,
        // garantindo que o usuário exista e não tenha um perfil de técnico duplicado
        public async Task<TechnicianViewModel> GetAsync(string id)
        {
            // Faz apenas UMA consulta trazendo os dados do usuário e verificando se o técnico já existe
            var userDetails = await _context.SystemUser
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    User = u,
                    HasTechnician = u.Technician != null // Usa a propriedade de navegação inversa
                })
                .FirstOrDefaultAsync();

            // Regra 1: Se o usuário não existir no banco
            if (userDetails == null)
            {
                throw new KeyNotFoundException($"Usuário com o ID {id} não foi encontrado.");
            }

            // Regra 2: Se o usuário já tiver um perfil de técnico criado, barra a duplicação
            if (userDetails.HasTechnician)
            {
                throw new InvalidOperationException($"O usuário {userDetails.User.UserName} já possui um perfil de técnico cadastrado.");
            }

            // Retorna o ViewModel preenchido com segurança
            return new TechnicianViewModel
            {
                UserId = userDetails.User.Id,
                UserName = userDetails.User.UserName,
                UserEmail = userDetails.User.Email,
                FirstName = userDetails.User.FirstName,
                Middlename = userDetails.User.MiddleName,
                LastName = userDetails.User.LastName,
                PhoneNumber = userDetails.User.PhoneNumber,
                UserRole = userDetails.User.UserRole
            };
        }


        /*
        public async Task<TechnicianViewModel> GetAsync(string id)
        {
            try
            {
                var userTechnician = await _context.SystemUser
                                        .Where(u => u.Id == id)
                                        .FirstOrDefaultAsync();

                if (userTechnician == null) 
                {
                    throw new KeyNotFoundException();
                }

                var entity = await _context.Technician
                             .FirstOrDefaultAsync(t => t.UserId == id);

                if (entity != null)
                {
                    throw new Exception();
                }

                var technicanViewModel = new TechnicianViewModel {
                    UserId = userTechnician.Id,
                    UserName = userTechnician.UserName,
                    UserEmail = userTechnician.Email,
                    FirstName = userTechnician.FirstName,
                    Middlename = userTechnician.MiddleName,
                    LastName = userTechnician.LastName,
                    PhoneNumber = userTechnician.PhoneNumber,
                    UserRole = userTechnician.UserRole
                };

                return technicanViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        */

        Task<Technician> IRepository<Technician>.GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
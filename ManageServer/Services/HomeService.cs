using ManageServer.Entities;
using ManageServer.IServices;
using ManageServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageServer.Services
{
    public class HomeService : IHomeService
    {
        private readonly ManageContext _context;

        public HomeService(ManageContext context)
        {
            _context = context;
        }

        public async Task<Home> AddHomeAsync(HomeModel home)
        {
            var newHome = new Home
            {
                Title = home.Titlte,
                Description = home.Description,
                LinkImage = home.LinkImage,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _context.AddAsync(newHome);
            await _context.SaveChangesAsync();

            return newHome;
        }

        public async Task DeleteHomeAsync(Guid id)
        {
            var data = await _context.Homes.FindAsync(id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Home>> GetAllHomeAsync()
        {
            var homeList = await _context.Homes.ToListAsync();
            return homeList;
        }

        public async Task<Home> GetHomeById(Guid id)
        {
            var data = await _context.Homes.FindAsync(id);
            return data;
        }

        public async Task<Home> UpdateHomeAsync(HomeModel home)
        {
            var data = await _context.Homes.FindAsync(home.Id);
            if (data != null)
            {
                data.Description = home.Description;
                data.Title = home.Titlte;
                data.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return data;
        }
    }
}

﻿using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Infrastructure.Repositories.UsersDashboard;
using E_CommerceProject.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories.UserDashboardRepository
{
    public class UserDashboardRepository : RepositoryAsync<ApplicationUser, string>, IUserDashboardRepository
    {
        private readonly ECommerceContext _dbContext;

        public UserDashboardRepository(ECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<ApplicationUser?> GetByIdAsync(string key)
        {
            return await _dbContext.Set<ApplicationUser>()
                .Include(c => c.Orders)
                    .ThenInclude(c => c.OrdersDetails)
                        .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == key);
        }

        public override async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _dbContext.Set<ApplicationUser>()
                .Include(c => c.Orders)
                     .ThenInclude(c => c.OrdersDetails)
                        .ThenInclude(c => c.Product)
                .ToListAsync();
        }

        public async Task<(List<ApplicationUser> items, int totalItemsCount)> Get(int pageIndex, int pageSize)
        {
            var users = _dbContext.Set<ApplicationUser>()
                .Include(c => c.Orders)
                     .ThenInclude(c => c.OrdersDetails)
                        .ThenInclude(c => c.Product)
                .AsQueryable();

            var totalItems = await users.CountAsync();
            var items = await users.OrderBy(c => c.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
    }
}


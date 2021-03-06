﻿using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly DrugInteractionsDbContext db;

        public AdminUserService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
        {
            return await this.db
               .Users
               .ProjectTo<AdminUserListingServiceModel>()
               .ToListAsync();
        }
    }
}

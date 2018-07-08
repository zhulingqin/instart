﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Instart.Models.Enums;

namespace Instart.Repository
{
    public interface IDivisionRepository
    {
        Task<Division> GetByIdAsync(int id);

        Task<PageModel<Division>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<IEnumerable<Division>> GetAllAsync();

        Task<bool> InsertAsync(Division model);

        Task<bool> UpdateAsync(Division model);

        Task<bool> DeleteAsync(int id);
    }
}

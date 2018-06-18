﻿using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(int id);

        Task<List<Category>> GetByParentIdAsync(int parentId);

        Task<PageModel<Category>> GetListAsync(int pageIndex, int pageSize, string name = null);

        Task<bool> InsertAsync(Category model);

        Task<bool> UpdateAsync(Category model);

        Task<bool> DeleteAsync(int id);
    }
}
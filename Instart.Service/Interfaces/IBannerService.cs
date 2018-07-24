﻿using Instart.Models;
using Instart.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface IBannerService
    {
        Task<Banner> GetByIdAsync(int id);

        Task<List<Banner>> GetListByPosAsync(EnumBannerPos pos = EnumBannerPos.Index);

        Task<PageModel<Banner>> GetListAsync(int pageIndex, int pageSize, string title = null, int pos = 1, int type = -1);

        Task<bool> InsertAsync(Banner model);

        Task<bool> UpdateAsync(Banner model);

        Task<bool> DeleteAsync(int id);

        Task<List<Banner>> GetBannerListByPosAsync(EnumBannerPos pos, int topCount = 20);

        Task<bool> SetShowAsync(int id, bool isShow);
    }
}

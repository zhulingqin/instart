﻿using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Repository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetRecommendListAsync(int topCount);
    }
}
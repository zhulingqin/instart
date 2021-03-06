﻿using Instart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instart.Service
{
    public interface ICourseService
    {
        Course GetByIdAsync(int id);

        PageModel<Course> GetListAsync(int pageIndex, int pageSize, int type = -1, string name = null);

        IEnumerable<Course> GetAllAsync();

        IEnumerable<Course> GetAllByStudentAsync(int studentId);

        bool InsertAsync(Course model);

        bool UpdateAsync(Course model);

        bool DeleteAsync(int id);

        List<Course> GetRecommendListAsync(int topCount);

        bool SetRecommendAsync(int id, bool isRecommend);

        int GetInfoCountAsync();

        CourseInfo GetInfoAsync();

        bool InsertInfoAsync(CourseInfo model);

        bool UpdateInfoAsync(CourseInfo model);

        IEnumerable<int> GetTeachersByIdAsync(int id);

        bool SetTeachers(int courseId, string teacherIds);
    }
}

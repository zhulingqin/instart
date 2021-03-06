﻿using Instart.Common;
using Instart.Models;
using Instart.Service;
using Instart.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        IPartnerService _partnerService = AutofacService.Resolve<IPartnerService>();
        ISchoolService _schoolService = AutofacService.Resolve<ISchoolService>();
        ITeacherService _teacherService = AutofacService.Resolve<ITeacherService>();
        IStudentService _studentService = AutofacService.Resolve<IStudentService>();
        ICourseService _courseService = AutofacService.Resolve<ICourseService>();
        IBannerService _bannerService = AutofacService.Resolve<IBannerService>();
        IRecruitService _recruitService = AutofacService.Resolve<IRecruitService>();

        public HomeController() {
            this.AddDisposableObject(_partnerService);
            this.AddDisposableObject(_schoolService);
            this.AddDisposableObject(_teacherService);
            this.AddDisposableObject(_studentService);
            this.AddDisposableObject(_courseService);
            this.AddDisposableObject(_bannerService);
            this.AddDisposableObject(_recruitService);
        }

        public  ActionResult Index() {
            ViewBag.PartnerList = ( _partnerService.GetRecommendListAsync(14)) ?? new List<Instart.Models.Partner>();
            ViewBag.SchoolList = ( _schoolService.GetRecommendListAsync(10)) ?? new List<Instart.Models.School>();
            ViewBag.TeacherList = ( _teacherService.GetRecommendListAsync(8)) ?? new List<Instart.Models.Teacher>();
            ViewBag.StudentList = ( _studentService.GetRecommendListAsync(8)) ?? new List<Instart.Models.Student>();
            ViewBag.CourseList = ( _courseService.GetRecommendListAsync(3)) ?? new List<Instart.Models.Course>();
            ViewBag.BannerList = ( _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Index)) ?? new List<Instart.Models.Banner>();
            return View();
        }

        /// <summary>
        /// 招贤纳士
        /// </summary>
        /// <returns></returns>
        public  ActionResult Recruit() {
            Recruit model =  _recruitService.GetInfoAsync() ?? new Recruit();

            List<Banner> bannerList =  _bannerService.GetBannerListByPosAsync(Instart.Models.Enums.EnumBannerPos.Recruit);
            ViewBag.BannerUrl = "";
            if (bannerList != null && bannerList.Count() > 0)
            {
                ViewBag.BannerUrl = bannerList[0].ImageUrl;
            }
            return View(model);
        }
    }
}
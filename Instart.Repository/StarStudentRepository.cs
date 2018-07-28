﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Models;
using Dapper;

namespace Instart.Repository
{
    public class StarStudentRepository : IStarStudentRepository
    {
        public StarStudent GetByIdAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "select * from [StarStudent] where Id = @Id and Status=1;";
                return conn.QueryFirstOrDefault<StarStudent>(sql, new { Id = id });
            }
        }

        public PageModel<StarStudent> GetListAsync(int pageIndex, int pageSize, string name = null) {
            using (var conn = DapperFactory.GetConnection()) {
                #region generate condition
                string where = "where a.Status=1";
                if (!string.IsNullOrEmpty(name)) {
                    where += $" and a.Name like '%{name}%'";
                }
                #endregion

                string countSql = $"select count(1) from [StarStudent] as a {where};";
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0) {
                    return new PageModel<StarStudent>();
                }

                string sql = $@"select * from (
                     select a.*, ROW_NUMBER() over (Order by a.Id desc) as RowNumber from [StarStudent] as a {where}
                     ) as c
                     where RowNumber between {((pageIndex - 1) * pageSize) + 1} and {pageIndex * pageSize};";
                var list = conn.Query<StarStudent>(sql);

                return new PageModel<StarStudent> {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public IEnumerable<StarStudent> GetAllAsync()
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where Status=1";
                #endregion

                string sql = $@"select * from [StarStudent] {where};";
                return conn.Query<StarStudent>(sql);
            }
        }

        public bool InsertAsync(StarStudent model) {
            using (var conn = DapperFactory.GetConnection()) {
                var fields = model.ToFields(removeFields: new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0) {
                    return false;
                }

                model.CreateTime = DateTime.Now;
                model.ModifyTime = DateTime.Now;
                model.Status = 1;

                string sql = $"insert into [StarStudent] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAsync(StarStudent model) {
            using (var conn = DapperFactory.GetConnection()) {
                List<string> removeFields = new List<string>
                {
                    nameof(model.Id),
                    nameof(model.CreateTime),
                    nameof(model.Status)
                };
                var fields = model.ToFields(removeFields: removeFields);

                if (fields == null || fields.Count == 0) {
                    return false;
                }

                var fieldList = new List<string>();
                foreach (var field in fields) {
                    fieldList.Add($"{field}=@{field}");
                }

                model.ModifyTime = DateTime.Now;

                string sql = $"update [StarStudent] set {string.Join(",", fieldList)} where Id=@Id;";
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAsync(int id) {
            using (var conn = DapperFactory.GetConnection()) {
                string sql = "update [StarStudent] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }
    }
}

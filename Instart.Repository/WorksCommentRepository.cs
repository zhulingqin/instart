﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Instart.Models;
using Instart.Models.Enums;

namespace Instart.Repository
{
    public class WorksCommentRepository : IWorksCommentRepository
    {
        public PageModel<WorksComment> GetListAsync(int pageIndex, int pageSize, string name, EnumAccept accept)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                #region generate condition
                string where = "where 1=1 ";
                if (!string.IsNullOrEmpty(name))
                {
                    where += string.Format(" and h.Name like '%{0}%'", name);
                }
                if(accept != EnumAccept.All)
                {
                    where += string.Format(" and h.IsAccept = {0}",(int)accept);
                }
                #endregion

                string countSql = string.Format("select count(1) from [WorksComment] as h {0};", where);
                int total = conn.ExecuteScalar<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<WorksComment>();
                }

                string sql = string.Format(@"select * from ( select h.*, m.Name as MajorName, m.NameEn as MajorNameEn, ROW_NUMBER() over (Order by h.Id desc) as RowNumber from [WorksComment] as h 
                    left join [Major] m on m.Id = h.MajorId {0} ) as b
                    where RowNumber between {1} and {2};", where, ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize);
                var list = conn.Query<WorksComment>(sql);

                return new PageModel<WorksComment>
                {
                    Total = total,
                    Data = list != null ? list.ToList() : null
                };
            }
        }

        public bool InsertAsync(WorksComment model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(removeFields: new List<string> { "Id","MajorName","MajorNameEn" });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                model.CreateTime = DateTime.Now;

                string sql = string.Format("insert into [WorksComment] ({0}) values ({1});", string.Join(",", fields), string.Join(",", fields.Select(n => "@" + n)));
                return conn.Execute(sql, model) > 0;
            }
        }

        public bool SetAcceptAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [WorksComment] set IsAccept=1,AcceptTime=GETDATE() where Id=@Id;";
                return conn.Execute(sql, new { Id = id }) > 0;
            }
        }

        public List<WorksComment> GetTopListAsync(int topCount)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = string.Format("select top {0} * from WorksComment order by Id Desc;", topCount);
                var list = conn.Query<WorksComment>(sql, null);
                return list != null ? list.ToList() : null;
            }
        }
    }
}

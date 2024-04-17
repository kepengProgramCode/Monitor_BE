﻿using Monitor_BE.Entity;
using Monitor_BE.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor_BE.Common.Response;
using Monitor_BE.ServerBusiness;
using System.Linq.Expressions;
using SqlSugar;
using NewLife;

namespace Monitor_BE.ServiceBuiness
{
    /// <summary>
    /// Business Function
    /// </summary>
    public class UserService : AccessClient<tb_user>
    {
        public List<tb_user> GetUserList(tb_user user)
        {
            var source = GetGetAllUsers().Result;//查询所有
            return source.WhereIF(!user.u_name.IsNullOrEmpty(), o => o.u_name == user.u_name).WhereIF(!user.u_pwd.IsNullOrEmpty(), p => p.u_pwd == user.u_pwd).ToList();
        }

        public ResUserLists GetUserList(GetUserPar userPar)
        {
            var res = GetGetAllUsers().Result;
            ResUserLists list = new();
            list.list = res;
            list.pageNum = userPar.pageNum;
            list.pageSize = userPar.pageSize;
            return list;
        }

        private async Task<List<tb_user>> GetGetAllUsers() => await Db.Queryable<tb_user>().ToListAsync();


        public int UpdateUserToken(tb_token token, LogService log)
        {
            int res = 0;
            try
            {
                Db.Ado.BeginTran();
                if ((Db.Updateable(token).ExecuteCommand() <= 0))
                {
                    Db.Insertable(token).ExecuteCommand();
                    log.Info($"注册新的用户{token.u_id}Token为{token}");
                }
                else
                {
                    log.Info($"更新用户{token.u_id}Token为{token.token_str}");
                }

                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                log.Error("更新Token错误", ex);
            }
            return res;
        }

        public int RegesiterUser(tb_user user, LogService log)
        {
            int res = 0;
            try
            {
                user?.userdpt.ToUpper();
                user?.userdpt.ToUpper();
                Db.Ado.BeginTran();
                res = Db.Insertable(user).ExecuteCommand() > 0 ? 0 : 1;
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                log.Error("注册用户错误", ex);
            }
            return res;
        }


        public int UpdateUser(tb_user user, LogService log)
        {
            int res = 0;
            try
            {
                user?.userdpt.ToUpper();
                user?.userdpt.ToUpper();
                Db.Ado.BeginTran();
                res = Db.Updateable(user).ExecuteCommand();
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                log.Error("注册用户错误", ex);
            }
            return res;
        }


        public int DeleteUser(tb_user user, LogService log)
        {
            int res = 0;
            try
            {
                Db.Ado.BeginTran();
                res = Db.Deleteable(user).ExecuteCommand();
                Db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                log.Error("注册删除错误", ex);
            }
            return res;
        }

    }
}

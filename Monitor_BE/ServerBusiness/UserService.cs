using Monitor_BE.Entity;
using Monitor_BE.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor_BE.Common.Response;
using Monitor_BE.ServerBusiness;

namespace Monitor_BE.ServiceBuiness
{
    /// <summary>
    /// Business Function
    /// </summary>
    public class UserService : AccessClient<tb_user>
    {
        public List<tb_user> GetUserList(tb_user user)
        {
            var source = Db.Queryable<tb_user>().ToList();//查询所有
            if (user.u_id == 0) return source;
            if (!string.IsNullOrEmpty(user.u_name))
            {
                if (user.u_name.Contains('*')) source = source.Where(a => a.u_name.Contains(user.u_name.Replace("*", ""))).ToList();
                else source = source.Where(a => a.u_name.Equals(user.u_name)).ToList();
            }
            if (!string.IsNullOrEmpty(user.userdpt))
            {
                if (user.userdpt.Contains('*')) source = source.Where(a => a.userdpt.Contains(user.userdpt.ToUpper().Replace("*", ""))).ToList();
                else source = source.Where(a => a.userdpt.Equals(user.userdpt.ToUpper())).ToList();
            }
            return source;
            //Condition condition = new Condition(CompareOper.equal, "string", "name", "%kkp%");
            //Condition condition2 = new Condition(CompareOper.equal, "int", "id", 1024);
            //Condition condition3 = new Condition(CompareOper.like, "string", "nickName", "%’kkp’%");
            //Condition condition4 = new Condition(CompareOper.equal, "date", "age", DateTime.Now);
            //Condition condition5 = new Condition(CompareOper.equal, "datetime", "signTime", DateTime.Now);
            //Condition condition6 = new Condition();
        }

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
                user?.region.ToUpper();
                user?.userdpt.ToUpper();
                Db.Ado.BeginTran();
                res = Db.Insertable(user).ExecuteCommand();
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
                user?.region.ToUpper();
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

using Monitor_BE.Entity;
using Monitor_BE.Repository;

namespace Monitor_BE.ServerBusiness
{
    public class RoleService : AccessClient<tb_role>
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="type">
        /// 1：注册 ，排除掉超级管理员角色</param>
        /// <returns></returns>
        public  List<tb_role> findRole(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "1")
                {
                    return Db.Ado.SqlQuery<tb_role>($" select * from  tb_role where id !='1' ");
                }
                else
                {
                    return Db.Ado.SqlQuery<tb_role>($" select * from  tb_role ");
                }
            }
            else
            {
                return Db.Ado.SqlQuery<tb_role>($" select * from  tb_role ");
            }

        }

    }
}

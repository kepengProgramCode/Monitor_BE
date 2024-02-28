using Monitor_BE.Entity;
using Monitor_BE.Repository;
using NewLife.Reflection;
using Newtonsoft.Json;

namespace Monitor_BE.ServerBusiness
{
    public class MenuService : AccessClient<tb_menu>
    {
        //public List<tb_menu> GetFindMenu(string u_name)
        //{
        //    return Db.Ado.SqlQuery<tb_menu>($"SELECT a.* FROM tb_user AS b INNER JOIN tb_user_role AS c ON b.u_id = c.user_id INNER JOIN tb_role_menu AS e ON c.role_id = e.role_id INNER JOIN tb_menu AS a ON e.menu_id = a.id WHERE b.u_name = '{u_name}'");
        //}

        /// <summary>
        /// 根据用户名和，菜单类型递归获取菜单
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="menuType">菜单类型</param>
        /// <returns>递归后的菜单</returns>
        public List<tb_role_permissions> findPermission(string u_id)
        {
            //var g = Db.Queryable<tb_role_permissions>().LeftJoin<tb_role>((p, r) => p.role_id == r.id).LeftJoin<tb_user_role>((p, r, ur) => r.id == ur.role_id).LeftJoin<tb_user>((p, r, ur, u) => ur.id == u.u_id && u.u_name == u_id);
            return findButtonByUser(u_id);
        }

        /// <summary>
        /// 根据用户名称获取菜单
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private List<tb_menu> findByUser(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return Db.Queryable<tb_menu>().ToList();
                }
                return Db.Ado.SqlQuery<tb_menu>($"SELECT m.*\r\nFROM tb_menu AS m\r\nJOIN tb_role_menu AS rm ON rm.menu_id = m.id\r\nJOIN tb_user_role AS ur ON ur.role_id = rm.role_id\r\nJOIN tb_user AS u ON u.u_id = ur.user_id\r\nWHERE u.u_name = '{userName}';");
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }



        /// <summary>
        /// 根据用户名称获取菜单
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private List<tb_role_permissions> findButtonByUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return Db.Queryable<tb_role_permissions>().ToList();
            }
            return Db.Ado.SqlQuery<tb_role_permissions>($"select p.* from tb_role_permissions p, tb_user u, tb_user_role ur where u.u_name = '{userName}' and u.u_id = ur.user_id and ur.role_id = p.role_id");
        }


        private void findChildren(List<tb_menu> SysMenus, List<tb_menu> menus, int menuType)
        {
            foreach (tb_menu SysMenu in SysMenus)
            {
                List<tb_menu> children = new();
                foreach (tb_menu menu in menus)
                {
                    if (menuType == 1)
                    {
                        // 如果是获取类型不需要按钮，且菜单类型是按钮的，直接过滤掉
                        continue;
                    }
                    if (SysMenu?.id == menu.parent_id)
                    {
                        //menu.parentName = SysMenu.name;
                        //menu.level = SysMenu.level + 1;
                        if (!Exists(children, menu))
                        {
                            children.Add(menu);
                        }
                    }
                }
                //SysMenu.children = children;
                //children.Sort();
                //children.sort((o1, o2)->o1.getOrderNum().compareTo(o2.getOrderNum()));
                findChildren(children, menus, menuType);
            }
        }


        private bool Exists(List<tb_menu> sysMenus, tb_menu sysMenu)
        {
            bool exist = false;
            foreach (tb_menu menu in sysMenus)
            {
                if (menu.id.Equals(sysMenu.id))
                {
                    exist = true;
                }
            }
            return exist;
        }



        /// <summary>
        /// 得到树形菜单
        /// </summary>
        /// <param name="u_id">用户Id</param>
        /// <returns></returns>
        public async Task<List<tb_menu>> GetRouteHierarchyAsync(string u_id)
        {
            //通过u_Id查询到对应用户的相关菜单id,
            object[] obj = findByUser(u_id).Select(o => o.id).Cast<object>().ToArray();
            //单一的树形查询
            // return await Db.Queryable<tb_menu>().ToTreeAsync(it => it.children, it => it.parent_id, null);
            //通过构造子表查询菜单
            return await Db.Queryable<tb_menu>().Includes(x => x.meta).ToTreeAsync(it => it.children, it => it.parent_id, null, obj);
        }
    }
}

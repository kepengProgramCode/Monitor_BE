using Monitor_BE.Entity;
using Monitor_BE.Repository;

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
        public List<tb_menu> findTree(string userName, int menuType)
        {
            List<tb_menu> sysMenus = new();
            List<tb_menu> menus = findByUser(userName);
            foreach (tb_menu menu in menus)
            {
                if (menu.parent_id is null or 0)
                {
                    //menu.level = 0;
                    if (!Exists(sysMenus, menu))
                    {
                        sysMenus.Add(menu);
                    }
                }
            }
            //sysMenus.Sort();
            //sysMenus.Sort((o1, o2)->o1.getOrderNum().compareTo(o2.getOrderNum()));
            findChildren(sysMenus, menus, menuType);
            return sysMenus;
        }

        /// <summary>
        /// 根据用户名称获取菜单
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<tb_menu> findByUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return Db.Queryable<tb_menu>().ToList();
            }
            return Db.Ado.SqlQuery<tb_menu>($"select m.* from tb_menu m, tb_user u, tb_user_role ur, tb_role_menu rm where u.u_name = '{userName}' and u.u_id = ur.user_id and ur.role_id = rm.role_id and rm.menu_id = m.id");
        }


        private void findChildren(List<tb_menu> SysMenus, List<tb_menu> menus, int menuType)
        {
            foreach (tb_menu SysMenu in SysMenus)
            {
                List<tb_menu> children = new();
                foreach (tb_menu menu in menus)
                {
                    if (menuType == 1 && menu.type == 2)
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
    }
}

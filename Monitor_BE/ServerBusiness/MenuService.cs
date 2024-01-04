using Monitor_BE.Entity;
using Monitor_BE.Repository;
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
        public List<tb_role_permissions> findPermission()
        {
            return Db.Queryable<tb_role_permissions>().ToList(); 
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


        public async Task<List<tb_menu>> GetRouteHierarchyAsync()
        {
            // return await Db.Queryable<tb_menu>().ToTreeAsync(it => it.children, it => it.parent_id, null);
            return await Db.Queryable<tb_menu>().Includes(x=>x.meta).ToTreeAsync(it => it.children, it => it.parent_id, null);
            // 获取所有路由，包括子路由
            //var allRoutes = await Db.Queryable<tb_routes>().ToListAsync();

            // 使用字典来优化查找
            //var routeDictionary = allRoutes.ToDictionary(r => r.id);

            // 构建树状结构
            //foreach (var route in allRoutes)
            //{
            //    if (routeDictionary[route.id].meta == null) routeDictionary[route.id].meta = new()
            //    {
            //        title = routeDictionary[route.id].meta_title,
            //        role = routeDictionary[route.id].meta_role?.Split(","),
            //        order = routeDictionary[route.id].meta_order,
            //        icon = routeDictionary[route.id].meta_icon,
            //        activeMenu = routeDictionary[route.id].meta_activeMenu,
            //        alwaysShow = routeDictionary[route.id].meta_alwaysShow,
            //        hideMenu = routeDictionary[route.id].meta_hideMenu,
            //    };
            //    if (route.parent_id.HasValue && routeDictionary.ContainsKey(route.parent_id.Value))
            //    {
            //        if (routeDictionary[route.parent_id.Value].children == null) routeDictionary[route.parent_id.Value].children = new();
            //        routeDictionary[route.parent_id.Value].children.Add(route);
            //    }
            //}

            // 返回顶级路由，即没有父路由的路由
            //var topLevelRoutes = allRoutes.Where(r => !r.parent_id.HasValue).ToList();
            //return topLevelRoutes;
        }
    }
}

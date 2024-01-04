using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///存储路由及其子路由的表
    ///</summary>
    [SugarTable("tb_routes")]
    public partial class tb_routes
    {
        public tb_routes()
        {


        }
        /// <summary>
        /// Desc:主键ID，自动递增
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// Desc:父路由ID，NULL表示顶级路由
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? parent_id { get; set; }

        /// <summary>
        /// Desc:路由路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string path { get; set; }

        /// <summary>
        /// Desc:路由名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? name { get; set; }

        /// <summary>
        /// Desc:Vue组件路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string component { get; set; }

        /// <summary>
        /// Desc:路由重定向路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? redirect { get; set; }

        /// <summary>
        /// Desc:路由元信息标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? meta_title { get; set; }

        /// <summary>
        /// Desc:路由元信息图标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? meta_icon { get; set; }

        /// <summary>
        /// Desc:路由是否总是显示，1为是，0为否
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public bool? meta_alwaysShow { get; set; }

        /// <summary>
        /// Desc:路由显示顺序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? meta_order { get; set; }

        /// <summary>
        /// Desc:路由角色权限，JSON格式字符串
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? meta_role { get; set; }

        /// <summary>
        /// Desc:关联的活动菜单路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? meta_activeMenu { get; set; }

        /// <summary>
        /// Desc:菜单是否隐藏，1为是，0为否
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public bool? meta_hideMenu { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<tb_routes> children { get; set; }

        

    }
}

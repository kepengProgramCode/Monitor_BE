using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///菜单管理
    ///</summary>
    [SugarTable("tb_menu")]
    public partial class tb_menu
    {
           public tb_menu(){


           }
           /// <summary>
           /// Desc:编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:菜单名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string name {get;set;}

           /// <summary>
           /// Desc:父菜单ID，一级菜单为0
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? parent_id {get;set;}

           /// <summary>
           /// Desc:菜单URL,类型：1.普通页面（如用户管理， /sys/user） 2.嵌套完整外部页面，以http(s)开头的链接 3.嵌套服务器页面，使用iframe:前缀+目标URL(如SQL监控， iframe:/druid/login.html, iframe:前缀会替换成服务器地址)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string url {get;set;}

           /// <summary>
           /// Desc:授权(多个用逗号分隔，如：sys:user:add,sys:user:edit)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string perms {get;set;}

           /// <summary>
           /// Desc:类型   0：目录   1：菜单   2：按钮
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? type {get;set;}

           /// <summary>
           /// Desc:菜单图标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string icon {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? order_num {get;set;}

           /// <summary>
           /// Desc:创建人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string create_by {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? create_time {get;set;}

           /// <summary>
           /// Desc:更新人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string last_update_by {get;set;}

           /// <summary>
           /// Desc:更新时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? last_update_time {get;set;}

           /// <summary>
           /// Desc:是否删除  -1：已删除  0：正常
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public byte? del_flag {get;set;}

    }
}

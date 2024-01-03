using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_user")]
    public partial class tb_user
    {
           public tb_user(){


           }
           /// <summary>
           /// Desc:用户Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int u_id {get;set;}

           /// <summary>
           /// Desc:用户会话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string u_token {get;set;}

           /// <summary>
           /// Desc:组别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? groupId {get;set;}

           /// <summary>
           /// Desc:名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string u_name {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string u_pwd {get;set;}

           /// <summary>
           /// Desc:权限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? permission {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string changeby {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? changetime {get;set;}

           /// <summary>
           /// Desc:用户邮箱
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string u_email {get;set;}

           /// <summary>
           /// Desc:注册地点
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string region {get;set;}

           /// <summary>
           /// Desc:部门
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string userdpt {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string position {get;set;}

           /// <summary>
           /// Desc:状态0：
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? status {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string u_tital {get;set;}

           /// <summary>
           /// Desc:用户类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? u_type {get;set;}

           /// <summary>
           /// Desc:token过期时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? u_expired {get;set;}

    }
}

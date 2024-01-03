using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///用户与角色对应关系
    ///</summary>
    [SugarTable("tb_user_role")]
    public partial class tb_user_role
    {
           public tb_user_role(){


           }
           /// <summary>
           /// Desc:编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? user_id {get;set;}

           /// <summary>
           /// Desc:角色ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? role_id {get;set;}

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

    }
}

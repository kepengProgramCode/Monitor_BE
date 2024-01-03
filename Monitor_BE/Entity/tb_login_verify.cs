using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_login_verify")]
    public partial class tb_login_verify
    {
           public tb_login_verify(){


           }
           /// <summary>
           /// Desc:会话id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string session_id {get;set;}

           /// <summary>
           /// Desc:用户id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int u_id {get;set;}

           /// <summary>
           /// Desc:过期时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? expire_time {get;set;}

    }
}

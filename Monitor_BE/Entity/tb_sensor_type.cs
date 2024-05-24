using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_sensor_type")]
    public partial class tb_sensor_type
    {
           public tb_sensor_type(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string sensor_type_id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string name {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string desc {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int sensor_type_id_int {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string sensor_class {get;set;}

    }
}

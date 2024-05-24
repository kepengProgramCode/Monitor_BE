using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_sensor_class")]
    public partial class tb_sensor_class
    {
        public tb_sensor_class()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int sensor_class_id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sensor_class { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string desc { get; set; }

    }
}

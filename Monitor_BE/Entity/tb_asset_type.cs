using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_asset_type")]
    public partial class tb_asset_type
    {
        public tb_asset_type()
        {

        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string asset_type_id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string asset_type { get; set; }

        /// <summary>
        /// Desc:1:笔记本 2：Dock 3.服务器 4.交换机 5.路由器 6.手机 7.台式机 8.屏幕 9.打印机 10.标签机 11.AP 12.扫码器 13.相机 14 手持设备
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string asset_attrb { get; set; }

    }
}

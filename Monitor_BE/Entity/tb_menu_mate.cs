using SqlSugar;

namespace Monitor_BE.Entity
{
    public class tb_menu_mate
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int id { get; set; }
        /// <summary>
        /// Desc:图标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string icon { get; set; }

        /// <summary>
        /// Desc:标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string title { get; set; }

        /// <summary>
        /// Desc:活跃菜单路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string activeMenu { get; set; }

        /// <summary>
        /// Desc:链接标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string isLink { get; set; }

        /// <summary>
        /// Desc:是否隐藏
        /// Default:true
        /// Nullable:True
        /// </summary>           
        public bool? isHide { get; set; }

        /// <summary>
        /// Desc:是否全屏
        /// Default:true
        /// Nullable:True
        /// </summary>           
        public bool? isFull { get; set; }

        /// <summary>
        /// Desc:是否固定
        /// Default:true
        /// Nullable:True
        /// </summary>           
        public bool? isAffix { get; set; }

        /// <summary>
        /// Desc:是否保持活跃
        /// Default:true
        /// Nullable:True
        /// </summary>           
        public bool? isKeepAlive { get; set; }
    }
}

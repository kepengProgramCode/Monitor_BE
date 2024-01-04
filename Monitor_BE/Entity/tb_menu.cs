using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_menu")]
    public partial class tb_menu
    {
        public tb_menu()
        {


        }
        /// <summary>
        /// Desc:主键，自增
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// Desc:父级ID，关联id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? parent_id { get; set; }

        /// <summary>
        /// Desc:路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string path { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string name { get; set; }

        /// <summary>
        /// Desc:重定向路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string redirect { get; set; }

        /// <summary>
        /// Desc:组件路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string component { get; set; }


        [Navigate(NavigateType.OneToOne, nameof(id), nameof(tb_menu_mate.id))]
        public tb_menu_mate meta { get; set; }


        [SugarColumn(IsIgnore = true)]
        public List<tb_menu> children { get; set; }

    }
}

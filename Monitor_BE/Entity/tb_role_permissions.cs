using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Monitor_BE.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_role_permissions")]
    public partial class tb_role_permissions
    {
        public tb_role_permissions()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>     
        [SugarColumn(IsIgnore = true)]
        public long? role_id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string useProTable { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string authButton { get; set; }

    }
}

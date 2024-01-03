using Monitor_BE.Entity;
using Monitor_BE.Repository;

namespace Monitor_BE.ServerBusiness
{
    public class AssetService : AccessClient<tb_asset>
    {
        public List<tb_asset> GetAsset(tb_asset assets)
        {
            return Db.Queryable<tb_asset>().ToList();//查询所有
        }


        public List<tb_asset_type> GetAssetType()
        {
            return Db.Ado.SqlQuery<tb_asset_type>("SELECT * FROM `hbpo_cn1`.`tb_asset_types` LIMIT 0,1000");
        }
    }
}

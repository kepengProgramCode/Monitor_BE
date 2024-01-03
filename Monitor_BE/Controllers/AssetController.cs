using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitor_BE.Common.Response;
using Monitor_BE.Entity;
using Monitor_BE.ServerBusiness;

namespace Monitor_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AssetController : ApiControllerBase
    {
        private readonly AssetService asset;
        private readonly LogService log;

        public AssetController(AssetService _asset, LogService log)
        {
            asset = _asset;
            this.log = log;
        }

        /// <summary>
        /// 获取资产
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("GetAsset")]
        public ResponseResult<IEnumerable<tb_asset>>? GetAsset(tb_asset assets)
        {
            //生成实体表结构
            var data = asset.GetAsset(assets);
            return data.ToList();
        }


        /// <summary>
        /// 获取资产类型
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("GetAssetType")]

        public ResponseResult<IEnumerable<tb_asset_type>>? GetAssetType()
        {
            var data = asset.GetAssetType();
            return data.ToList();
        }

        protected override void OnWriteError(string action, string message)
        {
            throw new NotImplementedException();
        }
    }
}

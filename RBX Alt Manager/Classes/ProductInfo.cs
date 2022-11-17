using Newtonsoft.Json;
using System;

namespace RBX_Alt_Manager
{
    public class Creator
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreatorType { get; set; }
        public long CreatorTargetId { get; set; }
    }

    public class ProductInfo
    {
        public long TargetId { get; set; }
        public object ProductType { get; set; }
        public long AssetId { get; set; }
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssetTypeId { get; set; }
        public Creator Creator { get; set; }
        public long IconImageAssetId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public object PriceInRobux { get; set; }
        public object PriceInTickets { get; set; }
        public int Sales { get; set; }
        public bool IsNew { get; set; }
        public bool IsForSale { get; set; }
        public bool IsPublicDomain { get; set; }
        public bool IsLimited { get; set; }
        public bool IsLimitedUnique { get; set; }
        public object Remaining { get; set; }
        public int MinimumMembershipLevel { get; set; }
        public int ContentRatingTypeId { get; set; }
    }

    public partial class InvalidAsset
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("assetType")]
        public AssetType AssetType { get; set; }
    }

    public partial class AssetType
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

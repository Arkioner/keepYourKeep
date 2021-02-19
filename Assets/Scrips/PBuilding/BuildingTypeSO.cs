using Scrips.PResource.PGenerator;
using UnityEngine;

namespace Scrips.PBuilding
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
    public class BuildingTypeSO : ScriptableObject
    {
        [SerializeField] private BuildingTypeId id = BuildingTypeId.WoodHarvester;
        [SerializeField] [NotNull] private Transform prefab = null;
        [SerializeField] private ResourceGeneratorDataHolder resourceGeneratorDataHolder = null;
        [SerializeField] [NotNull] private Sprite icon = null;

        public BuildingTypeId Id => id;
        public Transform Prefab => prefab;
        public ResourceGeneratorDataHolder ResourceGeneratorDataHolder => resourceGeneratorDataHolder;

        public Sprite Icon => icon;
    }
}
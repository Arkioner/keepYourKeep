using UnityEngine;

namespace Scrips.PResource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourcesType")]
    public class ResourceTypeSO : ScriptableObject
    {
        [SerializeField] private ResourceTypeId id = ResourceTypeId.Wood;
        [SerializeField] private Sprite _icon = null;

        public ResourceTypeId Id => id;
        public Sprite Icon => _icon;
    }
}
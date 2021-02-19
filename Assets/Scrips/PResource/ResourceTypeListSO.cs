using System.Collections.Generic;
using UnityEngine;

namespace Scrips.PResource
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourcesTypeList")]
    public class ResourceTypeListSO : ScriptableObject
    {
        [SerializeField] [NotNull] private List<ResourceTypeSO> items = null;

        public List<ResourceTypeSO> Items => items;
    }
}
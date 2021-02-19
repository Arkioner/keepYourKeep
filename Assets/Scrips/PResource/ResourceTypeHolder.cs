using UnityEngine;

namespace Scrips.PResource
{
    public class ResourceTypeHolder : MonoBehaviour
    {
        [SerializeField] [NotNull] private ResourceTypeSO resourceTypeSO = null;

        public ResourceTypeSO ResourceTypeSO => resourceTypeSO;
    }
}

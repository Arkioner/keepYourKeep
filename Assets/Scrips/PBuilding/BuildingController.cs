using UnityEngine;

namespace Scrips.PBuilding
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] [NotNull] private BuildingTypeSO buildingTypeSO = null;

        public BuildingTypeSO BuildingTypeSO => buildingTypeSO;

        private void Start()
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = buildingTypeSO.Icon;
        }
    }
}
using System;
using UnityEngine;

namespace Scrips.PBuilding
{
    public class BuildingGhost : MonoBehaviour
    {
        private GameObject _spriteGameObject;

        private void Awake()
        {
            _spriteGameObject = transform.Find("Sprite").gameObject;
            Hide();
        }

        private void Start()
        {
            BuildingManager.OnActiveBuildingTypeChanged += BuildingManager_OnOnActiveBuildingTypeChanged;
        }

        private void BuildingManager_OnOnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEvent e)
        {
            if (e.activeBuildingType is null)
            {
                Hide();
            }
            else
            {
                Show(e.activeBuildingType.Icon);
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = UIUtils.GetMouseWorldPosition();
        }

        private void Show(Sprite ghostSprite)
        {
            _spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
            _spriteGameObject.SetActive(true);
        }

        private void Hide()
        {
            _spriteGameObject.SetActive(false);
        }
    }
}

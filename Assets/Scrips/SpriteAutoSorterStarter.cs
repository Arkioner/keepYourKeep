using System.Collections.Generic;
using UnityEngine;

namespace Scrips
{
    [ExecuteInEditMode]
    public class SpriteAutoSorterStarter : MonoBehaviour
    {
        [SerializeField] private bool staticEntity = true;
        private static int _sortingPrecision = 100;
        private readonly List<SpriteRenderer> _spriteRenderers = new List<SpriteRenderer>();

        void Awake()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                _spriteRenderers.Add(spriteRenderer);
            }
        }
        private void Update()
        {
            Debug.Log("Its time: " + Time.time + " for: " + this.gameObject.name);
            _spriteRenderers
                .ForEach(sr => sr.sortingOrder = (int) (-transform.position.y * _sortingPrecision));
            if (staticEntity && Application.isPlaying)
            {
                Destroy(this);
            }
        }
    }
}
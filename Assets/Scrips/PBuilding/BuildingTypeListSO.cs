using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scrips.PBuilding
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
    public class BuildingTypeListSO : ScriptableObject
    {
        [SerializeField] [NotNull] private List<BuildingTypeSO> items = null;

        private Dictionary<BuildingTypeId, BuildingTypeSO> _hashedBuildings;

        private Dictionary<BuildingTypeId, BuildingTypeSO> buildHashedBuildings()
        {
            _hashedBuildings = items
                .Aggregate(
                    new Dictionary<BuildingTypeId, BuildingTypeSO>(),
                    (dic, so) =>
                    {
                        dic.Add(so.Id, so);
                        return dic;
                    });
            return _hashedBuildings;
        }

        public Dictionary<BuildingTypeId, BuildingTypeSO> Items =>
            _hashedBuildings ?? buildHashedBuildings();
    }
}
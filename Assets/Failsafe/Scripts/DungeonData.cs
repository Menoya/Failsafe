using System.Collections.Generic;
using UnityEngine;

namespace Failsafe.Scripts {
    //[CreateAssetMenu(fileName = "DungeonData", menuName = "DMDungeonGenerator/Create Dungeon Data", order = 1)]

    public class DungeonData : ScriptableObject {

        public List<DungeonSet> sets = new List<DungeonSet>();
    }
}
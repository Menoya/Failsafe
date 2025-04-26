using UnityEngine;

/// <summary>
/// Your "gameplay" door, with the logic for opening/closing, interating with other game specific systems, animation ,etc etc needs to have this component on it's root, as this is the data you can hook into
/// To find out if this door is locked, locked by what, etc
/// </summary>

namespace Failsafe.Scripts {
    public class GeneratorDoor:MonoBehaviour {

        public GraphConnection data;

    }
}

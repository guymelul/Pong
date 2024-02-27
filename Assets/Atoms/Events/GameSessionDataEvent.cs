using UnityEngine;

namespace UnityAtoms.PongGame
{
    /// <summary>
    /// Event of type `GameSessionData`. Inherits from `AtomEvent&lt;GameSessionData&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/GameSessionData", fileName = "GameSessionDataEvent")]
    public sealed class GameSessionDataEvent : AtomEvent<GameSessionData>
    {
    }
}

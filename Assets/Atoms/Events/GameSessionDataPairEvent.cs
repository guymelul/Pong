using UnityEngine;

namespace UnityAtoms.PongGame
{
    /// <summary>
    /// Event of type `GameSessionDataPair`. Inherits from `AtomEvent&lt;GameSessionDataPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/GameSessionDataPair", fileName = "GameSessionDataPairEvent")]
    public sealed class GameSessionDataPairEvent : AtomEvent<GameSessionDataPair>
    {
    }
}

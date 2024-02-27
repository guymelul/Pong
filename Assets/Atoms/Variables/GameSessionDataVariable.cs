using UnityEngine;
using System;

namespace UnityAtoms.PongGame
{
    /// <summary>
    /// Variable of type `GameSessionData`. Inherits from `AtomVariable&lt;GameSessionData, GameSessionDataPair, GameSessionDataEvent, GameSessionDataPairEvent, GameSessionDataGameSessionDataFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/GameSessionData", fileName = "GameSessionDataVariable")]
    public sealed class GameSessionDataVariable : AtomVariable<GameSessionData, GameSessionDataPair, GameSessionDataEvent, GameSessionDataPairEvent, GameSessionDataGameSessionDataFunction>
    {
        protected override bool ValueEquals(GameSessionData other)
        {
            return this.Equals(other);
        }
    }
}

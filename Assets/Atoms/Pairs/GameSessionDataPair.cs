using System;
using UnityEngine;
namespace UnityAtoms.PongGame
{
    /// <summary>
    /// IPair of type `&lt;GameSessionData&gt;`. Inherits from `IPair&lt;GameSessionData&gt;`.
    /// </summary>
    [Serializable]
    public struct GameSessionDataPair : IPair<GameSessionData>
    {
        public GameSessionData Item1 { get => _item1; set => _item1 = value; }
        public GameSessionData Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private GameSessionData _item1;
        [SerializeField]
        private GameSessionData _item2;

        public void Deconstruct(out GameSessionData item1, out GameSessionData item2) { item1 = Item1; item2 = Item2; }
    }
}
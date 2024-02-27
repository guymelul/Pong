using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms.PongGame
{
    [Serializable]
    public struct GameSessionPlayerData : IEquatable<GameSessionPlayerData>
    {
        public bool IsHuman;
        public Sprite PaddleSprite;

        public override bool Equals(object obj)
        {
            return obj is GameSessionPlayerData data && Equals(data);
        }

        public bool Equals(GameSessionPlayerData other)
        {
            return IsHuman == other.IsHuman &&
                   EqualityComparer<Sprite>.Default.Equals(PaddleSprite, other.PaddleSprite);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsHuman, PaddleSprite);
        }
    }

    [Serializable]
    public struct GameSessionData : IEquatable<GameSessionData>
    {
        public int WinScore;
        public GameSessionPlayerData Player1;
        public GameSessionPlayerData Player2;

        public int HumanPlayerCount
        {
            get { return (Player1.IsHuman ? 1 : 0) + (Player2.IsHuman ? 1 : 0); }
        }

        public override bool Equals(object obj)
        {
            return obj is GameSessionData data && Equals(data);
        }

        public bool Equals(GameSessionData other)
        {
            return EqualityComparer<GameSessionPlayerData>.Default.Equals(Player1, other.Player1) &&
                   EqualityComparer<GameSessionPlayerData>.Default.Equals(Player2, other.Player2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Player1, Player2);
        }
    }
}
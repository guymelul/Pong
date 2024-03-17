using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms.PongGame
{
    [Serializable]
    public struct GameSessionPlayerData : IEquatable<GameSessionPlayerData>, ICloneable
    {
        public bool IsHuman;
        public Sprite PaddleSprite;

        public object Clone()
        {
            GameSessionPlayerData clone = new GameSessionPlayerData();
            clone.IsHuman = IsHuman;
            clone.PaddleSprite = PaddleSprite;

            return clone;
        }

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
    public struct GameSessionData : IEquatable<GameSessionData>, ICloneable
    {
        public int WinScore;
        public GameSessionPlayerData Player1;
        public GameSessionPlayerData Player2;

        public int HumanPlayerCount
        {
            get { return (Player1.IsHuman ? 1 : 0) + (Player2.IsHuman ? 1 : 0); }
        }

        public object Clone()
        {
            GameSessionData clone = new GameSessionData();

            clone.WinScore = WinScore;
            clone.Player1 = (GameSessionPlayerData)Player1.Clone();
            clone.Player2 = (GameSessionPlayerData)Player2.Clone();

            return clone;
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
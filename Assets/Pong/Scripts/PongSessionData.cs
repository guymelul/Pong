using System;
using UnityEngine;

namespace MalulsArcade.Pong
{
    [Serializable]
    [CreateAssetMenu(menuName = "MalulsArcade/Pong/PongSessionPlayerData")]
    public class PongSessionPlayerData : ScriptableObject
    {
        public bool IsHuman;
        public Sprite PaddleSprite;
    }

    [Serializable]
    [CreateAssetMenu(menuName = "MalulsArcade/Pong/PongSessionData")]
    public class PongSessionData : ScriptableObject
    {
        public int WinScore;
        public PongSessionPlayerData Player1;
        public PongSessionPlayerData Player2;

        public int HumanPlayerCount
        {
            get { return (Player1.IsHuman ? 1 : 0) + (Player2.IsHuman ? 1 : 0); }
        }
    }
}
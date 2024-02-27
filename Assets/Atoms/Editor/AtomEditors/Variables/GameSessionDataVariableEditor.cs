using UnityAtoms.Editor;
using UnityEditor;

namespace UnityAtoms.PongGame.Editor
{
    /// <summary>
    /// Variable Inspector of type `GameSessionData`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(GameSessionDataVariable))]
    public sealed class GameSessionDataVariableEditor : AtomVariableEditor<GameSessionData, GameSessionDataPair> { }
}

#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.PongGame.Editor
{
    /// <summary>
    /// Event property drawer of type `GameSessionData`. Inherits from `AtomEventEditor&lt;GameSessionData, GameSessionDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(GameSessionDataEvent))]
    public sealed class GameSessionDataEventEditor : AtomEventEditor<GameSessionData, GameSessionDataEvent> { }
}
#endif

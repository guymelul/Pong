#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.PongGame.Editor
{
    /// <summary>
    /// Event property drawer of type `GameSessionDataPair`. Inherits from `AtomEventEditor&lt;GameSessionDataPair, GameSessionDataPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(GameSessionDataPairEvent))]
    public sealed class GameSessionDataPairEventEditor : AtomEventEditor<GameSessionDataPair, GameSessionDataPairEvent> { }
}
#endif

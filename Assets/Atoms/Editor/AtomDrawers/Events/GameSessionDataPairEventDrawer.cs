#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.PongGame.Editor
{
    /// <summary>
    /// Event property drawer of type `GameSessionDataPair`. Inherits from `AtomDrawer&lt;GameSessionDataPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(GameSessionDataPairEvent))]
    public class GameSessionDataPairEventDrawer : AtomDrawer<GameSessionDataPairEvent> { }
}
#endif

#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.PongGame.Editor
{
    /// <summary>
    /// Event property drawer of type `GameSessionData`. Inherits from `AtomDrawer&lt;GameSessionDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(GameSessionDataEvent))]
    public class GameSessionDataEventDrawer : AtomDrawer<GameSessionDataEvent> { }
}
#endif

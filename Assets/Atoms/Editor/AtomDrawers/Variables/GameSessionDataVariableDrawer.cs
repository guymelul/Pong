#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.PongGame.Editor
{
    /// <summary>
    /// Variable property drawer of type `GameSessionData`. Inherits from `AtomDrawer&lt;GameSessionDataVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(GameSessionDataVariable))]
    public class GameSessionDataVariableDrawer : VariableDrawer<GameSessionDataVariable> { }
}
#endif

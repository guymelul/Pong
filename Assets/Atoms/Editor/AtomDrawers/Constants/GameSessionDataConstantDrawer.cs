#if UNITY_2019_1_OR_NEWER
using UnityAtoms.Editor;
using UnityEditor;

namespace UnityAtoms.PongGame.Editor
{
    /// <summary>
    /// Constant property drawer of type `GameSessionData`. Inherits from `AtomDrawer&lt;GameSessionDataConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(GameSessionDataConstant))]
    public class GameSessionDataConstantDrawer : VariableDrawer<GameSessionDataConstant> { }
}
#endif

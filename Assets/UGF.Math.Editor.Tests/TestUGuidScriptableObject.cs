using UGF.Math.Runtime;
using UnityEngine;

namespace UGF.Math.Editor.Tests
{
    [CreateAssetMenu(menuName = "Test/TestUGuidScriptableObject")]
    public class TestUGuidScriptableObject : ScriptableObject
    {
        [SerializeField] private UGuid m_uguid = new UGuid("ea68094b39bd45be8226e104633310cd");

        public UGuid Uguid { get { return m_uguid; } set { m_uguid = value; } }
    }
}
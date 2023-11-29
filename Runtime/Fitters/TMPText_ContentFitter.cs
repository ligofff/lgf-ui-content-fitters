using TMPro;
using UnityEngine;

namespace Layout
{
    [RequireComponent(typeof(RectTransform)), ExecuteAlways, DefaultExecutionOrder(32000)]
    public class TMPText_ContentFitter : LayoutElementControllerBase
    {
        public TextMeshProUGUI text;

        protected override float GetMinimalX => text != null ? text.preferredWidth : base.GetMinimalX;

        protected override float GetMinimalY => text != null ? text.preferredHeight : base.GetMinimalY;

        protected override void Start()
        {
            base.Start();
            if (text != null) text.ForceMeshUpdate();
        }
    }
}
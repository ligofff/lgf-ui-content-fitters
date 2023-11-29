using UnityEngine;
using UnityEngine.UI;

namespace Layout
{
    [ExecuteAlways, RequireComponent(typeof(RectTransform)), DefaultExecutionOrder(32000)]
    public class HorizontalLayout_ContentFitter : LayoutElementControllerBase
    {
        public LayoutGroup layoutGroup;

        protected override float GetMinimalX => layoutGroup != null ? layoutGroup.preferredWidth : base.GetMinimalX;
        protected override float GetMinimalY => layoutGroup != null ? layoutGroup.preferredHeight : base.GetMinimalY;
    }
}
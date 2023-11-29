using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Layout
{
    [ExecuteAlways, DefaultExecutionOrder(32000), RequireComponent(typeof(RectTransform))]
    public class ExternalRect_ContentFitter : LayoutElementControllerBase
    {
        [SerializeField, TitleGroup(" ")]
        private RectTransform syncRectWith;

        protected override float GetMinimalX =>
            syncRectWith != null ? LayoutUtility.GetMinSize(syncRectWith, 0) : base.GetMinimalX;

        protected override float GetMinimalY =>
            syncRectWith != null ? LayoutUtility.GetMinSize(syncRectWith, 1) : base.GetMinimalY;
    }
}
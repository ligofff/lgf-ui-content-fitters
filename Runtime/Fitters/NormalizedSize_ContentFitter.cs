using Sirenix.OdinInspector;
using UnityEngine;

namespace Layout
{
    public class NormalizedSize_ContentFitter : LayoutElementControllerBase
    {
        [Range(0f, 1f)]
        public float normalizedSizeX = 0.5f;
        
        [Range(0f, 1f)]
        public float normalizedSizeY = 0.5f;

        public bool fromParent;
        
        [ShowIf("@!fromParent")]
        public RectTransform otherRect;

        public RectTransform Rect => fromParent ? ParentRect : otherRect;
        private RectTransform ParentRect => Application.isPlaying ? _cachedParent ??= GetComponentInParent<RectTransform>() : GetComponentInParent<RectTransform>();

        private RectTransform _cachedParent;

        protected override float GetMinimalX => Rect != null ? Rect.rect.size.x * normalizedSizeX : 10;
        protected override float GetMinimalY => Rect != null ? Rect.rect.size.y * normalizedSizeY : 10;

    }
}
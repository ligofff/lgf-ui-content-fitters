using System.Collections.Generic;
using Layout.LayoutControllerOptions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Layout
{
    [DefaultExecutionOrder(32000)]
    public abstract class LayoutElementControllerBase : UIBehaviour, ILayoutSelfController
    {
        [TitleGroup(" ")]
        public bool syncX;

        [TitleGroup(" ")]
        public bool syncY;

        [TitleGroup("Clamp")]
        public bool clampYSizeSync;

        [ShowIf("@clampYSizeSync"), TitleGroup("Clamp")]
        public float minYSizeSync;

        [ShowIf("@clampYSizeSync"), TitleGroup("Clamp")]
        public float maxYSizeSync = 300f;

        [TitleGroup("Clamp")]
        public bool clampXSizeSync;

        [ShowIf("@clampXSizeSync"), TitleGroup("Clamp")]
        public float minXSizeSync;

        [ShowIf("@clampXSizeSync"), TitleGroup("Clamp")]
        public float maxXSizeSync = 500f;

        [ShowIf("@syncX"), TitleGroup("Offsets")]
        public float xOffset;

        [ShowIf("@syncY"), TitleGroup("Offsets")]
        public float yOffset;

        protected virtual float GetMinimalX => 100f;
        protected virtual float GetMinimalY => 100f;

        protected RectTransform ThisRect => _cachedThisRect ??= GetComponent<RectTransform>();

        private RectTransform _cachedThisRect;

        [SerializeField, TitleGroup("Options")]
        private bool useOptions;

        [SerializeReference, ShowIf("@useOptions")]
        private List<BaseLayoutControllerOption> _options = new List<BaseLayoutControllerOption>();

        [TitleGroup("Other")]
        protected override void OnEnable()
        {
            base.OnEnable();

            SetDirty();
        }

        public void SetLayoutHorizontal()
        {
            if (!syncX) return;

            var xMinSize = GetMinimalX;
            xMinSize += xOffset;
            if (clampXSizeSync) xMinSize = Mathf.Clamp(xMinSize, minXSizeSync, maxXSizeSync);

            var newSize = new Vector2(xMinSize, ThisRect.sizeDelta.y);

            if (useOptions)
            {
                foreach (var option in _options)
                {
                    newSize.x = option.GetRectSize(newSize).x;
                }
            }
            
            ThisRect.sizeDelta = newSize;
        }

        public void SetLayoutVertical()
        {
            if (!syncY) return;

            var yMinSize = GetMinimalY;
            yMinSize += yOffset;
            if (clampYSizeSync) yMinSize = Mathf.Clamp(yMinSize, minYSizeSync, maxYSizeSync);

            var newSize = new Vector2(ThisRect.sizeDelta.x, yMinSize);
            
            if (useOptions)
            {
                foreach (var option in _options)
                {
                    newSize.y = option.GetRectSize(newSize).y;
                }
            }
            
            ThisRect.sizeDelta = newSize;
        }

        protected override void OnCanvasHierarchyChanged()
        {
            base.OnCanvasHierarchyChanged();
            //StartUpdateCoroutine();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            //StartUpdateCoroutine();
        }

        protected void SetDirty()
        {
            if (!IsActive())
                return;

            LayoutRebuilder.MarkLayoutForRebuild(ThisRect);
        }

        protected void UpdateLayout()
        {
            SetLayoutHorizontal();
            SetLayoutVertical();
        }

        private void LateUpdate()
        {
            SetDirty();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            SetDirty();
        }
#endif
    }
}
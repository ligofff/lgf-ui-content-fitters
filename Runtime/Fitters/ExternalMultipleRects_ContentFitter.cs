using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Layout
{
    [ExecuteAlways, RequireComponent(typeof(RectTransform)), DefaultExecutionOrder(32000)]
    public class ExternalMultipleRects_ContentFitter : LayoutElementControllerBase
    {
        public enum SyncType
        {
            FirstRect,
            MaxSizeYRect,
            MaxSizeXRect,
            YSumOfActiveRects
        }

        [SerializeField, TitleGroup(" ")]
        private List<RectTransform> rects;

        [SerializeField, TitleGroup(" ")]
        private SyncType syncType;

        protected override float GetMinimalX => GetTargetRect() != null ? GetTargetRect().rect.width : base.GetMinimalX;

        protected override float GetMinimalY =>
            syncType == SyncType.YSumOfActiveRects ?
                rects.Sum(rect => rect == null || !rect.gameObject.activeSelf ? 0 : rect.sizeDelta.y)
                :GetTargetRect() != null ? GetTargetRect().rect.height : base.GetMinimalY;


        private RectTransform GetTargetRect()
        {
            switch (syncType)
            {
                case SyncType.FirstRect:
                    return rects.FirstOrDefault();
                case SyncType.MaxSizeYRect:
                    return rects
                        .OrderByDescending(rect => rect == null || !rect.gameObject.activeSelf ? 0 : rect.sizeDelta.y)
                        .FirstOrDefault();
                case SyncType.MaxSizeXRect:
                    return rects
                        .OrderByDescending(rect => rect == null || !rect.gameObject.activeSelf ? 0 : rect.sizeDelta.x)
                        .FirstOrDefault();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
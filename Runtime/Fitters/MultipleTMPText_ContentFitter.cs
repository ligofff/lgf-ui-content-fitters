using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Layout
{
    [ExecuteAlways, RequireComponent(typeof(RectTransform)), DefaultExecutionOrder(32000)]
    public class MultipleTMPText_ContentFitter : LayoutElementControllerBase
    {
        public enum SyncType
        {
            FirstRect,
            MaxSizeYRect,
            MaxSizeXRect
        }

        [SerializeField, TitleGroup(" ")]
        private List<TextMeshProUGUI> rects;

        [SerializeField, TitleGroup(" ")]
        private SyncType syncType;

        protected override float GetMinimalX => GetTargetText() != null ? GetTargetText().preferredWidth : base.GetMinimalX;

        protected override float GetMinimalY => GetTargetText() != null ? GetTargetText().preferredHeight : base.GetMinimalY;


        private TextMeshProUGUI GetTargetText()
        {
            switch (syncType)
            {
                case SyncType.FirstRect:
                    return rects.FirstOrDefault();
                case SyncType.MaxSizeYRect:
                    return rects
                        .OrderByDescending(rect => rect == null || !rect.gameObject.activeSelf ? 0 : rect.preferredHeight)
                        .FirstOrDefault();
                case SyncType.MaxSizeXRect:
                    return rects
                        .OrderByDescending(rect => rect == null || !rect.gameObject.activeSelf ? 0 : rect.preferredWidth)
                        .FirstOrDefault();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
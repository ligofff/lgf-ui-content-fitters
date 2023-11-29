using System;
using UnityEngine;

namespace Layout.LayoutControllerOptions
{
    [Serializable]
    public abstract class BaseLayoutControllerOption
    {
        public virtual bool OverrideMinX => false;
        public virtual float GetMinX { get; }
        
        public virtual bool OverrideMinY => false;
        public virtual float GetMinY { get; }
        
        public virtual bool OverrideMaxX => false;
        public virtual float GetMaxX { get; }
        
        public virtual bool OverrideMaxY => false;
        public virtual float GetMaxY { get; }

        public Vector2 GetRectSize(Vector2 currentSize)
        {
            var newSize = currentSize;
            
            if (OverrideMinX)
            {
                newSize.x = Mathf.Clamp(newSize.x, GetMinX, 100000);
            }
            
            if (OverrideMinY)
            {
                newSize.y = Mathf.Clamp(newSize.y, GetMinY, 100000);
            }

            if (OverrideMaxX)
            {
                newSize.x = Mathf.Clamp(newSize.x, 0, GetMaxX);
            }

            if (OverrideMaxY)
            {
                newSize.y = Mathf.Clamp(newSize.y, 0, GetMaxY);
            }

            return newSize;
        }
    }
}
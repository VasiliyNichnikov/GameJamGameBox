using UnityEngine;

namespace Extensions
{
    public static class UIUtils
    {
        public static void PinToLeft(this RectTransform rt)
        {
            rt.anchorMin = new Vector2(1, 0);
            rt.anchorMax = new Vector2(1, 1);
            
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        public static void UIFullScreen(this RectTransform rt)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        public static RectTransform GetRectTransform(this Transform transform)
        {
            return transform as RectTransform;
        }
    }
}
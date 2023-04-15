using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Samurai.Showcase.Runtime
{
    public static class ShowcaseExtensions
    {
        public static void SetActive<T>(this T @this, bool state) where T : Component
        {
            if (@this.gameObject.activeSelf != state)
            {
                @this.gameObject.SetActive(state);
            }
        }

        public static void SetOnClick(this Button @this, UnityAction callback)
        {
            @this.onClick.RemoveAllListeners();
            @this.onClick.AddListener(callback);
        }
    }
}
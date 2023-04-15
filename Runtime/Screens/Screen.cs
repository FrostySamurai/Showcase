using System;
using UnityEngine;

namespace Samurai.Showcase.Runtime.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        public bool IsActive => gameObject.activeSelf;
        public abstract Type DataType { get; }
        
        public virtual void Show()
        {
            this.SetActive(true);
        }

        public virtual void Hide()
        {
            this.SetActive(false);
        }
    }
    
    public abstract class Screen<TData> : Screen
    {
        protected TData Parameters;

        public override Type DataType => typeof(TData);

        public virtual void Init(TData parameters)
        {
            Parameters = parameters;
        }
    }
}

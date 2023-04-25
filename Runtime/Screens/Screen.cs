using System;
using UnityEngine;

namespace Samurai.Showcase.Runtime.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        public bool IsActive => gameObject.activeSelf;
        public abstract Type DataType { get; }
        
        internal void Show()
        {
            this.SetActive(true);
            
            OnShow();
        }

        internal void Hide()
        {
            this.SetActive(false);
            
            OnHide();
        }
        
        protected virtual void OnShow() {}
        protected virtual void OnHide() {}
    }
    
    public abstract class Screen<TData> : Screen
    {
        private TData _parameters;
        protected TData Parameters => _parameters;

        public override Type DataType => typeof(TData);

        public void Init(TData parameters)
        {
            _parameters = parameters;
            OnInit();
        }

        protected virtual void OnInit() {}
    }
}

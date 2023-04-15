using System;
using System.Collections.Generic;
using Samurai.Showcase.Runtime.Layers;
using Samurai.Showcase.Runtime.Screens;
using UnityEngine;

namespace Samurai.Showcase.Runtime
{
    public class PanelHandler : IScreenHandler
    {
        private readonly Dictionary<Type, IPanel> _panels = new();

        #region Registration

        public void Register(Layer layer)
        {
            foreach (var screen in layer.Screens)
            {
                if (_panels.ContainsKey(screen.DataType))
                {
                    Debug.LogWarning($"Multiple panels found for data type '{screen.DataType.FullName}'. Skipping..");
                    continue;
                }
                
                if (screen is IPanel panel)
                {
                    _panels[screen.DataType] = panel;
                }
            }
        }

        public void Unregister(Layer layer)
        {
            foreach (var screen in layer.Screens)
            {
                if (screen is IPanel)
                {
                    _panels.Remove(screen.DataType);
                }
            }
        }

        #endregion Registration

        #region Screens Lifecycle

        public void Show<TData>(TData parameters)
        {
            Show<TData>()?.Init(parameters);
        }

        public Screen<TData> Show<TData>()
        {
            var panel = Get<TData>();
            if (panel != null)
            {
                panel.Show();
            }
            return panel;
        }

        public void Hide<TData>()
        {
            Get<TData>()?.Hide();
        }

        #endregion Screens Lifecycle

        #region Queries

        public bool IsHandled<TData>()
        {
            return _panels.ContainsKey(typeof(TData));
        }

        public bool IsActive<TData>()
        {
            return Get<TData>()?.IsActive ?? false;
        }

        public Screen<TData> Get<TData>()
        {
            return _panels.TryGetValue(typeof(TData), out var panel) ? panel as Screen<TData> : null;
        }

        public TScreen Get<TScreen, TData>() where TScreen : Screen<TData>
        {
            return _panels.TryGetValue(typeof(TData), out var panel) ? panel as TScreen : null;
        }

        #endregion Queries
    }
}
﻿using System;
using System.Collections.Generic;
using Samurai.Showcase.Runtime.Layers;
using Samurai.Showcase.Runtime.Screens;
using UnityEngine;
using Screen = Samurai.Showcase.Runtime.Screens.Screen;

namespace Samurai.Showcase.Runtime
{
    public class WindowHandler : IScreenHandler
    {
        private readonly Dictionary<Type, Screen> _windows = new();

        #region Registration

        public void Register(Layer layer)
        {
            foreach (var screen in layer.Screens)
            {
                if (_windows.ContainsKey(screen.DataType))
                {
                    Debug.LogWarning($"Multiple panels found for data type '{screen.DataType.FullName}'. Skipping..");
                    continue;
                }
                
                if (screen is IWindow)
                {
                    _windows[screen.DataType] = screen;
                }
            }
        }

        public void Unregister(Layer layer)
        {
            foreach (var screen in layer.Screens)
            {
                if (screen is IWindow)
                {
                    _windows.Remove(screen.DataType);
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

        public void Hide(Type type)
        {
            if (_windows.TryGetValue(type, out var panel))
            {
                panel.Hide();
            }
        }

        public void Hide<TData>()
        {
            Get<TData>()?.Hide();
        }

        #endregion Screens Lifecycle

        #region Queries

        public bool IsHandled(Type type)
        {
            return _windows.ContainsKey(type);
        }
        
        public bool IsHandled<TData>()
        {
            return _windows.ContainsKey(typeof(TData));
        }

        public bool IsActive<TData>()
        {
            return Get<TData>()?.IsActive ?? false;
        }

        public Screen<TData> Get<TData>()
        {
            return _windows.TryGetValue(typeof(TData), out var panel) ? panel as Screen<TData> : null;
        }

        public TScreen Get<TScreen, TData>() where TScreen : Screen<TData>
        {
            return _windows.TryGetValue(typeof(TData), out var panel) ? panel as TScreen : null;
        }

        #endregion Queries
    }
}
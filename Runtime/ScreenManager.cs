using System.Collections.Generic;
using System.Linq;
using Samurai.Showcase.Runtime.Layers;
using Samurai.Showcase.Runtime.Screens;
using UnityEngine;

namespace Samurai.Showcase.Runtime
{
    public class ScreenManager : MonoBehaviour
    {
        private readonly List<IScreenHandler> _screenHandlers = new()
        {
            new PanelHandler(),
            new DialogueHandler()
        };
        
        private readonly HashSet<Layer> _registeredLayers = new();

        #region Registration

        public void Register(Layer layer)
        {
            if (!_registeredLayers.Add(layer))
            {
                Debug.LogWarning($"[ScreenManager] Layer with id '{layer.LayerId}' is already registered. Skipping..");
                return;
            }
            
            _screenHandlers.ForEach(x => x.Register(layer));
        }

        public void Unregister(Layer layer)
        {
            if (_registeredLayers.Remove(layer))
            {
                _screenHandlers.ForEach(x => x.Unregister(layer));
            }
        }

        #endregion Registration

        #region Screens

        public void Show<TData>(TData parameters)
        {
            GetHandler<TData>()?.Show(parameters);
        }

        public Screen<TData> Show<TData>()
        {
            return GetHandler<TData>()?.Show<TData>();
        }

        public void Hide<TData>()
        {
            GetHandler<TData>()?.Hide<TData>();
        }

        #endregion Screens

        #region Queries

        public bool IsActive<TData>()
        {
            return GetHandler<TData>()?.IsActive<TData>() ?? false;
        }

        public Screen<TData> Get<TData>()
        {
            return GetHandler<TData>()?.Get<TData>();
        }

        public TScreen Get<TScreen, TData>() where TScreen : Screen<TData>
        {
            return GetHandler<TData>()?.Get<TScreen, TData>();
        }

        #endregion Queries

        #region Private

        private IScreenHandler GetHandler<TData>()
        {
            return _screenHandlers.FirstOrDefault(x => x.IsHandled<TData>());
        }

        #endregion Private
    }
}

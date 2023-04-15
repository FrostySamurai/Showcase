using System.Collections.Generic;
using UnityEngine;

namespace Samurai.Showcase.Runtime.Layers
{
    public class LayerRegistrationHandler : MonoBehaviour
    {
        [SerializeField]
        private ScreenManager _screenManager;
        [SerializeField]
        private List<Layer> _layers;

        private void Start()
        {
            _layers.ForEach(x => _screenManager.Register(x));
        }

        private void OnDestroy()
        {
            if (_screenManager)
            {
                _layers.ForEach(x => _screenManager.Unregister(x));
            }
        }
    }
}
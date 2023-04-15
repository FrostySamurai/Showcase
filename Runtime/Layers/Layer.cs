using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Screen = Samurai.Showcase.Runtime.Screens.Screen;

namespace Samurai.Showcase.Runtime.Layers
{
    public class Layer : MonoBehaviour
    {
        [SerializeField]
        private string _layerId;
        [SerializeField]
        private List<Screen> _screens;

        public string LayerId => _layerId;
        public IReadOnlyList<Screen> Screens => _screens;

        private void OnValidate()
        {
            if (_screens == null || _screens.Count == 0)
            {
                _screens = GetComponentsInChildren<Screen>(true).ToList();
            }
        }
    }
}
using Samurai.Showcase.Runtime.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Showcase.Example.Scripts.Panels
{
    public readonly struct HealthPanelParameters
    {
        public readonly int Health;

        public HealthPanelParameters(int health)
        {
            Health = health;
        }
    }
    
    public class HealthWindow : Window<HealthPanelParameters>
    {
        [SerializeField]
        private Text _text;

        protected override void OnInit()
        {
            _text.text = Parameters.Health.ToString();
        }
    }
}
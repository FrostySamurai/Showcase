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
    
    public class HealthPanel : Panel<HealthPanelParameters>
    {
        [SerializeField]
        private Text _text;
        
        public override void Init(HealthPanelParameters parameters)
        {
            base.Init(parameters);

            _text.text = parameters.Health.ToString();
        }
    }
}
using Samurai.Showcase.Runtime.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Samurai.Showcase.Example.Scripts.Panels
{
    public readonly struct ManaPanelParameters
    {
        public readonly int Mana;

        public ManaPanelParameters(int mana)
        {
            Mana = mana;
        }
    }
    
    public class ManaWindow : Window<ManaPanelParameters>
    {
        [SerializeField]
        private Text _text;
        
        protected override void OnInit()
        {
            _text.text = Parameters.Mana.ToString();
        }
    }
}
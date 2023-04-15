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
    
    public class ManaPanel : Panel<ManaPanelParameters>
    {
        [SerializeField]
        private Text _text;

        public override void Init(ManaPanelParameters parameters)
        {
            base.Init(parameters);

            _text.text = parameters.Mana.ToString();
        }
    }
}
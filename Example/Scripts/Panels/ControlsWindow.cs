using Samurai.Showcase.Example.Scripts.Dialogues;
using Samurai.Showcase.Runtime;
using Samurai.Showcase.Runtime.Screens;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Samurai.Showcase.Example.Scripts.Panels
{
    public struct ControlsPanelParameters
    {
        
    }

    public class ControlsWindow : Window<ControlsPanelParameters>

    {
    [SerializeField]
    private ScreenManager _screenManager;

    [Header("Interaction")]
    [SerializeField]
    private Button _toggleHealthPanelButton;

    [SerializeField]
    private Button _toggleManaPanelButton;

    [SerializeField]
    private Button _showExampleDialogueButton;

    [SerializeField]
    private Button _showAnotherExampleDialogueButton;

    [SerializeField]
    private Button _toggleDifferentLayerDialogueButton;

    private void Awake()
    {
        _toggleHealthPanelButton.SetOnClick(() => Toggle(new HealthPanelParameters(Random.Range(0, 100))));
        _toggleManaPanelButton.SetOnClick(() => Toggle(new ManaPanelParameters(Random.Range(0, 100))));

        _toggleDifferentLayerDialogueButton.SetOnClick(() => Toggle(new DifferentLayerDialogueParameters()));
        _showExampleDialogueButton.SetOnClick(() => _screenManager.Show(new ExampleDialogueParameters()));
        _showAnotherExampleDialogueButton.SetOnClick(() => _screenManager.Show(new AnotherExampleDialogueParameters()));
    }

    private void Toggle<TData>(TData data)
    {
        if (_screenManager.IsActive<TData>())
        {
            _screenManager.Hide<TData>();
            return;
        }

        _screenManager.Show(data);
    }
    }
}
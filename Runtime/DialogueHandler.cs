using System;
using System.Collections.Generic;
using Samurai.Showcase.Runtime.Layers;
using Samurai.Showcase.Runtime.Screens;
using UnityEngine;

namespace Samurai.Showcase.Runtime
{
    public class DialogueHandler : IScreenHandler
    {
        #region Inner Types

        private struct DialogueData
        {
            public string LayerId;
            public IDialogue Dialogue;

            public DialogueData(string layerId, IDialogue dialogue)
            {
                LayerId = layerId;
                Dialogue = dialogue;
            }
        }

        #endregion Inner Types
        
        private readonly Dictionary<string, IDialogue> _activeDialogues = new();
        private readonly Dictionary<Type, DialogueData> _dialogues = new();

        #region Registration

        public void Register(Layer layer)
        {
            string id = layer.LayerId;
            foreach (var screen in layer.Screens)
            {
                if (_dialogues.ContainsKey(screen.DataType))
                {
                    Debug.LogWarning($"Multiple dialogues found for data type '{screen.DataType.FullName}'. Skipping..");
                    continue;
                }
                
                if (screen is not IDialogue dialogue)
                {
                    continue;
                }
                
                _dialogues[screen.DataType] = new DialogueData(id, dialogue);
                if (!dialogue.IsActive)
                {
                    continue;
                }

                if (_activeDialogues.ContainsKey(id))
                {
                    dialogue.Hide();
                    continue;
                }

                _activeDialogues[id] = dialogue;
            }
        }

        public void Unregister(Layer layer)
        {
            foreach (var screen in layer.Screens)
            {
                if (screen is IDialogue)
                {
                    _dialogues.Remove(screen.DataType);
                }
            }
        }

        #endregion Registration

        #region Screens Lifecycle

        public void Show<TData>(TData parameters)
        {
            var dialogue = Show<TData>();
            if (dialogue != null)
            {
                dialogue.Init(parameters);
            }
        }
        
        public Screen<TData> Show<TData>()
        {
            if (!TryGet<TData>(out var dialogueData, out var dialogue))
            {
                return null;
            }
            
            if (_activeDialogues.TryGetValue(dialogueData.LayerId, out var activeDialogue))
            {
                activeDialogue.Hide();
            }

            _activeDialogues[dialogueData.LayerId] = dialogue;
            dialogue.Show();
            return dialogue;
        }

        public void Hide(Type type)
        {
            if (_dialogues.TryGetValue(type, out var dialogueData))
            {
                _activeDialogues.Remove(dialogueData.LayerId);
                dialogueData.Dialogue.Hide();
            }
        }

        public void Hide<TData>()
        {
            if (!TryGet<TData>(out var dialogueData, out var dialogue))
            {
                return;
            }

            _activeDialogues.Remove(dialogueData.LayerId);
            dialogue.Hide();
        }

        #endregion Screens Lifecycle

        #region Queries

        public bool IsHandled(Type type)
        {
            return _dialogues.ContainsKey(type);
        }
        
        public bool IsHandled<TData>()
        {
            return _dialogues.ContainsKey(typeof(TData));
        }

        public bool IsActive<TData>()
        {
            return TryGet<TData>(out var dialogueData, out var dialogue) && dialogue.IsActive;
        }

        public Screen<TData> Get<TData>()
        {
            return TryGet<TData>(out var dialogueData, out var dialogue) ? dialogue : null;
        }

        public TScreen Get<TScreen, TData>() where TScreen : Screen<TData>
        {
            return TryGet<TData>(out var dialogueData, out var dialogue) ? dialogue as TScreen : null;
        }

        #endregion Queries

        #region Private

        private bool TryGet<TData>(out DialogueData dialogueData, out Dialogue<TData> dialogue)
        {
            dialogue = null;
            var dataType = typeof(TData);
            if (!_dialogues.TryGetValue(dataType, out dialogueData))
            {
                Debug.LogError($"[ScreenManager] No dialogue found for data type '{dataType.FullName}'.");
                return false;
            }

            dialogue = dialogueData.Dialogue as Dialogue<TData>; 
            if (dialogue == null)
            {
                Debug.LogError($"[ScreenManager] Data type miss-match between dialogue '{dialogueData.Dialogue.GetType().FullName}' and data '{dataType.FullName}'.");
                return false;
            }

            return true;
        }

        #endregion Private
    }
}

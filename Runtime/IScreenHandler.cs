using System;
using Samurai.Showcase.Runtime.Layers;
using Samurai.Showcase.Runtime.Screens;

namespace Samurai.Showcase.Runtime
{
    public interface IScreenHandler
    {
        void Register(Layer layer);
        void Unregister(Layer layer);

        void Show<TData>(TData parameters);
        Screen<TData> Show<TData>();
        void Hide(Type type);
        void Hide<TData>();

        bool IsHandled(Type type);
        bool IsHandled<TData>();
        bool IsActive<TData>();
        Screen<TData> Get<TData>();
        TScreen Get<TScreen, TData>() where TScreen : Screen<TData>;
    }
}
namespace Samurai.Showcase.Runtime.Screens
{
    /// <summary>
    /// Screen type that can have multiple instances active in the same layer at the same time.
    /// </summary>
    /// <typeparam name="TData">Type of initialization parameters.</typeparam>
    public abstract class Window<TData> : Screen<TData>, IWindow
    {
    }
}
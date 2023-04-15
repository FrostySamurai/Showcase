namespace Samurai.Showcase.Runtime.Screens
{
    /// <summary>
    /// Screen type that can only have one instance active in the same layer at any one time. Showing
    /// another dialogue closes the currently open one.
    /// </summary>
    /// <typeparam name="TData">Type of initialization parameters.</typeparam>
    public class Dialogue<TData> : Screen<TData>, IDialogue
    {
    }
}
namespace Samurai.Showcase.Runtime.Screens
{
    public interface IScreen
    {
        bool IsActive { get; }
        void Show();
        void Hide();
    }
}
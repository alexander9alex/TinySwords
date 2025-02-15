using Code.UI.Data;

namespace Code.UI.Windows.Services
{
  public interface IWindowService
  {
    TWindow OpenWindow<TWindow>(WindowId windowId) where TWindow : BaseWindow;
    void CloseWindow(WindowId windowId);
  }
}

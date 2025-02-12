using Code.UI.Data;

namespace Code.UI.Windows.Services
{
  public interface IWindowService
  {
    BaseWindow OpenWindow(WindowId windowId);
    void CloseWindow(WindowId windowId);
  }
}

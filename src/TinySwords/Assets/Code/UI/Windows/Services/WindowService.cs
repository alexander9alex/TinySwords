using System.Collections.Generic;
using Code.UI.Data;
using Code.UI.Windows.Factory;
using UnityEngine;

namespace Code.UI.Windows.Services
{
  public class WindowService : IWindowService
  {
    private readonly IWindowFactory _windowFactory;
    private readonly List<BaseWindow> _spawnedWindows = new();

    public WindowService(IWindowFactory windowFactory) =>
      _windowFactory = windowFactory;

    public TWindow OpenWindow<TWindow>(WindowId windowId) where TWindow : BaseWindow
    {
      TWindow window = _windowFactory.CreateWindow<TWindow>(windowId);
      _spawnedWindows.Add(window);
      return window;
    }

    public void CloseWindow(WindowId windowId)
    {
      BaseWindow window = _spawnedWindows.Find(x => x.WindowId == windowId);
      _spawnedWindows.Remove(window);
      Object.Destroy(window.gameObject);
    }

    public void Cleanup()
    {
      foreach (BaseWindow window in _spawnedWindows)
        CloseWindow(window.WindowId);

      _spawnedWindows.Clear();
    }
  }
}

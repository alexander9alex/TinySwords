using Code.UI.Data;
using UnityEngine;

namespace Code.UI.Windows.Factory
{
  public interface IWindowFactory
  {
    TWindow CreateWindow<TWindow>(WindowId windowId) where TWindow : BaseWindow;
    void SetUIParent(RectTransform uiParent);
  }
}

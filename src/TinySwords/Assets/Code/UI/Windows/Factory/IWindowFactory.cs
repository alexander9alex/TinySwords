using System;
using Code.UI.Data;
using UnityEngine;

namespace Code.UI.Windows.Factory
{
  public interface IWindowFactory
  {
    BaseWindow CreateWindow(WindowId windowId);
    void SetUIParent(RectTransform uiParent);
  }
}

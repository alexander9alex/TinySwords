using Code.Gameplay.Common.Services;
using Code.UI.Data;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows.Factory
{
  public class WindowFactory : IWindowFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly IInstantiator _instantiator;
    private RectTransform _uiParent;

    public WindowFactory(IInstantiator instantiator, IStaticDataService staticData)
    {
      _instantiator = instantiator;
      _staticData = staticData;
    }

    public void SetUIParent(RectTransform uiParent) =>
      _uiParent = uiParent;

    public TWindow CreateWindow<TWindow>(WindowId windowId) where TWindow : BaseWindow =>
      _instantiator.InstantiatePrefabForComponent<TWindow>(PrefabFor(windowId), _uiParent);

    private GameObject PrefabFor(WindowId windowId) =>
      _staticData.GetWindowPrefab(windowId);
  }
}

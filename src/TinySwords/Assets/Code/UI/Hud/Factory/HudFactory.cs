using Code.UI.Buttons.Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Hud.Factory
{
  class HudFactory : IHudFactory
  {
    private readonly IInstantiator _instantiator;

    public HudFactory(IInstantiator instantiator) =>
      _instantiator = instantiator;

    public Button CreateControlButton(ControlButtonConfig controlButtonConfig, RectTransform parent) =>
      _instantiator.InstantiatePrefabForComponent<Button>(controlButtonConfig.ControlButtonPrefab, parent);

    public GameObject CreateActionDescription(GameObject prefab, RectTransform parent) =>
      _instantiator.InstantiatePrefab(prefab, parent);
  }
}

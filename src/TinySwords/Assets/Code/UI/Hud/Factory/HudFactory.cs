using Code.Gameplay.Features.ControlAction.Configs;
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

    public Button CreateControlButton(UnitActionUIConfig unitActionUIConfig, RectTransform parent) =>
      _instantiator.InstantiatePrefabForComponent<Button>(unitActionUIConfig.UnitActionButtonPrefab, parent);

    public GameObject CreateActionDescription(GameObject prefab, RectTransform parent) =>
      _instantiator.InstantiatePrefab(prefab, parent);
  }
}

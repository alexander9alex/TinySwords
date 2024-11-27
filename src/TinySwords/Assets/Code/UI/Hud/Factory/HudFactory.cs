using Code.Gameplay.Features.Command.Configs;
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

    public Button CreateControlButton(CommandUIConfig commandUIConfig, RectTransform parent) =>
      _instantiator.InstantiatePrefabForComponent<Button>(commandUIConfig.CommandButtonPrefab, parent);

    public GameObject CreateActionDescription(GameObject prefab, RectTransform parent) =>
      _instantiator.InstantiatePrefab(prefab, parent);
  }
}

using System.Collections.Generic;
using Code.Gameplay.Features.ControlAction.Configs;
using Code.UI.Hud.Factory;
using Code.UI.Hud.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Hud
{
  public class HUD : MonoBehaviour
  {
    public RectTransform ControlButtonsLayout;
    public RectTransform ActionDescriptionLayout;

    private IHudService _hudService;
    private IHudFactory _hudFactory;

    private readonly List<GameObject> _spawnedButtons = new();
    private GameObject _actionDescription;

    [Inject]
    private void Construct(IHudService hudService, IHudFactory hudFactory)
    {
      _hudService = hudService;
      _hudFactory = hudFactory;
    }

    private void Start() =>
      SubscribeUpdates();

    private void OnDestroy() =>
      UnsubscribeUpdates();

    private void SubscribeUpdates()
    {
      _hudService.UpdateHud += RefreshButtons;
      _hudService.UpdateActionDescription += RefreshActionDescription;

      RefreshButtons();
      RefreshActionDescription();
    }

    private void RefreshButtons()
    {
      DestroySpawnedButtons();
      CreateNewButtons();
    }

    private void RefreshActionDescription()
    {
      DestroySpawnedDescription();
      CreateNewDescription();
    }

    private void CreateNewDescription()
    {
      GameObject actionDescriptionPrefab = _hudService.GetActionDescription();

      if (actionDescriptionPrefab == null)
        return;

      _actionDescription = _hudFactory.CreateActionDescription(actionDescriptionPrefab, ActionDescriptionLayout);
    }

    private void DestroySpawnedButtons()
    {
      foreach (GameObject button in _spawnedButtons)
        Destroy(button);

      _spawnedButtons.Clear();
    }

    private void CreateNewButtons()
    {
      List<UnitActionUIConfig> configs = _hudService.GetAvailableUnitActionUIConfigs();

      foreach (UnitActionUIConfig config in configs)
      {
        Button button = _hudFactory.CreateControlButton(config, ControlButtonsLayout);
        _spawnedButtons.Add(button.gameObject);
        button.onClick.AddListener(() => _hudService.ClickedToButton(config.UnitActionTypeId));
      }
    }

    private void UnsubscribeUpdates()
    {
      _hudService.UpdateHud -= RefreshButtons;
      _hudService.UpdateActionDescription -= RefreshActionDescription;
    }

    private void DestroySpawnedDescription() =>
      Destroy(_actionDescription);
  }
}

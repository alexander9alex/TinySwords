using System.Collections.Generic;
using Code.Gameplay.Features.Command.Configs;
using Code.UI.Hud.Factory;
using Code.UI.Hud.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Hud
{
  public class HUD : MonoBehaviour
  {
    public RectTransform CommandLayout;
    public RectTransform CommandDescriptionLayout;

    private IHudService _hudService;
    private IHudFactory _hudFactory;

    private readonly List<GameObject> _spawnedButtons = new();
    private GameObject _spawnedActionDescription;

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
      _hudService.UpdateCommandButtons += RefreshCommandButtons;
      _hudService.UpdateCommandDescription += RefreshCommandDescription;

      RefreshCommandButtons();
      RefreshCommandDescription();
    }

    private void RefreshCommandButtons()
    {
      DestroySpawnedCommandButtons();
      CreateNewCommandButtons();
    }

    private void RefreshCommandDescription()
    {
      DestroySpawnedDescription();
      CreateNewDescription();
    }

    private void CreateNewDescription()
    {
      GameObject actionDescriptionPrefab = _hudService.CommandDescriptionPrefab;

      if (actionDescriptionPrefab == null)
        return;

      _spawnedActionDescription = _hudFactory.CreateActionDescription(actionDescriptionPrefab, CommandDescriptionLayout);
    }

    private void DestroySpawnedCommandButtons()
    {
      foreach (GameObject button in _spawnedButtons)
        Destroy(button);

      _spawnedButtons.Clear();
    }

    private void CreateNewCommandButtons()
    {
      List<CommandUIConfig> configs = _hudService.AvailableCommandUIConfigs;

      foreach (CommandUIConfig config in configs)
      {
        Button button = _hudFactory.CreateControlButton(config, CommandLayout);
        _spawnedButtons.Add(button.gameObject);
        button.onClick.AddListener(() => _hudService.ApplyCommand(config.CommandTypeId));
      }
    }

    private void UnsubscribeUpdates()
    {
      _hudService.UpdateCommandButtons -= RefreshCommandButtons;
      _hudService.UpdateCommandDescription -= RefreshCommandDescription;
    }

    private void DestroySpawnedDescription() =>
      Destroy(_spawnedActionDescription);
  }
}

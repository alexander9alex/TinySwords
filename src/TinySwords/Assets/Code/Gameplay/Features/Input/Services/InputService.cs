using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Input.Data;
using Code.UI.Layouts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Features.Input.Services
{
  public class InputService : IInputService, ITickable
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly InputSystem _inputSystem = new();
    private Image _gameZoneLayout;

    private bool GameInputMapEnabled => _inputSystem.Game.enabled;
    private Vector2 _mousePos;
    private Vector2 _actionStartedPos;

    public InputService(ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;

      _inputSystem.Game.Action.started += OnActionStarted;
      _inputSystem.Game.Action.canceled += OnActionEnded;

      _inputSystem.Game.FastInteraction.canceled += OnFastInteracted;

      _inputSystem.Game.MousePosition.started += ChangeMousePosition;
      _inputSystem.Game.MousePosition.performed += ChangeMousePosition;
      _inputSystem.Game.MousePosition.canceled += ChangeMousePosition;

      ChangeInputMap(InputMap.UI);
    }

    public void Tick()
    {
      if (GameInputMapEnabled)
        CreateMousePositionInput();
    }

    public void SetGameZoneButton(Image gameZoneLayout) =>
      _gameZoneLayout = gameZoneLayout;

    public void ChangeInputMap(InputMap inputMap)
    {
      switch (inputMap)
      {
        case InputMap.UI:
          _inputSystem.Disable();
          _inputSystem.UI.Enable();
          break;
        case InputMap.Game:
          _inputSystem.Disable();
          _inputSystem.Game.Enable();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(inputMap), inputMap, null);
      }
    }

    private void CreateMousePositionInput()
    {
      CreateEntity.Empty()
        .AddMousePositionOnScreen(_mousePos);
    }

    private void OnFastInteracted(InputAction.CallbackContext context)
    {
      if (!ClickInGameZone(_mousePos))
        return;

      CreateEntity.Empty()
        .With(x => x.isFastInteraction = true)
        .AddPositionOnScreen(_mousePos);
    }

    private void OnActionStarted(InputAction.CallbackContext context)
    {
      _actionStartedPos = _mousePos;
      
      if (!ClickInGameZone(_mousePos))
        return;
      
      CreateEntity.Empty()
        .With(x => x.isActionStarted = true)
        .AddPositionOnScreen(_mousePos);
    }

    private void OnActionEnded(InputAction.CallbackContext context)
    {
      if (!ClickInGameZone(_actionStartedPos))
        return;
      
      CreateEntity.Empty()
        .With(x => x.isActionEnded = true)
        .AddPositionOnScreen(_mousePos);
    }

    private void ChangeMousePosition(InputAction.CallbackContext context) =>
      _mousePos = context.ReadValue<Vector2>();

    private bool ClickInGameZone(Vector2 mousePos)
    {
      PointerEventData eventData = new(EventSystem.current);
      eventData.position = mousePos;
      List<RaycastResult> results = new();
      EventSystem.current.RaycastAll(eventData, results);

      if (results[0].gameObject.layer == GameConstants.UILayer)
        return results[0].gameObject.GetComponent<GameZoneLayout>() != null;
      
      return false;
    }
  }
}

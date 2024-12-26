using System;
using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Input.Data;
using Code.UI.Layouts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Gameplay.Features.Input.Services
{
  public class InputService : IInputService, ITickable
  {
    private readonly InputSystem _inputSystem = new();

    private bool GameInputMapEnabled => _inputSystem.Game.enabled;
    private Vector2 _mousePos;
    private Vector2 _actionStartedPos;
    private Vector2 _cameraMoveDir;
    private bool _actionStarted;

    public InputService()
    {
      InitGameInputMap();
      InitCommandInputMap();

      ChangeInputMap(InputMap.UI);
    }

    public void Tick()
    {
      if (GameInputMapEnabled)
        CreateMousePositionInput();

      CreateCameraMoveInput();
    }

    private void InitGameInputMap()
    {
      _inputSystem.Game.Action.started += OnActionStarted;
      _inputSystem.Game.Action.canceled += OnActionEnded;

      _inputSystem.Game.FastInteraction.canceled += OnFastInteracted;

      _inputSystem.Game.MousePosition.started += ChangeMousePosition;
      _inputSystem.Game.MousePosition.performed += ChangeMousePosition;
      _inputSystem.Game.MousePosition.canceled += ChangeMousePosition;

      _inputSystem.Game.CameraMovement.started += MoveCamera;
      _inputSystem.Game.CameraMovement.performed += MoveCamera;
      _inputSystem.Game.CameraMovement.canceled += MoveCamera;

      _inputSystem.Game.CameraScaling.performed += ScaleCamera;
    }
    
    private void InitCommandInputMap()
    {
      _inputSystem.CommandIsActive.ApplyCommand.canceled += ApplyCommand;

      _inputSystem.CommandIsActive.CancelCommand.canceled += CancelCommand;

      _inputSystem.CommandIsActive.MousePosition.started += ChangeMousePosition;
      _inputSystem.CommandIsActive.MousePosition.performed += ChangeMousePosition;
      _inputSystem.CommandIsActive.MousePosition.canceled += ChangeMousePosition;

      _inputSystem.CommandIsActive.CameraMovement.started += MoveCamera;
      _inputSystem.CommandIsActive.CameraMovement.performed += MoveCamera;
      _inputSystem.CommandIsActive.CameraMovement.canceled += MoveCamera;
      
      _inputSystem.CommandIsActive.CameraScaling.performed += ScaleCamera;
    }

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
        case InputMap.CommandIsActive:
          _inputSystem.Disable();
          _inputSystem.CommandIsActive.Enable();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(inputMap), inputMap, null);
      }
    }

    private void CreateMousePositionInput()
    {
      CreateEntity.Empty()
        .AddMouseScreenPosition(_mousePos);
    }

    private void CreateCameraMoveInput()
    {
      if (_actionStarted)
        return;

      if (_cameraMoveDir == Vector2.zero)
        return;

      CreateEntity.Empty()
        .With(x => x.isMoveCamera = true)
        .AddMoveDirection(_cameraMoveDir);
    }

    private void OnFastInteracted(InputAction.CallbackContext context)
    {
      if (_actionStarted)
        return;

      if (!ClickInGameZone(_mousePos))
        return;

      CreateEntity.Empty()
        .With(x => x.isFastInteraction = true)
        .AddScreenPosition(_mousePos);
    }

    private void OnActionStarted(InputAction.CallbackContext context)
    {
      if (_actionStarted)
        return;

      _actionStartedPos = _mousePos;

      if (!ClickInGameZone(_actionStartedPos))
        return;

      _actionStarted = true;

      CreateEntity.Empty()
        .With(x => x.isActionStarted = true)
        .AddScreenPosition(_mousePos);
    }

    private void OnActionEnded(InputAction.CallbackContext context)
    {
      if (!_actionStarted)
        return;

      if (!ClickInGameZone(_actionStartedPos))
        return;

      _actionStarted = false;

      CreateEntity.Empty()
        .With(x => x.isActionEnded = true)
        .AddScreenPosition(_mousePos);
    }

    private void MoveCamera(InputAction.CallbackContext context) =>
      _cameraMoveDir = context.ReadValue<Vector2>();
    
    private void ScaleCamera(InputAction.CallbackContext context)
    {
      float scaling = context.ReadValue<float>();

      CreateEntity.Empty()
        .AddScaleCamera(-scaling);
    }


    private void ApplyCommand(InputAction.CallbackContext context)
    {
      if (!ClickInGameZone(_mousePos))
        return;

      CreateEntity.Empty()
        .With(x => x.isApplyCommand = true)
        .AddScreenPosition(_mousePos);
    }

    private void CancelCommand(InputAction.CallbackContext context)
    {
      CreateEntity.Empty()
        .With(x => x.isCancelCommand = true)
        .AddScreenPosition(_mousePos);
    }

    private void ChangeMousePosition(InputAction.CallbackContext context) =>
      _mousePos = context.ReadValue<Vector2>();

    private bool ClickInGameZone(Vector2 mousePos)
    {
      PointerEventData eventData = new(EventSystem.current);
      eventData.position = mousePos;
      List<RaycastResult> results = new();
      EventSystem.current.RaycastAll(eventData, results);

      foreach (RaycastResult result in results)
      {
        if (result.gameObject.layer == GameConstants.UILayer)
          return result.gameObject.GetComponent<GameZoneLayout>() != null;
      }

      return false;
    }
  }
}

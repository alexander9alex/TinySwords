using System;
using System.Collections.Generic;
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

    private Vector2 _mousePos;
    private Vector2 _cameraMoveDir;
    private GameEntity _inputEntity;
    private bool _interactionStarted;
    private bool _inputStarted;

    public InputService()
    {
      InitGameInputMap();
      InitCommandInputMap();

      ChangeInputMap(InputMap.UI);
    }

    public void Tick()
    {
      if (!_inputStarted)
        return;

      SetMousePositionInput();
      SetCameraMoveInput();
    }

    public void Cleanup()
    {
      _inputEntity = null;
      _inputStarted = false;
    }

    public void SetInputEntity(GameEntity input) =>
      _inputEntity = input;

    public void StartInput() =>
      _inputStarted = true;

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

    public bool PositionInGameZone(Vector2 pos)
    {
      PointerEventData eventData = new(EventSystem.current);
      eventData.position = pos;
      List<RaycastResult> results = new();
      EventSystem.current.RaycastAll(eventData, results);

      foreach (RaycastResult result in results)
      {
        if (result.gameObject.layer == GameConstants.UILayer)
          return result.gameObject.GetComponent<GameZoneLayout>() != null;
      }

      return false;
    }

    private void InitGameInputMap()
    {
      _inputSystem.Game.Interaction.started += OnInteractionStarted;
      _inputSystem.Game.Interaction.canceled += OnInteractionEnded;

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

    private void SetMousePositionInput()
    {
      _inputEntity
        .ReplaceMousePosition(_mousePos);
    }

    private void SetCameraMoveInput()
    {
      if (_cameraMoveDir == Vector2.zero)
        return;

      _inputEntity
        .ReplaceMoveCameraDirection(_cameraMoveDir);
    }

    private void OnFastInteracted(InputAction.CallbackContext context)
    {
      if (_interactionStarted)
        return;

      if (!PositionInGameZone(_mousePos))
        return;

      _inputEntity
        .With(x => x.isFastInteractionInput = true);
    }

    private void OnInteractionStarted(InputAction.CallbackContext context)
    {
      if (_interactionStarted)
        return;

      if (!PositionInGameZone(_mousePos))
        return;

      _interactionStarted = true;

      _inputEntity
        .With(x => x.isInteractionStartInput = true);
    }

    private void OnInteractionEnded(InputAction.CallbackContext context)
    {
      if (!_interactionStarted)
        return;

      _interactionStarted = false;

      _inputEntity
        .With(x => x.isInteractionEndInput = true);
    }

    private void MoveCamera(InputAction.CallbackContext context) =>
      _cameraMoveDir = context.ReadValue<Vector2>();

    private void ScaleCamera(InputAction.CallbackContext context)
    {
      float scaling = context.ReadValue<float>();

      _inputEntity
        .ReplaceScaleCamera(-scaling);
    }

    private void ApplyCommand(InputAction.CallbackContext context)
    {
      if (!PositionInGameZone(_mousePos))
        return;

      _inputEntity
        .With(x => x.isApplyCommandInput = true);
    }

    private void CancelCommand(InputAction.CallbackContext context)
    {
      _inputEntity
        .With(x => x.isCancelCommandInput = true);
    }

    private void ChangeMousePosition(InputAction.CallbackContext context) =>
      _mousePos = context.ReadValue<Vector2>();
  }
}

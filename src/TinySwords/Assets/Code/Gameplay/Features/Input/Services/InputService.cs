using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Gameplay.Features.Input.Services
{
  public class InputService : IInputService, ITickable
  {
    private readonly InputSystem _inputSystem = new();
    private bool GameInputMapEnabled => _inputSystem.Game.enabled;
    private Vector2 _mousePos;

    public InputService()
    {
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
      CreateEntity.Empty()
        .With(x => x.isFastInteraction = true)
        .AddPositionOnScreen(_mousePos);
    }

    private void OnActionStarted(InputAction.CallbackContext context)
    {
      CreateEntity.Empty()
        .With(x => x.isActionStarted = true)
        .AddPositionOnScreen(_mousePos);
    }
    private void OnActionEnded(InputAction.CallbackContext context)
    {
      CreateEntity.Empty()
        .With(x => x.isActionEnded = true)
        .AddPositionOnScreen(_mousePos);
    }
    private void ChangeMousePosition(InputAction.CallbackContext context) =>
      _mousePos = context.ReadValue<Vector2>();
  }
}

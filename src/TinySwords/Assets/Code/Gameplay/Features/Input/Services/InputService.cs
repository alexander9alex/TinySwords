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
      _inputSystem.Game.LeftClick.started += LeftClickStarted;
      _inputSystem.Game.LeftClick.canceled += LeftClickCanceled;

      _inputSystem.Game.RightClick.canceled += RightClickCanceled;
      
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
        .AddMousePosition(_mousePos)
        .With(x => x.isMousePositionInput = true);
    }

    private void RightClickCanceled(InputAction.CallbackContext context)
    {
      CreateEntity.Empty()
        .With(x => x.isRightClick = true)
        .AddMousePosition(_mousePos);
    }

    private void LeftClickStarted(InputAction.CallbackContext context)
    {
      CreateEntity.Empty()
        .With(x => x.isLeftClickStarted = true)
        .AddMousePosition(_mousePos);
    }
    private void LeftClickCanceled(InputAction.CallbackContext context)
    {
      CreateEntity.Empty()
        .With(x => x.isLeftClickEnded = true)
        .AddMousePosition(_mousePos);
    }
    private void ChangeMousePosition(InputAction.CallbackContext context) =>
      _mousePos = context.ReadValue<Vector2>();
  }
}

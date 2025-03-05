using System;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.FogOfWar.Services;
using Code.Gameplay.Features.Input.Data;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Move.Behaviours;
using Code.Gameplay.Features.ProcessCommand.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.UI.Hud.Service;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Services
{
  class CommandService : ICommandService
  {
    private const float RaycastDistance = 1;
    private readonly int _mapLayerMask = LayerMask.GetMask("Map");

    private readonly IHudService _hudService;
    private readonly IInputService _inputService;
    private readonly ISoundService _soundService;
    private readonly SelectableCommandService _selectableCommandService;
    private readonly IProcessCommandService _processCommandService;
    private readonly IFogOfWarService _fogOfWarService;
    private readonly ICameraProvider _cameraProvider;
    
    public CommandService(IHudService hudService, IInputService inputService, ISoundService soundService, IProcessCommandService processCommandService,
      IFogOfWarService fogOfWarService, ICameraProvider cameraProvider)
    {
      _hudService = hudService;
      _inputService = inputService;
      _soundService = soundService;
      _processCommandService = processCommandService;
      _fogOfWarService = fogOfWarService;
      _cameraProvider = cameraProvider;
    }

    public void SelectCommand(CommandTypeId command)
    {
      _hudService.SelectCommand(command);
      _inputService.ChangeInputMap(InputMap.CommandIsActive);
      _soundService.PlaySound(SoundId.SelectCommand);
    }

    public void CancelCommand(bool isCommandProcessed)
    {
      _hudService.CancelCommand();
      _inputService.ChangeInputMap(InputMap.Game);

      CreateEntity.Empty()
        .With(x => x.isUpdateHudControlButtons = true);

      if (!isCommandProcessed)
        _soundService.PlaySound(SoundId.CancelCommand);
    }

    public bool CanApplyCommand(CommandTypeId command, Vector2 screenPos)
    {
      switch (command)
      {
        case CommandTypeId.Move:
        case CommandTypeId.MoveWithAttack:
          return CanApplyMoveCommand(screenPos);
        case CommandTypeId.AimedAttack:
          return CanApplyAimedAttackCommand(screenPos);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void ApplyCommand(CommandTypeId command, Vector2 screenPos)
    {
      GameEntity entity = CreateEntity.Empty()
        .AddCommandTypeId(command)
        .AddScreenPosition(screenPos)
        .With(x => x.isProcessCommandRequest = true);

      SetCommandTypeId(entity, command);

      _soundService.PlaySound(SoundId.ApplyCommand);
    }

    public void ProcessIncorrectCommand(CommandTypeId commandTypeId, Vector2 screenPos)
    {
      GameEntity entity = CreateEntity.Empty()
        .AddCommandTypeId(commandTypeId)
        .AddScreenPosition(screenPos)
        .With(x => x.isProcessIncorrectCommandRequest = true);

      SetCommandTypeId(entity, commandTypeId);

      _soundService.PlaySound(SoundId.IncorrectCommand);
    }

    private static void SetCommandTypeId(GameEntity entity, CommandTypeId commandTypeId)
    {
      switch (commandTypeId)
      {
        case CommandTypeId.Move:
          entity.isMoveCommand = true;
          break;
        case CommandTypeId.MoveWithAttack:
          entity.isMoveWithAttackCommand = true;
          break;
        case CommandTypeId.AimedAttack:
          entity.isAimedAttackCommand = true;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool CanApplyMoveCommand(Vector2 screenPos)
    {
      RaycastHit2D[] hits = GetHits(screenPos);
      
      if (CanNotMove(hits))
        return false;

      if (CanMove(hits))
        return true;

      return false;
    }

    private RaycastHit2D[] GetHits(Vector2 screenPos)
    {
      Vector3 worldPos = _cameraProvider.ScreenToWorldPoint(screenPos);

      ContactFilter2D contactFilter2D = new();
      contactFilter2D.layerMask = _mapLayerMask;

      RaycastHit2D[] hits = new RaycastHit2D[10];
      Physics2D.Raycast(worldPos, Vector2.zero, contactFilter2D, hits, RaycastDistance);

      return hits;
    }

    private bool CanApplyAimedAttackCommand(Vector2 screenPos)
    {
      if (!_fogOfWarService.IsPositionVisible(_cameraProvider.ScreenToWorldPoint(screenPos)))
        return false;

      return _processCommandService.CanProcessAimedAttack(screenPos);
    }

    private static bool CanMove(RaycastHit2D[] hits) =>
      hits.Length > 0 && hits.Where(hit => hit.collider != null).Select(hit => hit.collider).Any(x => x.GetComponent<MovablePlace>());

    private static bool CanNotMove(RaycastHit2D[] hits) =>
      hits.Length > 0 && hits.Where(hit => hit.collider != null).Select(hit => hit.collider).Any(x => x.GetComponent<NotMovablePlace>());
  }
}

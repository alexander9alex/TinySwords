﻿using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Command.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.FastInteractions.Systems
{
  public class AimedAttackFastInteractionSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;

    private readonly IGroup<GameEntity> _fastInteractions;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public AimedAttackFastInteractionSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _fastInteractions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.FastInteraction,
          GameMatcher.ScreenPosition
        )
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity fastInteraction in _fastInteractions.GetEntities(_buffer))
      {
        if (CanNotMakeAimedAttack(fastInteraction.ScreenPosition))
          continue;

        _commandService.ApplyCommand(CommandTypeId.AimedAttack, fastInteraction.ScreenPosition);

        fastInteraction.isProcessed = true;
      }
    }

    private bool AllSelectedCanAttack()
    {
      foreach (GameEntity selected in _selected)
      {
        if (!selected.isCanAttack)
          return false;
      }

      return true;
    }

    private bool CanNotMakeAimedAttack(Vector2 screenPos) =>
      !CanMakeAimedAttack(screenPos);

    private bool CanMakeAimedAttack(Vector2 screenPos) =>
      _commandService.CanApplyCommand(CommandTypeId.AimedAttack, screenPos) && SelectedCanAttack();

    private bool SelectedCanAttack() =>
      HasSelected() && AllSelectedCanAttack();

    private bool HasSelected() =>
      _selected.count > 0;
  }
}

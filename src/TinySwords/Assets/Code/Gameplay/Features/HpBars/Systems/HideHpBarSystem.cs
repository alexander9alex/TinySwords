﻿using Entitas;

namespace Code.Gameplay.Features.HpBars.Systems
{
  public class HideHpBarSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _hpBars;

    public HideHpBarSystem(GameContext game)
    {
      _hpBars = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.HpBar, GameMatcher.Unfocused));
    }

    public void Execute()
    {
      foreach (GameEntity hpBar in _hpBars)
      {
        hpBar.HpBar.Hide();
      }
    }
  }
}

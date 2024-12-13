using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.ProcessCommand.Systems
{
  public class OffsetPositionByLegsSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public OffsetPositionByLegsSystem(GameContext game)
    {
      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.OffsetPositionByLegs,
          GameMatcher.UserCommand,
          GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        if (entity.hasLegsPositionOffset)
          entity.UserCommand.WorldPosition -= entity.LegsPositionOffset;

        entity.isOffsetPositionByLegs = false;
      }
    }
  }
}

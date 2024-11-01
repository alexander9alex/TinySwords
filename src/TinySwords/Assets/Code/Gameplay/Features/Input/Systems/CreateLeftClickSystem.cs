using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateLeftClickSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _clickStarted;
    private readonly IGroup<GameEntity> _clickEnded;

    public CreateLeftClickSystem(GameContext game)
    {
      _clickStarted = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.LeftClickStarted,
          GameMatcher.MousePosition));
      
      _clickEnded = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.LeftClickEnded,
          GameMatcher.MousePosition));
    }

    public void Execute()
    {
      foreach (GameEntity ended in _clickEnded)
      foreach (GameEntity started in _clickStarted)
      {
        if (Vector2.Distance(started.MousePosition, ended.MousePosition) < float.Epsilon)
        {
          CreateEntity.Empty()
            .With(x => x.isLeftClick = true);
        }
      }
    }
  }
}

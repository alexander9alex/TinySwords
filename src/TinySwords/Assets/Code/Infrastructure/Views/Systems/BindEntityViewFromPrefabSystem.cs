using System.Collections.Generic;
using Code.Infrastructure.Views.Factory;
using Entitas;
using UnityEngine;

namespace Code.Infrastructure.Views.Systems
{
  public class BindEntityViewFromPrefabSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly IEntityViewFactory _entityViewFactory;
    private readonly List<GameEntity> _buffer = new(64);

    public BindEntityViewFromPrefabSystem(GameContext game, IEntityViewFactory entityViewFactory)
    {
      _entityViewFactory = entityViewFactory;
      
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ViewPrefab)
        .NoneOf(GameMatcher.View));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        _entityViewFactory.CreateViewFromPrefab(entity);
      }
    }
  }
}

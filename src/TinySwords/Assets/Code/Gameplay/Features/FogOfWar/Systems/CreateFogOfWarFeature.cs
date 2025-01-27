using Code.Gameplay.Features.FogOfWar.Factory;
using Entitas;

namespace Code.Gameplay.Features.FogOfWar.Systems
{
  public class CreateFogOfWarFeature : IExecuteSystem
  {
    private readonly IFogOfWarFactory _fogOfWarFactory;

    private readonly IGroup<GameEntity> _createFogOfWarRequests;
    private readonly IGroup<GameEntity> _levelParents;

    public CreateFogOfWarFeature(GameContext game, IFogOfWarFactory fogOfWarFactory)
    {
      _fogOfWarFactory = fogOfWarFactory;

      _createFogOfWarRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateFogOfWar, GameMatcher.FogOfWarMarker));

      _levelParents = game.GetGroup(GameMatcher.LevelParent);
    }

    public void Execute()
    {
      foreach (GameEntity request in _createFogOfWarRequests)
      foreach (GameEntity levelParent in _levelParents)
      {
        _fogOfWarFactory.CreateFogOfWar(request.FogOfWarMarker, levelParent.Transform);

        request.isDestructed = true;
      }
    }
  }
}

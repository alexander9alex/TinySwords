using Code.Gameplay.Features.FogOfWar.Services;
using Entitas;

namespace Code.Gameplay.Features.FogOfWar.Systems
{
  public class SetGlowingObjectsSystem : IExecuteSystem
  {
    private readonly IFogOfWarService _fogOfWarService;
    private readonly IGroup<GameEntity> _glowingEntities;

    public SetGlowingObjectsSystem(GameContext game, IFogOfWarService fogOfWarService)
    {
      _fogOfWarService = fogOfWarService;
      
      _glowingEntities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Glowing,
          GameMatcher.WorldPosition,
          GameMatcher.Alive
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _glowingEntities)
      {
        _fogOfWarService.UpdateGlowingObjectPosition(entity.WorldPosition);
      }
    }
  }
}

using static Code.Gameplay.Constants.GameConstants;

namespace Code.Gameplay.Features.CollectEntities.Services
{
  public class CollectEntityService : ICollectEntityService
  {
    public bool EntityIsUnreachable(float collectRadius, float distanceToEntity) =>
      distanceToEntity > collectRadius;

    public static bool TargetIsNotValid(GameEntity target) =>
      target is not { isAlive: true, hasTeamColor: true, hasId: true };

    public bool EntityIsNotValid(GameEntity entity) =>
      entity is not { isAlive: true, hasTeamColor: true, hasId: true };

    public bool SameTeamColor(GameEntity firstEntity, GameEntity secondEntity) =>
      firstEntity.TeamColor == secondEntity.TeamColor;

    public bool IsAlly(GameEntity entity, GameEntity ally) =>
      AllyTeamColor[entity.TeamColor] == ally.TeamColor;
  }
}

namespace Code.Gameplay.Features.CollectEntities.Services
{
  public interface ICollectEntityService
  {
    bool EntityIsUnreachable(float collectRadius, float distanceToEntity);
    bool EntityIsNotValid(GameEntity entity);
    bool SameTeamColor(GameEntity firstEntity, GameEntity secondEntity);
    bool IsAlly(GameEntity entity, GameEntity ally);
  }
}

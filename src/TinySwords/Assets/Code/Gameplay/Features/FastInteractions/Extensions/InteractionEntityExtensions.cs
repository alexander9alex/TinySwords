namespace Code.Gameplay.Features.FastInteractions.Extensions
{
  public static class InteractionEntityExtensions
  {
    private static GameContext GameContext => Contexts.sharedInstance.game;
    public static GameEntity GetEntity(this int id) =>
      GameContext.GetEntityWithId(id);
  }
}
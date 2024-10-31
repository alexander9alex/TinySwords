namespace Code.Gameplay.Common.Registrars
{
  public abstract class EntityComponentRegistrar : EntityDependent, IEntityComponentRegistrar
  {
    public abstract void RegisterComponents();
    public abstract void UnregisterComponents();
  }
}

using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Registrars;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Views
{
  public class EntityBehaviour : MonoBehaviour, IEntityView
  {
    public GameEntity Entity => _entity;
    private GameEntity _entity;
    
    private ICollisionRegistry _collisionRegistry;

    [Inject]
    private void Construct(ICollisionRegistry collisionRegistry) =>
      _collisionRegistry = collisionRegistry;

    public void SetEntity(GameEntity entity)
    {
      _entity = entity;
      _entity.AddView(this);
      _entity.Retain(this);

      foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
        registrar.RegisterComponents();

      foreach (Collider2D collider in GetComponentsInChildren<Collider2D>(includeInactive: true))
        _collisionRegistry.Register(collider.GetInstanceID(), _entity);
    }

    public void ReleaseEntity()
    {
      foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
        registrar.UnregisterComponents();

      foreach (Collider2D collider in GetComponentsInChildren<Collider2D>(includeInactive: true))
        _collisionRegistry.Unregister(collider.GetInstanceID());
      
      _entity.Release(this);
      _entity = null;
    }
  }
}

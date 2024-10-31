using Code.Gameplay.Common.Registrars;
using UnityEngine;

namespace Code.Common.Views
{
  public class EntityBehaviour : MonoBehaviour, IEntityView
  {
    public GameEntity Entity => _entity;
    private GameEntity _entity;

    public void SetEntity(GameEntity entity)
    {
      _entity = entity;
      _entity.AddView(this);
      _entity.Retain(this);

      foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
        registrar.RegisterComponents();
    }

    public void ReleaseEntity()
    {
      foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
        registrar.RegisterComponents();

      _entity.Release(this);
      _entity = null;
    }
  }
}

using UnityEngine;

namespace Code.Infrastructure.Views.Factory
{
  public interface IEntityViewFactory
  {
    EntityBehaviour CreateViewFromPrefab(GameEntity entity);
    void SetEntityViewParent(Transform entityViewParent);
  }
}

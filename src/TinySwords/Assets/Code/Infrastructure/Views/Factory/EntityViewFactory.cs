using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Views.Factory
{
  public class EntityViewFactory : IEntityViewFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly Vector3 _farAway = new(9999, 9999, 9999);
    private Transform _entityViewParent;

    public EntityViewFactory(IInstantiator instantiator) =>
      _instantiator = instantiator;

    public void SetEntityViewParent(Transform entityViewParent) =>
      _entityViewParent = entityViewParent;

    public EntityBehaviour CreateViewFromPrefab(GameEntity entity)
    {
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(entity.ViewPrefab, _farAway, Quaternion.identity, _entityViewParent);

      view.SetEntity(entity);

      return view;
    }
  }
}

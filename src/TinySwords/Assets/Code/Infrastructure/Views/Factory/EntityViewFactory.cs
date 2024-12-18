using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Views.Factory
{
  public class EntityViewFactory : IEntityViewFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly Vector3 _farAway = new(9999, 9999, 9999);
    private Transform Parent => _parent ??= new GameObject("EntityViewParent").transform;
    private Transform _parent;

    public EntityViewFactory(IInstantiator instantiator) =>
      _instantiator = instantiator;

    public EntityBehaviour CreateViewFromPrefab(GameEntity entity)
    {
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(entity.ViewPrefab, _farAway, Quaternion.identity, Parent);

      view.SetEntity(entity);

      return view;
    }
  }
}

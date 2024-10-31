using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Views.Factory
{
  class EntityViewFactory : IEntityViewFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly Vector3 _farAway = new Vector3(9999, 9999, 9999);

    public EntityViewFactory(IInstantiator instantiator) =>
      _instantiator = instantiator;

    public EntityBehaviour CreateViewFromPrefab(GameEntity entity)
    {
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(entity.ViewPrefab, _farAway, Quaternion.identity, null);
      
      view.SetEntity(entity);
      
      return view;
    }
  }
}

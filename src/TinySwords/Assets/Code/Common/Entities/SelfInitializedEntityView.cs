using Code.Gameplay.Common.Identifiers;
using Code.Infrastructure.Views;
using UnityEngine;
using Zenject;

namespace Code.Common.Entities
{
  public class SelfInitializedEntityView : MonoBehaviour
  {
    public EntityBehaviour EntityBehaviour;

    private IIdentifierService _identifiers;

    [Inject]
    private void Construct(IIdentifierService identifiers)
    {
      _identifiers = identifiers;

      Initialize();
    }

    private void Initialize()
    {
      GameEntity entity = CreateEntity.Empty()
        .AddId(_identifiers.Next());

      EntityBehaviour.SetEntity(entity);
    }
  }
}

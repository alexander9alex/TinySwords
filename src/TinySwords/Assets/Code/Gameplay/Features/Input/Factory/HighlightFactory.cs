using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Services;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Factory
{
  class HighlightFactory : IHighlightFactory
  {
    private readonly IStaticDataService _staticData;

    public HighlightFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public GameEntity CreateHighlight()
    {
      EntityBehaviour highlightViewPrefab = _staticData.GetHighlightViewPrefab();

      return CreateEntity.Empty()
        .AddViewPrefab(highlightViewPrefab)
        .AddCenterPosition(Vector2.zero)
        .AddSize(Vector2.zero)
        .With(x => x.isHighlight = true);
    }
  }
}

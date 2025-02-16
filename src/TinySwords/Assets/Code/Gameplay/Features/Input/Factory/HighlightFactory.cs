using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Services;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Factory
{
  class HighlightFactory : IHighlightFactory
  {
    private readonly EntityBehaviour _highlightPrefab;

    public HighlightFactory(IStaticDataService staticData) =>
      _highlightPrefab = staticData.GetHighlightPrefab();

    public GameEntity CreateHighlight()
    {
      return CreateEntity.Empty()
        .AddViewPrefab(_highlightPrefab)
        .AddCenterPosition(Vector2.zero)
        .AddSize(Vector2.zero)
        .With(x => x.isHighlight = true);
    }
  }
}

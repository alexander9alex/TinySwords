using Code.Common.Entities;
using Code.Common.Extensions;
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

    public GameEntity CreateHighlight(Vector2 start, Vector2 end)
    {
      EntityBehaviour highlightViewPrefab = _staticData.GetHighlightViewPrefab();

      return CreateEntity.Empty()
        .AddViewPrefab(highlightViewPrefab)
        .AddStartPosition(start)
        .AddEndPosition(end)
        .With(x => x.isHighlight = true);
    }
  }
}

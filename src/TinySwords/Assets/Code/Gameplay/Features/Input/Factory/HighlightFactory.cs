using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Input.Factory
{
  class HighlightFactory : IHighlightFactory
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly EntityBehaviour _highlightPrefab;

    public HighlightFactory(IStaticDataService staticData, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
      _highlightPrefab = staticData.GetHighlightPrefab();
    }

    public GameEntity CreateHighlight(Vector2 startScreenPos, Vector2 endScreenPos)
    {
      Vector3 startWorldPos = _cameraProvider.ScreenToWorldPoint(startScreenPos);
      Vector3 endWorldPos = _cameraProvider.ScreenToWorldPoint(endScreenPos);
      
      return CreateEntity.Empty()
        .AddViewPrefab(_highlightPrefab)
        .AddStartPosition(startWorldPos)
        .AddEndPosition(endWorldPos)
        .With(x => x.isHighlight = true);
    }
  }
}

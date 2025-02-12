using System;
using Code.Gameplay.Tutorials.Data;
using Code.UI.Data;

namespace Code.Gameplay.Tutorials.Extensions
{
  public static class TutorialIdExtensions
  {
    public static WindowId ToWindowId(this TutorialId tutorialId)
    {
      switch (tutorialId)
      {
        case TutorialId.First:
          return WindowId.FirstTutorial;
        default:
          throw new ArgumentOutOfRangeException(nameof(tutorialId), tutorialId, null);
      }
    }
  }
}

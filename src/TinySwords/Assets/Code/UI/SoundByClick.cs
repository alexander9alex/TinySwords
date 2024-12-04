using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using UnityEngine;
using Zenject;

namespace Code.UI
{
  public class SoundByClick : MonoBehaviour
  {
    public SoundId SoundId;
    private ISoundService _soundService;

    [Inject]
    private void Construct(ISoundService soundService) =>
      _soundService = soundService;

    public void PlaySound() =>
      _soundService.PlaySoundDirectly(SoundId);
  }
}

namespace Code.Gameplay.Features.Sounds.Services
{
  public interface ISoundService
  {
    void PlayTakingDamageSound(GameEntity entity);
    void PlayMakeDamageSound(GameEntity entity);
  }
}

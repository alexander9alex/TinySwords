namespace Code.Gameplay.Features.Units.Data
{
  public enum UnitDecisionTypeId
  {
    Unknown = 0,
    
    Stay = 100,
    
    Move = 200,
    MoveToTarget = 201,
    MoveToAllyTarget = 202,
    MoveToAimedTarget = 203,
    
    Attack = 300,
    AttackAimedTarget = 301,
  }
}

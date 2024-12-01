namespace Code.Gameplay.Features.Units.Data
{
  public enum UnitDecisionTypeId
  {
    Unknown = 0,
    
    Stay = 100,
    
    MoveToEndDestination = 200,
    MoveToTarget = 201,
    MoveToAllyTarget = 202,
    
    Attack = 300,
  }
}

using UnityEngine;

namespace Code.Gameplay.Features.Units.Services
{
  public class RecruitUnitService : IRecruitUnitService
  {
    public void RecruitUnit(GameEntity unit)
    {
      Debug.Log("Unit is recruit!");
      
      // придумать, как сделать юнитов дружелюбными, чтоб не сбивались анимации и прочее
      // И как вообще заменить view
      // Или просто удалить его и создать нового юнита
      // Тогда придётся выставлять ему все хп ипрочее
      //
      // Сделать, чтоб в allies collect system добавлялись white -> blue, blue -> white (?)
    }
  }
}

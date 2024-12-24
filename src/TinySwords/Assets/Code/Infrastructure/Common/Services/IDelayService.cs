using System;

namespace Code.Infrastructure.Common.Services
{
  public interface IDelayService
  {
    void MakeActionWithDelay(Action action, float delay);
  }
}

using System;
using System.Collections;
using Code.Infrastructure.Common.CoroutineRunner;
using UnityEngine;

namespace Code.Infrastructure.Common.Services
{
  public class DelayService : IDelayService
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public DelayService(ICoroutineRunner coroutineRunner) =>
      _coroutineRunner = coroutineRunner;

    public void MakeActionWithDelay(Action action, float delay) =>
      _coroutineRunner.StartCoroutine(MakeActionWithDelayCoroutine(action, delay));

    private IEnumerator MakeActionWithDelayCoroutine(Action action, float delay)
    {
      yield return new WaitForSeconds(delay);
      action?.Invoke();
    }
  }
}

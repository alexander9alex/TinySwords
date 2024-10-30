using System.Collections;
using UnityEngine;

namespace Code.Infrastructure.Common.CoroutineRunner
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator enumerator);
  }
}
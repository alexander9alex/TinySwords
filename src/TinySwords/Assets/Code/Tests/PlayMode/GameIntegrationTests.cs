using System.Collections;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

namespace Code.Tests.PlayMode
{
  public class GameIntegrationTests
  {
    [UnityTest]
    public IEnumerator When1FramePassed_ThenDeltaTimeShouldBePositive()
    {
      // Act
      yield return null;

      // Assert
      Time.deltaTime.Should().BePositive();
    }
  }
}

using Code.Tools;
using NUnit.Framework;

namespace Code.Tests.EditMode
{
  public class ValidationTests
  {
    [Test]
    public void ValidationTestsSimplePasses()
    {
      Validator.FindMissingComponents();
    }
  }
}

using System.Reflection;
using NetArchTest.Rules;
using SpeakerMeet.Api.Controllers;
using Xunit;

namespace SpeakerMeet.Architecture.Tests;

public class EntityTests
{
    private static Assembly DomainAssembly => typeof(SpeakersController).Assembly;

    [Fact]
    public void Entities_Should_Not_Be_Referenced_By_Api()
    {
        TestResult? result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("SpeakerMeet.Core.Models.Entities")
            .GetResult();

        Assert.True(result.IsSuccessful, GetFailingTypes(result));
    }

    private static string GetFailingTypes(TestResult result)
    {
        return result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : string.Empty;
    }
}
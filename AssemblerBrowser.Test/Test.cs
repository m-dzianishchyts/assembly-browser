using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using AssemblerBrowser.Core.Entities;
using NUnit.Framework;

namespace AssemblerBrowser.Test;

public class Tests
{
    private readonly Assembly _testAssembly = Assembly.LoadFile(
        "C:\\Users\\maxiemar\\source\\repos\\assembly-browser\\AssemblerBrowser.Test\\AssemblerBrowser.Core.dll");

    private AssemblyInformation? _assemblyInformation;

    [SetUp]
    public void Setup()
    {
        _assemblyInformation = new AssemblyInformation(_testAssembly);
    }

    [Test]
    public void TestNamespacesSuccess()
    {
        var expectedNamespaceNames = new List<string>
        {
            "AssemblerBrowser.Core.Utilities",
            "AssemblerBrowser.Core.Entities"
        };
        const int expectedNamespacesCount = 2;

        Assert.AreEqual(expectedNamespacesCount, _assemblyInformation?.Namespaces.Count());
        foreach (string expectedNamespaceName in expectedNamespaceNames)
            Assert.Contains(expectedNamespaceName, _assemblyInformation?.Namespaces
                .Select(namespaceInfo => namespaceInfo.Name).ToImmutableList());
    }

    [Test]
    public void TestClassesSuccess()
    {
        var expectedUtilityClassNames = new List<string>
        {
            "public CompilerUtilities",
            "public ModifierUtilities",
            "public TypeUtilities"
        };

        var expectedEntityClassNames = new List<string>
        {
            "public AssemblyInformation",
            "public TypeInformation",
            "public FieldInformation",
            "public MethodInformation",
            "public NamespaceInformation",
            "public PropertyInformation"
        };

        var utilityClasses = _assemblyInformation?.Namespaces
            .First(information => information.Name.Equals("AssemblerBrowser.Core.Utilities"))
            .Types;
        var typesInformations = utilityClasses?.ToList();
        Assert.AreEqual(typesInformations?.Count, 3);

        var entityClasses = _assemblyInformation?.Namespaces
            .First(information => information.Name.Equals("AssemblerBrowser.Core.Entities"))
            .Types;
        var typesInformation = entityClasses?.ToList();
        Assert.AreEqual(typesInformation?.Count, 6);

        foreach (string expectedUtilityClassName in expectedUtilityClassNames)
            Assert.Contains(expectedUtilityClassName, typesInformations?
                .Select(information => information.Name).ToImmutableList());

        foreach (string expectedEntityClassName in expectedEntityClassNames)
            Assert.Contains(expectedEntityClassName, typesInformation?
                .Select(information => information.Name).ToImmutableList());
    }

    [Test]
    public void Test_FieldsSuccess()
    {
        var expectedFieldNames = new List<string>
        {
            "private static readonly BindingFlags TypeBindingFlags",
            "public readonly IEnumerable<FieldInformation> Fields",
            "public readonly IEnumerable<MethodInformation> Methods",
            "public readonly IEnumerable<PropertyInformation> Properties"
        };
        const int expectedFieldsCount = 4;

        var typeInformation = _assemblyInformation?.Namespaces
            .First(information => information.Name.Equals("AssemblerBrowser.Core.Entities")).Types
            .First(classInformation => classInformation.Name.Equals("public TypeInformation"));

        Assert.AreEqual(expectedFieldsCount, typeInformation?.Fields.Count());
        foreach (string expectedFieldName in expectedFieldNames)
            Assert.Contains(expectedFieldName, typeInformation?.Fields
                .Select(field => field.Name).ToImmutableList());
    }

    [Test]
    public void Test_PropertiesSuccess()
    {
        var expectedPropertyNames = new List<string>
        {
            "String Name { public get; private set; }"
        };
        const int expectedPropertiesCount = 1;

        var classInformation = _assemblyInformation?.Namespaces
            .First(information => information.Name.Equals("AssemblerBrowser.Core.Entities")).Types
            .First(classInformation => classInformation.Name.Equals("public TypeInformation"));

        Assert.AreEqual(expectedPropertiesCount, classInformation?.Properties.Count());
        foreach (string expectedPropertyName in expectedPropertyNames)
            Assert.Contains(expectedPropertyName, classInformation?.Properties
                .Select(field => field.Name).ToImmutableList());
    }

    [Test]
    public void Test_MethodsSuccess()
    {
        var expectedMethodNames = new List<string>
        {
            "private static IEnumerable<PropertyInformation> GetProperties(IEnumerable<MemberInfo> members)",
            "private static IEnumerable<FieldInformation> GetFields(IEnumerable<MemberInfo> members)",
            "private static IEnumerable<MethodInformation> GetMethods(IEnumerable<MemberInfo> members)"
        };
        const int expectedMethodsCount = 3;

        var classInformation = _assemblyInformation?.Namespaces
            .First(information => information.Name.Equals("AssemblerBrowser.Core.Entities")).Types
            .First(classInformation => classInformation.Name.Equals("public TypeInformation"));

        Assert.AreEqual(expectedMethodsCount, classInformation?.Methods.Count());
        foreach (string expectedMethodName in expectedMethodNames)
            Assert.Contains(expectedMethodName, classInformation?.Methods
                .Select(method => method.Name).ToImmutableList());
    }
}
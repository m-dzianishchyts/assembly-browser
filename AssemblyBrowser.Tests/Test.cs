using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AssemblyBrowser.Core.Entities;
using AssemblyBrowser.TestProject;
using NUnit.Framework;

namespace AssemblyBrowser.Tests;

public class Tests
{
    private const string AbsolutePathToSolution = "C:\\Users\\maxiemar\\source\\repos\\assembly-browser\\";

    private const string RelativePathToAssembly =
        "AssemblyBrowser.TestProject\\bin\\Debug\\net6.0\\AssemblyBrowser.TestProject.dll";

    private const string AbsolutePathToAssembly = AbsolutePathToSolution + RelativePathToAssembly;

    private const string AssemblyName = "AssemblyBrowser.TestProject";

    private readonly Assembly _testAssembly = Assembly.LoadFile(AbsolutePathToAssembly);

    private AssemblyInformation? _assemblyInformation;

    [SetUp]
    public void Setup()
    {
        _assemblyInformation = new AssemblyInformation(_testAssembly);
    }

    [Test]
    public void AssemblyInformation_ContainsAll_Namespaces()
    {
        var expectedNamespaces = new List<string>
        {
            typeof(Fields).Namespace!
        };

        IEnumerable<NamespaceInformation> namespacesInformation = _assemblyInformation!.Namespaces;
        IEnumerable<string> namespaces =
            namespacesInformation.Select(namespaceInformation => namespaceInformation.Name);
        CollectionAssert.AreEquivalent(expectedNamespaces, namespaces);
    }

    [Test]
    public void AssemblyInformation_ContainsAll_Fields()
    {
        var expectedFields = new List<string>
        {
            "public static readonly System.Int32 field0",
            "public static System.Int32 field1",
            "public static const System.Int32 field2",
            "public readonly System.Int32 field3",
            "public System.Int32 field4",
            "private static readonly System.Int32 field5",
            "private static System.Int32 field6",
            "private static const System.Int32 field7",
            "private readonly System.Int32 field8",
            "private System.Int32 field9",
            "protected static readonly System.Int32 field10",
            "protected static System.Int32 field11",
            "protected static const System.Int32 field12",
            "protected readonly System.Int32 field13",
            "protected System.Int32 field14",
            "internal static readonly System.Int32 field15",
            "internal static System.Int32 field16",
            "internal static const System.Int32 field17",
            "internal readonly System.Int32 field18",
            "internal System.Int32 field19",
            "protected internal static readonly System.Int32 field20",
            "protected internal static System.Int32 field21",
            "protected internal static const System.Int32 field22",
            "protected internal readonly System.Int32 field23",
            "protected internal System.Int32 field24",
            "private protected static readonly System.Int32 field25",
            "private protected static System.Int32 field26",
            "private protected static const System.Int32 field27",
            "private protected readonly System.Int32 field28",
            "private protected System.Int32 field29"
        };

        NamespaceInformation targetNamespaceInformation = _assemblyInformation!.Namespaces
            .First(namespaceInformation => namespaceInformation.Name
                       .Equals(typeof(Fields).Namespace)
            );
        TypeInformation classWithFieldsInformation = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(Fields).FullName)
            );

        IEnumerable<FieldInformation> fieldsInformation = classWithFieldsInformation.Fields;
        IEnumerable<string> fields = fieldsInformation.Select(fieldInformation => fieldInformation.Name);

        CollectionAssert.AreEquivalent(expectedFields, fields);
    }

    [Test]
    public void AssemblyInformation_ContainsAll_Methods()
    {
        var expectedMethods = new List<string>
        {
            "public sealed System.Void Method0()",
            "public abstract System.Void Method1()",
            "public virtual System.Void Method2()",
            "public static System.Void Method3()",
            "public System.Void Method4()",
            "private static System.Void Method5()",
            "private System.Void Method6()",
            "protected sealed System.Void Method7()",
            "protected abstract System.Void Method8()",
            "protected virtual System.Void Method9()",
            "protected static System.Void Method10()",
            "protected System.Void Method11()",
            "internal sealed System.Void Method12()",
            "internal abstract System.Void Method13()",
            "internal virtual System.Void Method14()",
            "internal static System.Void Method15()",
            "internal System.Void Method16()",
            "protected internal sealed System.Void Method17()",
            "protected internal abstract System.Void Method18()",
            "protected internal virtual System.Void Method19()",
            "protected internal static System.Void Method20()",
            "protected internal System.Void Method21()",
            "private protected sealed System.Void Method22()",
            "private protected abstract System.Void Method23()",
            "private protected virtual System.Void Method24()",
            "private protected static System.Void Method25()",
            "private protected System.Void Method26()",
            "protected Methods()"
        };

        var expectedMethodsToOverride = new List<string>
        {
            "public virtual System.Void Method0()",
            "protected virtual System.Void Method7()",
            "internal virtual System.Void Method12()",
            "protected internal virtual System.Void Method17()",
            "private protected virtual System.Void Method22()",
            "public MethodsToOverride()"
        };

        NamespaceInformation targetNamespaceInformation = _assemblyInformation!.Namespaces
            .First(namespaceInformation => namespaceInformation.Name
                       .Equals(typeof(Methods).Namespace)
            );
        TypeInformation classWithMethodsInformation = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(Methods).FullName)
            );
        TypeInformation classWithMethodsToOverrideInformation = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(MethodsToOverride).FullName)
            );

        IEnumerable<MethodInformation> methodsInformation = classWithMethodsInformation.Methods;
        IEnumerable<MethodInformation> methodsToOverrideInformation = classWithMethodsToOverrideInformation.Methods;

        IEnumerable<string> methods = methodsInformation.Select(methodInformation => methodInformation.Name);
        IEnumerable<string> methodsToOverride =
            methodsToOverrideInformation.Select(methodInformation => methodInformation.Name);

        CollectionAssert.AreEquivalent(expectedMethods, methods);
        CollectionAssert.AreEquivalent(expectedMethodsToOverride, methodsToOverride);
    }

    [Test]
    public void AssemblyInformation_ContainsAll_Properties()
    {
        var expectedProperties = new List<string>
        {
            "System.Int32 Property0 { public sealed get; public sealed set; }",
            "System.Int32 Property1 { public abstract get; public abstract set; }",
            "System.Int32 Property2 { public virtual get; public virtual set; }",
            "System.Int32 Property3 { public static get; public static set; }",
            "System.Int32 Property4 { public get; public set; }",
            "System.Int32 Property5 { private static get; private static set; }",
            "System.Int32 Property6 { private get; private set; }",
            "System.Int32 Property7 { protected sealed get; protected sealed set; }",
            "System.Int32 Property8 { protected abstract get; protected abstract set; }",
            "System.Int32 Property9 { protected virtual get; protected virtual set; }",
            "System.Int32 Property10 { protected static get; protected static set; }",
            "System.Int32 Property11 { protected get; protected set; }",
            "System.Int32 Property12 { internal sealed get; internal sealed set; }",
            "System.Int32 Property13 { internal abstract get; internal abstract set; }",
            "System.Int32 Property14 { internal virtual get; internal virtual set; }",
            "System.Int32 Property15 { internal static get; internal static set; }",
            "System.Int32 Property16 { internal get; internal set; }",
            "System.Int32 Property17 { protected internal sealed get; protected internal sealed set; }",
            "System.Int32 Property18 { protected internal abstract get; protected internal abstract set; }",
            "System.Int32 Property19 { protected internal virtual get; protected internal virtual set; }",
            "System.Int32 Property20 { protected internal static get; protected internal static set; }",
            "System.Int32 Property21 { protected internal get; protected internal set; }",
            "System.Int32 Property22 { private protected sealed get; private protected sealed set; }",
            "System.Int32 Property23 { private protected abstract get; private protected abstract set; }",
            "System.Int32 Property24 { private protected virtual get; private protected virtual set; }",
            "System.Int32 Property25 { private protected static get; private protected static set; }",
            "System.Int32 Property26 { private protected get; private protected set; }"
        };
        var expectedPropertiesToOverride = new List<string>
        {
            "System.Int32 Property0 { public virtual get; public virtual set; }",
            "System.Int32 Property7 { protected virtual get; protected virtual set; }",
            "System.Int32 Property12 { internal virtual get; internal virtual set; }",
            "System.Int32 Property17 { protected internal virtual get; protected internal virtual set; }",
            "System.Int32 Property22 { private protected virtual get; private protected virtual set; }"
        };

        NamespaceInformation targetNamespaceInformation = _assemblyInformation!.Namespaces
            .First(namespaceInformation => namespaceInformation.Name
                       .Equals(typeof(Properties).Namespace)
            );
        TypeInformation classWithPropertiesInformation = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(Properties).FullName)
            );
        TypeInformation classWithPropertiesToOverrideInformation = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(PropertiesToOverride).FullName)
            );

        IEnumerable<PropertyInformation> propertiesInformation = classWithPropertiesInformation.Properties;
        IEnumerable<PropertyInformation> propertiesToOverrideInformation =
            classWithPropertiesToOverrideInformation.Properties;

        IEnumerable<string> properties = propertiesInformation.Select(propertyInformation => propertyInformation.Name);
        IEnumerable<string> propertiesToOverride =
            propertiesToOverrideInformation.Select(propertyInformation => propertyInformation.Name);

        CollectionAssert.AreEquivalent(expectedProperties, properties);
        CollectionAssert.AreEquivalent(expectedPropertiesToOverride, propertiesToOverride);
    }

    [Test]
    public void AssemblyInformation_ContainsAll_NestedTypes()
    {
        var expectedNestedTypes = new List<string>
        {
            "public sealed class AssemblyBrowser.TestProject.Type0",
            "public abstract class AssemblyBrowser.TestProject.Type1",
            "public static class AssemblyBrowser.TestProject.Type2",
            "public class AssemblyBrowser.TestProject.Type3",
            "private sealed class AssemblyBrowser.TestProject.Type4",
            "private abstract class AssemblyBrowser.TestProject.Type5",
            "private static class AssemblyBrowser.TestProject.Type6",
            "private class AssemblyBrowser.TestProject.Type7",
            "protected sealed class AssemblyBrowser.TestProject.Type8",
            "protected abstract class AssemblyBrowser.TestProject.Type9",
            "protected static class AssemblyBrowser.TestProject.Type10",
            "protected class AssemblyBrowser.TestProject.Type11",
            "internal sealed class AssemblyBrowser.TestProject.Type12",
            "internal abstract class AssemblyBrowser.TestProject.Type13",
            "internal static class AssemblyBrowser.TestProject.Type14",
            "internal class AssemblyBrowser.TestProject.Type15",
            "protected internal sealed class AssemblyBrowser.TestProject.Type16",
            "protected internal abstract class AssemblyBrowser.TestProject.Type17",
            "protected internal static class AssemblyBrowser.TestProject.Type18",
            "protected internal class AssemblyBrowser.TestProject.Type19",
            "private protected sealed class AssemblyBrowser.TestProject.Type20",
            "private protected abstract class AssemblyBrowser.TestProject.Type21",
            "private protected static class AssemblyBrowser.TestProject.Type22",
            "private protected class AssemblyBrowser.TestProject.Type23",
            "public abstract interface AssemblyBrowser.TestProject.Type24",
            "private abstract interface AssemblyBrowser.TestProject.Type25",
            "protected abstract interface AssemblyBrowser.TestProject.Type26",
            "internal abstract interface AssemblyBrowser.TestProject.Type27",
            "protected internal abstract interface AssemblyBrowser.TestProject.Type28",
            "private protected abstract interface AssemblyBrowser.TestProject.Type29",
            "public sealed struct AssemblyBrowser.TestProject.Type30",
            "private sealed struct AssemblyBrowser.TestProject.Type31",
            "protected sealed struct AssemblyBrowser.TestProject.Type32",
            "internal sealed struct AssemblyBrowser.TestProject.Type33",
            "protected internal sealed struct AssemblyBrowser.TestProject.Type34",
            "private protected sealed struct AssemblyBrowser.TestProject.Type35"
        };

        NamespaceInformation targetNamespaceInformation = _assemblyInformation!.Namespaces
            .First(namespaceInformation => namespaceInformation.Name
                       .Equals(typeof(NestedTypes).Namespace)
            );
        TypeInformation classWithClassesInformation = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(NestedTypes).FullName)
            );

        IEnumerable<TypeInformation> nestedTypesInformation = classWithClassesInformation.NestedTypes;
        IEnumerable<string> nestedTypes =
            nestedTypesInformation.Select(nestedTypeInformation => nestedTypeInformation.Name);
        CollectionAssert.AreEquivalent(expectedNestedTypes, nestedTypes);
    }

    [Test]
    public void AssemblyInformation_Contains_GenericClass()
    {
        const string expectedGenericClass = "public class GenericClass<TType>";
        const string expectedField = "private TType _value";
        const string expectedMethod = "public TType Method(TType value)";

        NamespaceInformation targetNamespaceInformation = _assemblyInformation!.Namespaces
            .First(namespaceInformation => namespaceInformation.Name
                       .Equals(typeof(GenericClass<>).Namespace)
            );
        TypeInformation genericClass = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Last()
                       .Contains(Regex.Replace(typeof(GenericClass<>).Name, "[`\\d]", ""))
            );
        FieldInformation field = genericClass.Fields.First();
        MethodInformation method = genericClass.Methods.First();

        Assert.AreEqual(expectedGenericClass, genericClass.Name);
        Assert.AreEqual(expectedField, field.Name);
        Assert.AreEqual(expectedMethod, method.Name);
    }

    [Test]
    public void AssemblyInformation_ContainsAll_GenericMethods()
    {
        var expectedMethods = new List<string>
        {
            "public static TType Method0<TType>(TType value)",
            "public static TType Method1<TType>(TType[] values)",
            "public List<List<TType>> Method2<TType>()",
            "public GenericMethods()"
        };

        NamespaceInformation targetNamespaceInformation = _assemblyInformation!.Namespaces
            .First(namespaceInformation => namespaceInformation.Name
                       .Equals(typeof(GenericMethods).Namespace)
            );
        TypeInformation classWithGenericMethods = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(GenericMethods).FullName)
            );
        IEnumerable<MethodInformation> methodsInformation = classWithGenericMethods.Methods;
        IEnumerable<string> methods = methodsInformation.Select(methodInformation => methodInformation.Name);

        CollectionAssert.AreEquivalent(expectedMethods, methods);
    }

    [Test]
    public void AssemblyInformation_ContainsAll_DeepNestedTypes()
    {
        var nestedTypesDictionary = new Dictionary<string, IEnumerable<string>>
        {
            {
                $"public class {AssemblyName}.{nameof(DeepNestedTypes)}", new List<string>
                {
                    $"public class {AssemblyName}.Type0"
                }
            },
            {
                $"public class {AssemblyName}.Type0", new List<string>
                {
                    $"public class {AssemblyName}.Type1"
                }
            },
            {
                $"public class {AssemblyName}.Type1", new List<string>
                {
                    $"public class {AssemblyName}.Type2"
                }
            },
            {
                $"public class {AssemblyName}.Type2", Enumerable.Empty<string>()
            }
        };

        NamespaceInformation targetNamespaceInformation = _assemblyInformation!.Namespaces
            .First(namespaceInformation => namespaceInformation.Name
                       .Equals(typeof(DeepNestedTypes).Namespace)
            );
        TypeInformation classWithDeepNestedTypes = targetNamespaceInformation.Types
            .First(typeInformation => typeInformation.Name
                       .Split(" ")
                       .Contains(typeof(DeepNestedTypes).FullName)
            );
        TypeInformation currentTypeInformation = classWithDeepNestedTypes;
        IEnumerable<TypeInformation> nestedTypesInformation = currentTypeInformation.NestedTypes;

        AssemblyInformation_ContainsAll_DeepNestedTypes_Stage(currentTypeInformation, nestedTypesInformation,
                                                              nestedTypesDictionary);
    }

    private static void AssemblyInformation_ContainsAll_DeepNestedTypes_Stage(TypeInformation currentTypeInformation,
                                                                              IEnumerable<TypeInformation>
                                                                                  nestedTypesInformation,
                                                                              IDictionary<string, IEnumerable<string>>
                                                                                  nestedTypesDictionary)
    {
        IEnumerable<string> expectedNestedTypes = nestedTypesDictionary[currentTypeInformation.Name];
        IEnumerable<TypeInformation> nestedTypesInformationList = nestedTypesInformation.ToList();
        IEnumerable<string> nestedTypes = nestedTypesInformationList.Select(typeInformation => typeInformation.Name);
        CollectionAssert.AreEquivalent(expectedNestedTypes, nestedTypes);

        foreach (TypeInformation typeInformation in nestedTypesInformationList)
        {
            AssemblyInformation_ContainsAll_DeepNestedTypes_Stage(typeInformation, typeInformation.NestedTypes,
                                                                  nestedTypesDictionary);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyBrowser.Core.Utilities;

namespace AssemblyBrowser.Core.Entities;

public class MethodInformation
{
    public readonly string Name;

    public MethodInformation(MethodInfo method)
    {
        Name = $"{ModifierUtilities.GetMethodModifiers(method)} {GetSignature(method)}";
    }

    private static string GetSignature(MethodInfo method)
    {
        string methodName = method.Name;
        IEnumerable<Type> arguments = method.GetGenericArguments();
        if (arguments.Any())
        {
            IEnumerable<string> argumentsName = method.GetGenericArguments().Select(TypeUtilities.GetName);
            var argumentsDeclaration = $"<{string.Join(", ", argumentsName)}>";
            methodName += argumentsDeclaration;
        }

        string returnType = TypeUtilities.GetName(method.ReturnType);
        ParameterInfo[] parameters = method.GetParameters();
        if (parameters.Length == 0)
        {
            return $"{returnType} {methodName}()";
        }

        IEnumerable<string> parameterNames = parameters.Select(parameter =>
            parameter.IsOut
                ? $"out {TypeUtilities.GetName(parameter.ParameterType)} {parameter.Name}"
                : $"{TypeUtilities.GetName(parameter.ParameterType)} {parameter.Name}");

        string parametersDeclaration = string.Join(", ", parameterNames);

        return $"{returnType} {methodName}({parametersDeclaration})";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyBrowser.Core.Utilities;

namespace AssemblyBrowser.Core.Entities;

public class MethodInformation
{
    public readonly string Name;

    public MethodInformation(MethodBase method)
    {
        Name = $"{ModifierUtilities.GetMethodModifiers(method)} {GetSignature(method)}";
    }

    private static string GetSignature(MethodBase method)
    {
        string methodName = method.Name;

        if (!method.IsConstructor)
        {
            var commonMethod = (MethodInfo) method;
            IEnumerable<Type> arguments = commonMethod.GetGenericArguments();
            if (arguments.Any())
            {
                IEnumerable<string> argumentsName = arguments.Select(TypeUtilities.GetName);
                var argumentsDeclaration = $"<{string.Join(", ", argumentsName)}>";
                methodName += argumentsDeclaration;
            }
        }
        else if (method.DeclaringType is not null)
        {
            string fullName = TypeUtilities.GetName(method.DeclaringType);
            methodName = fullName.Split(".").Last();
        }

        ParameterInfo[] parameters = method.GetParameters();
        IEnumerable<string> parameterNames =
            parameters.Select(parameter => parameter.IsOut
                                  ? $"out {TypeUtilities.GetName(parameter.ParameterType)} {parameter.Name}"
                                  : $"{TypeUtilities.GetName(parameter.ParameterType)} {parameter.Name}");

        string parametersDeclaration = string.Join(", ", parameterNames);

        if (method.IsConstructor)
        {
            return $"{methodName}({parametersDeclaration})";
        }

        string returnType = TypeUtilities.GetName(((MethodInfo) method).ReturnType);
        return $"{returnType} {methodName}({parametersDeclaration})";
    }
}
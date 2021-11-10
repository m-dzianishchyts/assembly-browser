using System.Reflection;
using AssemblerBrowser.Core.Utilities;

namespace AssemblerBrowser.Core.Entities;

public class MethodInformation
{
    public readonly string Name;

    public MethodInformation(MethodInfo method)
    {
        Name = $"{ModifierUtilities.GetMethodModifiers(method)}{GetSignature(method)}";
    }

    private static string GetSignature(MethodInfo method)
    {
        var signature = $"{TypeUtilities.GetName(method.ReturnType)} {method.Name}(";
        var methodParameters = method.GetParameters();
        if (methodParameters.Length == 0)
            return $"{signature})";

        foreach (var parameter in methodParameters)
        {
            if (parameter.IsOut)
                signature += "out ";
            signature += $"{TypeUtilities.GetName(parameter.ParameterType)} {parameter.Name}, ";
        }

        while (signature.IndexOf('&') != -1) signature = signature.Replace('&', ' ');

        return signature.Substring(0, signature.Length - 2) + ")";
    }
}
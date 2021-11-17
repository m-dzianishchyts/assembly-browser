namespace AssemblyBrowser.TestProject;

public class GenericMethods
{
    public static TType? Method0<TType>(TType value)
    {
        return value ?? default;
    }

    public static TType? Method1<TType>(params TType[] values)
    {
        return values.First() ?? default;
    }

    public List<List<TType>>? Method2<TType>()
    {
        return default;
    }
}
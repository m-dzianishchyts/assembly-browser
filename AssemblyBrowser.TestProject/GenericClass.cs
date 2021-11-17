namespace AssemblyBrowser.TestProject;

public class GenericClass<TType>
{
    private TType? _value;

    public TType Method(TType value)
    {
        _value = value;
        return _value;
    }
}
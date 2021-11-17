using System.Diagnostics.CodeAnalysis;

namespace AssemblyBrowser.TestProject;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
[SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
public abstract class Properties : PropertiesToOverride
{
    public sealed override int Property0 { get; set; }
    public abstract int Property1 { get; set; }
    public virtual int Property2 { get; set; }
    public static int Property3 { get; set; }
    public int Property4 { get; set; }
    private static int Property5 { get; set; }
    private int Property6 { get; set; }
    protected sealed override int Property7 { get; set; }
    protected abstract int Property8 { get; set; }
    protected virtual int Property9 { get; set; }
    protected static int Property10 { get; set; }
    protected int Property11 { get; set; }
    internal sealed override int Property12 { get; set; }
    internal abstract int Property13 { get; set; }
    internal virtual int Property14 { get; set; }
    internal static int Property15 { get; set; }
    internal int Property16 { get; set; }
    protected internal sealed override int Property17 { get; set; }
    protected internal abstract int Property18 { get; set; }
    protected internal virtual int Property19 { get; set; }
    protected internal static int Property20 { get; set; }
    protected internal int Property21 { get; set; }
    private protected sealed override int Property22 { get; set; }
    private protected abstract int Property23 { get; set; }
    private protected virtual int Property24 { get; set; }
    private protected static int Property25 { get; set; }
    private protected int Property26 { get; set; }
}
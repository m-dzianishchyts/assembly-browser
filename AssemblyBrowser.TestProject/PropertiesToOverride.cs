using System.Diagnostics.CodeAnalysis;

namespace AssemblyBrowser.TestProject;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
[SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
public class PropertiesToOverride
{
    public virtual int Property0 { get; set; }
    protected virtual int Property7 { get; set; }
    internal virtual int Property12 { get; set; }
    protected internal virtual int Property17 { get; set; }
    private protected virtual int Property22 { get; set; }
}
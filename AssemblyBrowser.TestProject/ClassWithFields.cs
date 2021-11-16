#pragma warning disable CS0414

using System.Diagnostics.CodeAnalysis;

namespace AssemblyBrowser.TestProject;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
[SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
[SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
public class ClassWithFields
{
    public const int field2 = 0;
    private const int field7 = 0;
    protected const int field12 = 0;
    internal const int field17 = 0;
    protected internal const int field22 = 0;
    private protected const int field27 = 0;
    public static readonly int field0 = 0;
    public static int field1 = 0;
    private static readonly int field5 = 0;
    private static int field6 = 0;
    protected static readonly int field10 = 0;
    protected static int field11 = 0;
    internal static readonly int field15 = 0;
    internal static int field16 = 0;
    protected internal static readonly int field20 = 0;
    protected internal static int field21 = 0;
    private protected static readonly int field25 = 0;
    private protected static int field26 = 0;
    protected readonly int field13 = 0;
    internal readonly int field18 = 0;
    protected internal readonly int field23 = 0;
    private protected readonly int field28 = 0;
    public readonly int field3 = 0;
    private readonly int field8 = 0;
    protected int field14 = 0;
    internal int field19 = 0;
    protected internal int field24 = 0;
    private protected int field29 = 0;
    public int field4 = 0;
    private int field9 = 0;
}
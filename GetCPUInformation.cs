using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using System.Runtime.InteropServices;

namespace GetCPUInformationPlugin;

using CPUBrandName = System.String;

public class GetCPUInformationPlugin : BasePlugin
{
    public override string ModuleName => "GetCPUInformation";
    public override string ModuleVersion => "0.1";
    [DllImport("libserver.so")]
    public static extern IntPtr GetCPUInformation();

    private CPUBrandName? _brandName;

    public override void Load(bool hotReload)
    {
        IntPtr ptrBrandName = Marshal.ReadIntPtr(GetCPUInformation() + 24);
        _brandName = Marshal.PtrToStringAnsi(ptrBrandName);
    }

    [ConsoleCommand("css_cpu", "Get CPU Information")]
    public void OnGetCPUInformation(CCSPlayerController? player, CommandInfo command)
    {
        if (_brandName is CPUBrandName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(_brandName);
        }
    }
}
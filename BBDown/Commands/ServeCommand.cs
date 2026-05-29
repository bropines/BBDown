using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace BBDown.Commands;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)]
public class ServeSettings : CommandSettings
{
    [CommandOption("-l|--listen")]
    [LocalizedDescription("opt_listen")]
    public string ListenUrl { get; set; } = "http://0.0.0.0:23333";

    [CommandOption("--max-concurrent")]
    [LocalizedDescription("opt_max_concurrent")]
    public int MaxConcurrent { get; set; } = 3;
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class ServeCommand : Command<ServeSettings>
{
    protected override int Execute(CommandContext context, ServeSettings settings, CancellationToken cancellationToken)
    {
        _ = BBDownUtil.CheckUpdateAsync();
        Program.StartServer(settings.ListenUrl, settings.MaxConcurrent);
        return 0;
    }
}

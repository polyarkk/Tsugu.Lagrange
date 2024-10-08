﻿using Tsugu.Api.Enum;
using Tsugu.Lagrange.Util;

namespace Tsugu.Lagrange.Command.Endpoint;

[ApiCommand(
    Aliases = ["预测线总览", "ycxall"],
    Description = "指定档位的预测线",
    Example = """
              ycxall 177：返回177号活动的全部档位预测线
              ycxall 177 jp：返回日服177号活动的全部档位预测线
              """
)]
public class CutoffAll : BaseCommand {
    protected override ArgumentMeta[] Arguments { get; } = [
        OptionalArgument<uint>("eventId", "活动ID"),
        OptionalArgument<Server>("mainServer", "服务器"),
    ];

    protected async override Task Invoke(Context ctx, ParsedArgs args) {
        string base64 = await ctx.Tsugu.Query.CutoffAll(
            args["mainServer"].GetOr(() => ctx.TsuguUser.MainServer),
            args["eventId"].GetOrNull<uint>(),
            ctx.AppSettings.Compress
        );

        await ctx.SendImage(base64);
    }
}

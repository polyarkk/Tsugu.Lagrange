﻿using Tsugu.Api.Enum;
using Tsugu.Lagrange.Util;

namespace Tsugu.Lagrange.Command.Endpoint;

[ApiCommand(
    Aliases = ["查试炼", "查stage", "查舞台", "查festival", "查5v5"],
    Description = "查询活动的试炼信息",
    Example = """
              查试炼 true 157 jp：返回日服的157号活动的试炼信息，包含歌曲meta
              查试炼 false 157：返回157号活动的试炼信息，不包含歌曲meta
              查试炼 true：返回当前活动的试炼信息，包含歌曲meta
              查试炼：返回当前活动的试炼信息
              """
)]
public class EventStage : BaseCommand {
    protected override ArgumentMeta[] Arguments { get; } = [
        OptionalArgument<bool>("meta", "是否显示歌曲Meta"),
        OptionalArgument<uint>("eventId", "活动ID"),
        OptionalArgument<Server>("mainServer", "服务器"),
    ];

    protected async override Task Invoke(Context ctx, ParsedArgs args) {
        string base64 = await ctx.Tsugu.Query.EventStage(
            args["mainServer"].GetOr(() => ctx.TsuguUser.MainServer),
            args["eventId"].GetOrNull<uint>(),
            args["meta"].GetOr(() => false),
            ctx.AppSettings.Compress
        );

        await ctx.SendImage(base64);
    }
}

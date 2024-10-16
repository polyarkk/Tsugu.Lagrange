﻿using Tsugu.Frontend.Command.Argument;
using Tsugu.Frontend.Context;
using Tsugu.Frontend.Util;

namespace Tsugu.Frontend.Command.Endpoint;

[ApiCommand(
    Aliases = ["历史预测线", "lsycx"],
    Description = "查询指定档位的预测线与最近的4期活动类型相同的活动的档线数据"
)]
public class CutoffListOfRecentEvent : BaseCommand {
    protected override ArgumentMeta[] Arguments { get; } = [
        Argument<uint>("tier", "档位"),
        Argument<uint>("eventId", "活动ID").AsOptional(),
    ];

    protected async override Task InvokeInternal(TsuguContext ctx, ParsedArgs args) {
        string base64 = await ctx.Tsugu.Query.CutoffListOfRecentEvent(
            ctx.TsuguUser.MainServer,
            args["tier"].Get<uint>(),
            args["eventId"].GetOrNull<uint>(),
            ctx.AppSettings.Compress
        );

        await ctx.ReplyImage(base64);
    }
}

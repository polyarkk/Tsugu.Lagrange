﻿using Tsugu.Lagrange.Util;

namespace Tsugu.Lagrange.Command.Endpoint;

[ApiCommand(
    Aliases = ["随机曲"],
    Description = "根据关键词或曲目ID随机曲目信息",
    Example = """
              随机曲 lv27：在所有包含27等级难度的曲中, 随机返回其中一个
              随机曲 ag：返回随机的 Afterglow 曲目
              """
)]
public class SongRandom : BaseCommand {
    protected override ArgumentMeta[] Arguments { get; } = [
        OptionalArgument<string>("keywords", "关键词"),
    ];

    protected async override Task Invoke(Context ctx, ParsedArgs args) {
        string base64 = await ctx.Tsugu.Query.SongRandom(
            ctx.TsuguUser.MainServer,
            args.ConcatenatedArgs,
            ctx.AppSettings.Compress
        );

        await ctx.SendImage(base64);
    }
}

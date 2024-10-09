﻿namespace Tsugu.Lagrange.Command.Endpoint;

[ApiCommand(
    Aliases = ["查卡"],
    Description = """
                  查询指定卡面的信息，或查询符合条件的卡面列表
                  使用示例:
                  查卡 1399：返回1399号卡面的信息
                  查卡 红 ars 4x：返回角色为ars，稀有度为4星的卡面列表
                  """,
    UsageHint = "<关键词>"
)]
public class SearchCard : BaseCommand {
    public async override Task Invoke(Context ctx, ParsedCommand args) {
        string arg = args.ConcatenatedArgs;

        if (string.IsNullOrWhiteSpace(arg)) {
            await ctx.SendPlainText(GetErrorAndHelpText("未指定查询关键词！"));

            return;
        }

        string base64 = await ctx.Tsugu.Query.SearchCard(
            ctx.TsuguUser.DisplayedServerList,
            arg,
            false,
            ctx.AppSettings.Compress
        );

        await ctx.SendImage(base64);
    }
}

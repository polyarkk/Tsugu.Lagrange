﻿using Lagrange.Core.Common.Interface.Api;
using Lagrange.Core.Message;
using Tsugu.Lagrange.Util;

namespace Tsugu.Lagrange.Command.Endpoint;

[ApiCommand(
    Aliases = ["查卡面", "查插画"],
    Description = "根据卡面ID查询卡面插画"
)]
public class GetCardIllustration : BaseCommand {
    protected override ArgumentMeta[] Arguments { get; } = [
        Argument<uint>("cardId", "卡面ID"),
    ];

    protected async override Task Invoke(Context ctx, ParsedArgs args) {
        string[] base64List = await ctx.Tsugu.Query.GetCardIllustration(args["cardId"].Get<uint>());

        MessageBuilder messageBuilder = MessageUtil.GetDefaultMessageBuilder(ctx);

        foreach (string base64 in base64List) {
            messageBuilder.Image(Convert.FromBase64String(base64));
        }

        await ctx.Bot.SendMessage(messageBuilder.Build());
    }
}

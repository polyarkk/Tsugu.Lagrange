﻿using Lagrange.Tsugu.Api.Enum;
using Lagrange.Tsugu.Api.Rest;
using Lagrange.Tsugu.Command;

namespace Lagrange.Tsugu.Api.Endpoint;

[ApiCommand(Alias = "卡池模拟", Description = "模拟抽卡", UsageHint = "[次数] [卡池ID]")]
public class GachaSimulate : BaseCommand {
    public async override Task Invoke(Context ctx, ParsedCommand args) {
        Dictionary<string, object?> p = new() {
            ["mainServer"] = BandoriServer.Cn
        };

        if (args.HasArgument(0)) {
            p["times"] = args.GetInt32(0);
        }

        if (args.HasArgument(1)) {
            p["gachaId"] = args.GetInt32(1);
        }

        p["compress"] = ctx.Settings.Compress;

        using SugaredHttpClient rest = ctx.Rest;

        RestResponse response = (await rest.TsuguPost("/gachaSimulate", p))[0];

        if (response.IsImageBase64()) {
            await ctx.SendImage(response.String!);
        }
    }
}

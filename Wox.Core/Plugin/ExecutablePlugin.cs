﻿using System;
using System.Diagnostics;
using Wox.Core.UserSettings;
using Wox.Plugin;

namespace Wox.Core.Plugin
{
    internal class ExecutablePlugin : JsonRPCPlugin
    {
        private readonly ProcessStartInfo _startInfo;
        public override string SupportedLanguage { get; set; } = AllowedLanguage.Executable;

        public ExecutablePlugin(string filename)
        {
            _startInfo = new ProcessStartInfo
            {
                FileName = filename,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
        }

        protected override string ExecuteQuery(Query query)
        {
            JsonRPCServerRequestModel request = new JsonRPCServerRequestModel
            {
                Method = "query",
                Parameters = new object[] { query.Search },
                HttpProxy = HttpProxy.Instance
            };

            _startInfo.Arguments = $"\"{request}\"";

            return Execute(_startInfo);
        }

        protected override string ExecuteCallback(JsonRPCRequestModel rpcRequest)
        {
            _startInfo.Arguments = $"\"{rpcRequest}\"";
            return Execute(_startInfo);
        }
    }
}
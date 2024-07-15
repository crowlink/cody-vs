﻿using Cody.Core.AgentProtocol;
using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cody.VisualStudio.Connector
{
    public interface IAgentClient
    {
        [JsonRpcMethod("initialize")]
        Task<ServerInfo> Initialize(ClientInfo clientInfo);

        [JsonRpcMethod("graphql/getCurrentUserCodySubscription")]
        Task<CurrentUserCodySubscription> GetCurrentUserCodySubscription();

        [JsonRpcMethod("initialized")]
        void Initialized();
    }
}

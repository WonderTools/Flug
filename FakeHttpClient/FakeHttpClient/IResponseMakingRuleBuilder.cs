﻿using System;
using System.Net.Http;

namespace WonderTools.Flug
{
    public interface IResponseMakingRuleBuilder : IRuleBuilder
    {
        RuleBuilder Using(Action<HttpRequestMessage, HttpResponseMessage> modifier);
    }
}
﻿using System;
using System.Net.Http;

namespace WonderTools.FakeHttpClient
{
    public interface IRuleBuilder
    {
        IResponseMakingRuleBuilder Respond();
        void Throw(Func<HttpRequestMessage, Exception> exceptionGenerator);
        IRuleBuilder Execute(Action<HttpRequestMessage> action);
    }
}
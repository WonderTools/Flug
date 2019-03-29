﻿using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using WonderTools.FakeHttpClient.RequestRules;
using WonderTools.FakeHttpClient.ResponseRules;

namespace WonderTools.FakeHttpClient.Tests
{
    public class ResponseHeaderTests
    {
        MockableHttpMessageHandler _messageHandler;
        HttpClient _client;
        private string _defaultUri= @"https://www.google.com/";

        [SetUp]
        public void SetUp()
        {
            _messageHandler = new MockableHttpMessageHandler();
            _client = new HttpClient(_messageHandler);
        }

        [Test]
        public void When_request_then_use_range_response()
        {
            var responseHttpCode = HttpStatusCode.Accepted;
            var responseRange = "byte";
            _messageHandler.WhenRequest().WhenUri(_defaultUri).SetAcceptRangeInResponse(responseRange).UseStatusCode(responseHttpCode);

            var response = _client.GetAsync(new Uri("https://www.google.com")).Result;

            Assert.AreEqual(responseRange, response.Headers.AcceptRanges.ToString());
            Assert.AreEqual(responseHttpCode, response.StatusCode);
        }

        [Test]
        public void When_request_then_use_transfer_encoding_response()
        {
            var responseHttpCode = HttpStatusCode.Accepted;
            var responseRange = "byte";
            var transferEncoding = "trailer";
            _messageHandler.WhenRequest().WhenUri(_defaultUri).SetAcceptRangeInResponse(responseRange).SetTransferEncodingInResponse(transferEncoding).UseStatusCode(responseHttpCode);

            var response = _client.GetAsync(new Uri(_defaultUri)).Result;
            
            Assert.AreEqual(transferEncoding, response.Headers.TransferEncoding.ToString());
        }
       
        [Test]
        public void When_request_then_use_server_in_response()
        {
            var responseHttpCode = HttpStatusCode.Accepted;
            var responseRange = "byte";
            _messageHandler.WhenRequest().WhenUri(_defaultUri).SetAcceptRangeInResponse(responseRange).SetServerInResponse("Kestrel").UseStatusCode(responseHttpCode);

            var response = _client.GetAsync(new Uri(_defaultUri)).Result;

            Assert.AreEqual("Kestrel", response.Headers.Server.ToString());
        }

        [Test]
        public void When_request_then_use_proxy_authenticate_in_response()
        {
            var responseHttpCode = HttpStatusCode.Accepted;
            var responseRange = "byte";
            var proxyAuthenticate = "Basic";
            _messageHandler.WhenRequest().WhenUri(_defaultUri).SetAcceptRangeInResponse(responseRange).SetProxyAuthenticateInResponse(proxyAuthenticate).UseStatusCode(responseHttpCode);

            var response = _client.GetAsync(new Uri(_defaultUri)).Result;

            Assert.AreEqual(proxyAuthenticate, response.Headers.ProxyAuthenticate.ToString());
        }

        [Test]
        public void When_request_then_use_pragma_in_response()
        {
            var responseHttpCode = HttpStatusCode.Accepted;
            var responseRange = "byte";
            var proxyAuthenticate = "Basic";
            _messageHandler.WhenRequest().WhenUri(_defaultUri).SetAcceptRangeInResponse(responseRange).SetProxyAuthenticateInResponse(proxyAuthenticate).SetPragmaInResponse("Cache-Control","no-cache").UseStatusCode(responseHttpCode);

            var response = _client.GetAsync(new Uri(_defaultUri)).Result;

            Assert.AreEqual("Cache-Control=no-cache", response.Headers.Pragma.ToString());
        }
    }
}

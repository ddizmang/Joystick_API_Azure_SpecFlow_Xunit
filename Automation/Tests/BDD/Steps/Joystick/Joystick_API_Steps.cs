﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Automation.Classes.Operations;
using Automation.Domain.Classes;
using Automation.Domain.Models.Joystick;
using Automation.Domain.Operations;
using Automation.Domain.Resources;
using AventStack.ExtentReports.Utils;
using HL7Enumerator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using TechTalk.SpecFlow;
using Xunit;

namespace Automation.Tests.BDD.Steps.Joystick
{
    [Binding]
    public class Joystick_API_Steps
    {
        private ScenarioContext _scenarioContext;

        public Joystick_API_Steps(ScenarioContext scenarioContext)
        {
            GetScenarioContext(scenarioContext);
        }
        public void GetScenarioContext(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        #region Given Steps

        [Given(@"api url (.*)")]
        public void Given_ApiUrl(string url)
        {
            var sc = _scenarioContext.StepContext.StepInfo.Text;
            var tc = _scenarioContext.ScenarioInfo.Title;
            //Replace variable
            url = url.Contains("%") ? ReplaceToken(url) : url;
            //Replace with resources url
            if (url.Contains("%testrun_resources."))
            {
                var first = url.IndexOf(".");
                var second = url.IndexOf("%", first);
                var property = url.Substring(first + 1, second - (first + 1));
                switch (property.ToLower())
                {
                    /* Configure Resources API entries below */
                    case "url":
                        url = url.Replace("%testrun_resources.url%", TestRun_Resources.URL);
                        break;
                    case "idpauthurl":
                        url = url.Replace("%testrun_resources.idpauthurl%", TestRun_Resources.IDPAuthUrl);
                        break;
                    case "worker_v3_url":
                        url = url.Replace("%testrun_resources.worker_v3_url%", TestRun_Resources.WorkerV3LocalURL);
                        break;
                }

            }
            _scenarioContext["URL"] = url;

        }

        [Given(@"request json (.*)")]
        public void Given_JsonRequestString(string jsonString)
        {
            try
            {
                _scenarioContext["JSONRequestBody"] = jsonString.Contains("%") ? ReplaceToken(jsonString) : jsonString;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given request json", _scenarioContext.ScenarioInfo.Title + "Error - getting json", e);
            }
        }

        [Given(@"request read json file (.*)")]
        public void Given_JsonRequestWithFile(string jsonFile)
        {
            try
            {
                using (StreamReader r = new StreamReader(jsonFile))
                {
                    string json = r.ReadToEnd();
                    _scenarioContext["JSONRequestBody"] = json.Contains("%") ? ReplaceToken(json) : json;
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given read json file", _scenarioContext.ScenarioInfo.Title + "Error - getting json file", e);
            }
        }

        [Given(@"var (.*) read json file (.*)")]
        public void GivenVarRequestReadJsonFile(string variableName, string jsonFile)
        {
            string json = String.Empty;
            try
            {
                using (StreamReader r = new StreamReader(jsonFile))
                {
                    json = r.ReadToEnd();
                    json = json.Contains("%") ? ReplaceToken(json) : json;
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given var read json file", _scenarioContext.ScenarioInfo.Title + "Error - Given var read json file", e);
            }
            _scenarioContext.Remove(variableName);
            _scenarioContext.Add(variableName, json);
        }


        [Given(@"request read sql file (.*)")]
        [Given(@"sql read file (.*)")]
        public void GivenRequestReadSqlFile(string sqlFile)
        {
            try
            {
                using (StreamReader r = new StreamReader(sqlFile))
                {
                    string sql = r.ReadToEnd();
                    if (sql.ToUpper().StartsWith("SELECT"))
                    {
                        sql = sql + " for json path, root('sqlresult')";
                    }

                    _scenarioContext["sqlQueryText"] = sql.Contains("%") ? ReplaceToken(sql) : sql;
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given read json file", _scenarioContext.ScenarioInfo.Title + "Error - getting json file", e);
            }
        }


        //TODO: Need to research if this is still used
        [Given(@"response (.*)")]
        public void GivenResponse(string p0)
        {
            var response = _scenarioContext["response"];
            var queryString = p0.ToLower();
            var previousValue = "";
            try
            {
                JObject jo = JObject.Parse(response.ToString());
                previousValue = JsonXPathQuery(queryString, jo);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - response", _scenarioContext.ScenarioInfo.Title + "Error - getting response", e);
            }

            _scenarioContext["response"] = response;
            _scenarioContext["previousResponseValue"] = previousValue;
        }

        [Given(@"path (.*)")]
        public void Given_AddPathToRequest(string pathValue)
        {
            var url = (string)_scenarioContext["URL"];

            try
            {
                pathValue = pathValue.Contains("%") ? ReplaceToken(pathValue) : pathValue;
                pathValue = pathValue.Contains("response.") ? ReplaceWithJsonResponseData(pathValue) : pathValue;
                url = url + "/" + pathValue;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given path", _scenarioContext.ScenarioInfo.Title + "Error - adding path variable", e);
            }

            _scenarioContext["URL"] = url;
        }

        [Given(@"param (.*) value (.*)")]
        public void Given_AddQueryParameterToRequest(string parameterName, string parameterValue)
        {
            var url = (string)_scenarioContext["URL"];

            try
            {
                if (parameterName.ToLower().Contains("response."))
                {
                    var response = _scenarioContext["response"];
                    var queryString = parameterName.ToLower().Split("response.").Last();
                    JObject jo = JObject.Parse(response.ToString());
                    if (!url.Contains("?"))
                    {
                        url = url + "?" + parameterName + "=" + JsonXPathQuery(queryString, jo);
                    }
                    else
                    {
                        url = url + "&" + parameterName + "=" + JsonXPathQuery(queryString, jo);
                    }
                }
                else
                {
                    if (parameterValue.Contains("%"))
                    {
                        parameterValue = ReplaceToken(parameterValue);
                    }
                    if (!url.Contains("?"))
                    {
                        url = url + "?" + parameterName + "=" + parameterValue;
                    }
                    else
                    {
                        url = url + "&" + parameterName + "=" + parameterValue;
                    }
                }

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given add query param", _scenarioContext.ScenarioInfo.Title + "Error - adding query param", e);
            }

            _scenarioContext["URL"] = url;
        }

        [Given(@"var (.*) as (.*) type (.*)")]
        public void Given_AddVariableAsType(string variableName, string variableValue, string p2)
        {
            object formatted = null;
            try
            {
                switch (p2.ToLower())
                {
                    case "date":
                        if (variableValue == "now")
                        {
                            formatted = DateTime.Now;
                        }
                        else if (variableValue == "utc")
                        {
                            formatted = ClientDate.CreateUTCEventDateTime();
                        }
                        else
                        {
                            DateTime.TryParse(variableValue, out DateTime dateValue);
                            formatted = dateValue;
                        }
                        break;
                    case "guid":
                        formatted = Guid.NewGuid().ToString();
                        break;
                    default:
                        formatted = variableValue;
                        break;
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given var", _scenarioContext.ScenarioInfo.Title + "Error - setting variable", e);
            }
            _scenarioContext.Remove(variableName);
            _scenarioContext.Add(variableName, formatted);
        }

        [Given(@"format var (.*) as string (.*)")]
        public void Given_FormatVarAsStringFormat(string variableName, string stringFormat)
        {
            var variable = _scenarioContext.Get<object>(variableName);
            string formatted = "";

            try
            {
                if (variable is DateTime)
                {
                    DateTime.TryParse(variable.ToString(), out DateTime dateValue);
                    formatted = dateValue.ToString(stringFormat);
                }
                else
                {
                    formatted = variable.ToString();
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given format var as string", _scenarioContext.ScenarioInfo.Title + "Error - formatting var", e);
            }
            _scenarioContext.Remove(variableName);
            _scenarioContext.Add(variableName, formatted);
        }

        [Given(@"var (.*) as value of var (.*) and var (.*)")]
        public void Given_AddVarAsVarAndVar(string newValueName, string firstValueName, string secondValueName)
        {
            var firstVariable = _scenarioContext.Get<object>(firstValueName);
            var secondVariable = _scenarioContext.Get<object>(secondValueName);
            string formatted = "";

            try
            {
                formatted = $"{firstVariable.ToString()}{secondVariable.ToString()}";
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given format var as combination", _scenarioContext.ScenarioInfo.Title + "Error - combining var", e);
            }
            _scenarioContext.Remove(newValueName);
            _scenarioContext.Add(newValueName, formatted);
        }

        //TODO: Need to refactor/remove
        [Given(@"var (.*) is equal to (.*) type (.*)")]
        public void Given_AddVarIsEqualValueOfType(string variableName, string variableValue, string variableType)
        {
            object formatted = null;
            JToken jt = null;

            try
            {
                if (variableValue.Contains("response."))
                {
                    if (variableType == "json")
                    {
                        variableValue = (string)ReplaceWithJsonResponseJSONObject(variableValue);
                    }
                    else
                    {
                        variableValue = ReplaceWithJsonResponseData(variableValue);
                    }
                }
                else if (variableValue.Contains("%testrun_resources.WorkerV3BaseId%"))
                {
                    variableValue = TestRun_Resources.WorkerV3BaseId;
                }
                else if (variableValue.Contains('%'))
                {
                    variableValue = ReplaceToken(variableValue);
                }

                switch (variableType.ToLower())
                {
                    case "date":
                        if (variableValue == "now")
                        {
                            formatted = DateTime.Now;
                        }
                        else
                        {
                            DateTime.TryParse(variableValue, out DateTime dateValue);
                            formatted = dateValue;
                        }
                        break;
                    case "int":
                        formatted = Int32.Parse(variableValue);
                        break;
                    case "json":
                        formatted = JObject.Parse(variableValue).ToString();
                        break;
                    default:
                        formatted = variableValue;
                        break;
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given var is equal to", _scenarioContext.ScenarioInfo.Title + "Error - setting var", e);
            }
            _scenarioContext.Remove(variableName);
            _scenarioContext.Add(variableName, formatted);
        }

        [Given(@"generate var (.*) as random type (.*) length of (.*)")]
        public void Given_GenerateVarAsRandomTypeLengthOf(string variableName, string variableType, string variableLength)
        {
            object formatted = "";

            try
            {
                switch (variableType.ToLower())
                {
                    case "int":
                        formatted = RandomOperations.Int32(1, int.Parse(variableLength));
                        break;
                    case "guid":
                        formatted = Guid.NewGuid().ToString();
                        break;

                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given var as random", _scenarioContext.ScenarioInfo.Title + "Error - generating random", e);
            }
            _scenarioContext.Remove(variableName);
            _scenarioContext.Add(variableName, formatted.ToString());
        }

        [Given(@"request sqlserver (.*)")]
        [Given(@"sql query (.*)")]
        public void Given_SqlServerQueryString(string sqlQueryString)
        {
            //TODO: Need to rename to Given sqlserver query 
            try
            {
                var sql = sqlQueryString.Contains("%") ? ReplaceToken(sqlQueryString) : sqlQueryString;
                if (sql.ToUpper().StartsWith("SELECT"))
                {
                    sql = sql + " for json path, root('sqlresult')";
                }
                //_scenarioContext["sqlQueryText"] = sqlQueryString.Contains("%") ? ReplaceToken(sqlQueryString) + " for json path, root('sqlresult')" : sqlQueryString + " for json path, root('sqlresult')";
                _scenarioContext.Remove("sqlQueryText");
                _scenarioContext.Add("sqlQueryText", sql.ToString());

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given request sqlserver", _scenarioContext.ScenarioInfo.Title + "Error - getting sql query text", e);
            }
        }

        [Given(@"sqlserver connection string (.*)")]
        public void Given_SqlserverConnectionString(string sqlConnString)
        {
            try
            {
                _scenarioContext["sqlConnectionString"] = sqlConnString;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given sqlserver connection string", _scenarioContext.ScenarioInfo.Title + "Error - getting sql connect string", e);
            }
        }

        [Given(@"header Content-Type = (.*)")]
        public void Given_SetHeaderContentType(string contentType)
        {
            try
            {
                _scenarioContext["HeaderContentType"] = contentType;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given header Content-Type", _scenarioContext.ScenarioInfo.Title + "Error - setting header Content-Type", e);
            }
        }

        [Given(@"header Authorization = Bearer (.*)")]
        public void Given_SetHeaderAuthorizationBearer(string bearerToken)
        {
            try
            {
                TestRun_Resources.BearerToken = bearerToken.Contains("%") ? ReplaceToken(bearerToken) : bearerToken;

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given header bearer token", _scenarioContext.ScenarioInfo.Title + "Error - setting header bearer token", e);
            }
        }

        [Given(@"header Authorization = Basic (.*)")]
        public void Given_SetHeaderAuthorizationBasic(string basicToken)
        {
            try
            {
                TestRun_Resources.BasicAuth = basicToken.Contains("%") ? ReplaceToken(basicToken) : basicToken;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given header Authorization = Basic", _scenarioContext.ScenarioInfo.Title + "Error - setting header Authorization = Basic", e);
            }
        }

        [Given(@"form content (.*) value (.*)")]
        public void Given_SetFormContentValue(string name, string value)
        {
            try
            {
                if (TestRun_Resources.FormContent != null)
                {
                    Dictionary<string, string> addContent = TestRun_Resources.FormContent;
                    if (value.Contains("%"))
                    {
                        value = ReplaceToken(value);
                    }
                    addContent.Add(name, value);
                    TestRun_Resources.FormContent = addContent;
                }
                else
                {
                    Dictionary<string, string> newContent = new Dictionary<string, string>();
                    if (value.Contains("%"))
                    {
                        value = ReplaceToken(value);
                    }
                    newContent.Add(name, value);
                    TestRun_Resources.FormContent = newContent;
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given header form content values",
                                           _scenarioContext.ScenarioInfo.Title + "Error - setting header form content values", e);
            }
        }

        [Given(@"wait (.*)")]
        public void Given_Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        [Given(@"RabbitMQ publish exchange is equal to (.*)")]
        public void Given_SetRabbitMQPublishExchangeIsEqualTo(string exchangeValueName)
        {
            try
            {
                _scenarioContext.Remove("%rabbitPublishExchange%");
                _scenarioContext.Add("%rabbitPublishExchange%", exchangeValueName);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given RabbitMQ publish exchange",
                    _scenarioContext.ScenarioInfo.Title + "Error - setting RabbitMQ publish exchange", e);
            }
        }

        [Given(@"RabbitMQ publish routing key is equal to (.*)")]
        public void Given_SetRabbitMQPublishRoutingKeyIsEqualTo(string routingKeyValue)
        {
            try
            {
                _scenarioContext.Remove("%rabbitRoutingKey");
                _scenarioContext.Add("%rabbitRoutingKey", routingKeyValue);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given RabbitMQ publish routing key",
                    _scenarioContext.ScenarioInfo.Title + "Error - setting RabbitMQ publish routing key", e);
            }
        }


        [Given(@"replace json (.*) at path (.*) with (.*)")]
        public void GivenReplaceJsonPathWith(string jsonVariableName, string jsonPath, string newValue)
        {
            var jsonString = _scenarioContext.Get<object>(jsonVariableName);
            var newJson = string.Empty;
            try
            {
                newValue = newValue.Contains("%") ? ReplaceToken(newValue) : newValue;
                JObject json = JObject.Parse(jsonString.ToString());
                JToken jt = json.SelectToken(jsonPath);
                if (jt != null)
                {
                    if (newValue.ToLower() != "null")
                    {
                        if (JsonOperations.IsJson(newValue))
                        {
                            JToken jtnw = JToken.Parse(newValue);
                            if (jtnw is JObject)
                            {
                                var jo = JObject.Parse(newValue);
                                jt.Replace(jo);
                            }
                            else
                            {
                                jt.Replace(jtnw);
                            }

                        }
                        else
                        {
                            jt.Replace(newValue);
                        }
                    }
                    else
                    {
                        jt.Replace(JValue.CreateNull());
                    }
                }
                else
                {
                    throw new ArgumentNullException($"The jsonPath value of {jsonPath} does not exist");
                }

                newJson = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given replace json at path with value",
                    _scenarioContext.ScenarioInfo.Title + "Error - replacing json path with value", e);
            }
            _scenarioContext.Remove(jsonVariableName);
            _scenarioContext.Add(jsonVariableName, newJson);
        }

        [Given(@"replace json at path (.*) with null")]
        public void GivenReplaceJsonAtPathWithNull(string jsonPath)
        {
            var jsonString = _scenarioContext.Get<object>("JSONRequestBody");
            var newJson = string.Empty;
            try
            {
                JObject json = JObject.Parse(jsonString.ToString());
                JToken jt = json.SelectToken(jsonPath);
                if (jt != null)
                {
                    jt.Replace(JValue.CreateNull());
                }
                else
                {
                    throw new ArgumentNullException($"The jsonPath value of {jsonPath} does not exist");
                }
                newJson = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - replace json at path jsonPath with null",
                    _scenarioContext.ScenarioInfo.Title + "Error - replace json at path jsonPath with null", e);
            }
            _scenarioContext.Remove("JSONRequestBody");
            _scenarioContext.Add("JSONRequestBody", newJson);
        }


        [Given(@"remove json (.*) at path (.*)")]
        public void GivenRemoveJsonAtPath(string jsonVariableName, string jsonPath)
        {
            var jsonString = _scenarioContext.Get<object>(jsonVariableName);
            var newJson = string.Empty;
            try
            {
                JObject json = JObject.Parse(jsonString.ToString());
                JToken jt = json.SelectToken(jsonPath);
                JArray ja = new JArray();
                if (jt != null)
                {
                    jt.Replace(ja);
                }
                else
                {
                    throw new ArgumentNullException($"The jsonPath value of {jsonPath} does not exist");
                }

                newJson = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given replace json at path with value",
                    _scenarioContext.ScenarioInfo.Title + "Error - replacing json path with value", e);
            }
            _scenarioContext.Remove(jsonVariableName);
            _scenarioContext.Add(jsonVariableName, newJson);
        }


        [Given(@"var (.*) = (.*) \+ (.*)")]
        public void GivenVar(string variableName, string firstValue, string secondValue)
        {
            var newValue = string.Empty;
            try
            {
                firstValue = firstValue.Contains("%") ? ReplaceToken(firstValue) : firstValue;
                secondValue = secondValue.Contains("%") ? ReplaceToken(secondValue) : secondValue;
                newValue = $"{firstValue}{secondValue}";
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given var is equal to first value and second value",
                    _scenarioContext.ScenarioInfo.Title + "Error - creating a variable as a combination of values", e);
            }
            _scenarioContext.Remove(variableName);
            _scenarioContext.Add(variableName, newValue);
        }

        [Given(@"hl7 file location (.*)")]
        public void GivenHlFileLocation(string hl7ExportDirectory)
        {
            var newValue = string.Empty;
            try
            {
                newValue = hl7ExportDirectory.Contains("%") ? ReplaceToken(hl7ExportDirectory) : hl7ExportDirectory;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given hl7 file location",
                    _scenarioContext.ScenarioInfo.Title + "Error - creating a variable for hl7 file location", e);
            }
            _scenarioContext.Remove("hl7FileLocation");
            _scenarioContext.Add("hl7FileLocation", newValue);
        }

        [Given(@"hl7 file prefix (.*)")]
        public void GivenHlFilePrefix(string hl7FilePrefix)
        {
            var newValue = string.Empty;
            try
            {
                newValue = hl7FilePrefix.Contains("%") ? ReplaceToken(hl7FilePrefix) : hl7FilePrefix;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given hl7 file prefix",
                    _scenarioContext.ScenarioInfo.Title + "Error - creating a variable for hl7 file prefix", e);
            }
            _scenarioContext.Remove("hl7FilePrefix");
            _scenarioContext.Add("hl7FilePrefix", newValue);
        }

        [Given(@"hl7 file wait (.*) mins")]
        public void GivenHlFileWaitMins(int hl7FileWaitTime)
        {
            var newValue = 0;
            try
            {
                newValue = hl7FileWaitTime;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given hl7 file wait mins",
                    _scenarioContext.ScenarioInfo.Title + "Error - creating a variable for hl7 file wait mins", e);
            }
            _scenarioContext.Remove("hl7FileWaitTime");
            _scenarioContext.Add("hl7FileWaitTime", newValue);
        }


        [Given(@"xml file location (.*)")]
        public void GivenXmlFileLocation(string xmlExportDirectory)
        {
            var newValue = string.Empty;
            try
            {
                newValue = xmlExportDirectory.Contains("%") ? ReplaceToken(xmlExportDirectory) : xmlExportDirectory;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given hl7 file location",
                    _scenarioContext.ScenarioInfo.Title + "Error - creating a variable for hl7 file location", e);
            }
            _scenarioContext.Remove("xmlFileLocation");
            _scenarioContext.Add("xmlFileLocation", newValue);
        }

        [Given(@"xml file prefix (.*)")]
        public void GivenXmlFilePrefix(string xmlFilePrefix)
        {
            var newValue = string.Empty;
            try
            {
                newValue = xmlFilePrefix.Contains("%") ? ReplaceToken(xmlFilePrefix) : xmlFilePrefix;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given xml file prefix",
                    _scenarioContext.ScenarioInfo.Title + "Error - creating a variable for xml file prefix", e);
            }
            _scenarioContext.Remove("xmlFilePrefix");
            _scenarioContext.Add("xmlFilePrefix", newValue);
        }

        [Given(@"xml file wait (.*) mins")]
        public void GivenXmlFileWaitMins(int xmlFileWaitTime)
        {
            var newValue = 0;
            try
            {
                newValue = xmlFileWaitTime;
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given xml file wait mins",
                    _scenarioContext.ScenarioInfo.Title + "Error - creating a variable for xml file wait mins", e);
            }
            _scenarioContext.Remove("xmlFileWaitTime");
            _scenarioContext.Add("xmlFileWaitTime", newValue);
        }
        #endregion


        #region When Steps

        [When(@"method (.*)")]
        public void WhenMethod(string p0)
        {
            APIResponse response = null;
            try
            {
                var url = "";
                switch (p0.ToLower())
                {
                    case "get":
                    case "get-http":
                        response = Helpers.Joystick.HttpRequests.GetRequest<object>((string)_scenarioContext["URL"],
                            _scenarioContext.ScenarioInfo.Title,
                            TestRun_Resources.BearerToken).Result;
                        break;
                    case "post":
                    case "post-http":
                        if ((string)_scenarioContext["HeaderContentType"] == "application/x-www-form-urlencoded")
                        {
                            response = Helpers.Joystick.HttpRequests.PostRequest<object, object>((string)_scenarioContext["URL"],
                                _scenarioContext.ScenarioInfo.Title,
                                TestRun_Resources.FormContent, TestRun_Resources.BearerToken,
                                TestRun_Resources.BasicAuth,
                                (string)_scenarioContext["HeaderContentType"]).Result;
                        }
                        else
                        {
                            response = Helpers.Joystick.HttpRequests.PostRequest<object, object>((string)_scenarioContext["URL"],
                                _scenarioContext.ScenarioInfo.Title,
                                (string)_scenarioContext["JSONRequestBody"], TestRun_Resources.BearerToken,
                                TestRun_Resources.BasicAuth,
                                (string)_scenarioContext["HeaderContentType"]).Result;
                        }
                        break;
                    case "put":
                    case "put-http":
                        if ((string)_scenarioContext["HeaderContentType"] == "application/x-www-form-urlencoded")
                        {
                            response = Helpers.Joystick.HttpRequests.PutRequest<object, object>((string)_scenarioContext["URL"],
                                _scenarioContext.ScenarioInfo.Title,
                                TestRun_Resources.FormContent, TestRun_Resources.BearerToken,
                                TestRun_Resources.BasicAuth,
                                (string)_scenarioContext["HeaderContentType"]).Result;
                        }
                        else if ((string)_scenarioContext["HeaderContentType"] == "application/json")
                        {
                            response = Helpers.Joystick.HttpRequests.PutRequest<object, object>((string)_scenarioContext["URL"],
                                _scenarioContext.ScenarioInfo.Title,
                                (string)_scenarioContext["JSONRequestBody"], TestRun_Resources.BearerToken,
                                TestRun_Resources.BasicAuth,
                                (string)_scenarioContext["HeaderContentType"]).Result;
                        }
                        else
                        {
                            response = Helpers.Joystick.HttpRequests.PutRequestNullContent<object>((string)_scenarioContext["URL"],
                                _scenarioContext.ScenarioInfo.Title, TestRun_Resources.BearerToken, TestRun_Resources.BasicAuth,
                                (string)_scenarioContext["HeaderContentType"]).Result;
                        }
                        break;
                    case "delete":
                    case "delete-http":
                        response = Helpers.Joystick.HttpRequests.DeleteRequest<object>((string)_scenarioContext["URL"],
                            _scenarioContext.ScenarioInfo.Title,
                            TestRun_Resources.BearerToken).Result;
                        break;
                    case "sqlserver-select":
                        response = Helpers.Joystick.SQLRequests.SqlServerRequest(_scenarioContext.ScenarioInfo.Title, (string)_scenarioContext["sqlConnectionString"],
                                                        (string)_scenarioContext["sqlQueryText"]);
                        break;
                    case "sqlserver-update":
                        response = Helpers.Joystick.SQLRequests.SqlServerUpdateRequest(_scenarioContext.ScenarioInfo.Title, (string)_scenarioContext["sqlConnectionString"],
                            (string)_scenarioContext["sqlQueryText"]);
                        break;
                    case "sqlserver-insert":
                        response = Helpers.Joystick.SQLRequests.SqlServerInsertRequest(_scenarioContext.ScenarioInfo.Title, (string)_scenarioContext["sqlConnectionString"],
                            (string)_scenarioContext["sqlQueryText"]);
                        break;
                    case "get-hl7-file":
                    case "get-hl7":
                        response = Helpers.Joystick.HL7Requests.GetHL7Request(_scenarioContext.ScenarioInfo.Title,
                            (string)_scenarioContext["hl7FileLocation"],
                            (string)_scenarioContext["hl7FilePrefix"], 
                            (int)_scenarioContext["hl7FileWaitTime"]);
                        break;
                    case "get-xml-file":
                    case "get-xml":
                        response = Helpers.Joystick.XMLRequests.GetXmlRequest(_scenarioContext.ScenarioInfo.Title,
                            (string)_scenarioContext["xmlFileLocation"],
                            (string)_scenarioContext["xmlFilePrefix"],
                            (int)_scenarioContext["xmlFileWaitTime"]);
                        break;
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - When method",
                    _scenarioContext.ScenarioInfo.Title + "Error - sending request", e);
            }

            _scenarioContext["response"] = response;

        }

        [When(@"hl7 file location delete")]
        public void WhenHlFileLocationDelete()
        {
            var fileLocation = _scenarioContext.Get<object>("hl7FileLocation").ToString();
            var filePrefix = _scenarioContext.Get<object>("hl7FilePrefix").ToString();
            var fileCriteria = string.Empty;

            try
            {
                if (fileLocation.IsNullOrEmpty())
                {
                    throw new Exception("hl7 file location is null or empty");
                }
                else
                {
                    fileLocation = fileLocation.Contains("%") ? ReplaceToken(fileLocation) : fileLocation;
                }

                if (!filePrefix.IsNullOrEmpty())
                {
                    filePrefix = filePrefix.Contains("%") ? ReplaceToken(filePrefix) : filePrefix;
                    fileCriteria = $"{filePrefix}*.hl7";
                }
                else
                {
                    fileCriteria = "*.hl7";
                }

                var fileList = System.IO.Directory.GetFiles(fileLocation, fileCriteria);
                foreach (var file in fileList)
                {
                    System.IO.File.Delete(file);
                }

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given hl7 file location delete",
                    _scenarioContext.ScenarioInfo.Title + "Error - deleting the contents of directory", e);
            }
        }

        [When(@"xml file location delete")]
        public void WhenXmlFileLocationDelete()
        {
            var fileLocation = _scenarioContext.Get<object>("xmlFileLocation").ToString();
            var filePrefix = _scenarioContext.Get<object>("xmlFilePrefix").ToString();
            var fileCriteria = string.Empty;

            try
            {
                if (fileLocation.IsNullOrEmpty())
                {
                    throw new Exception("xml file location is null or empty");
                }
                else
                {
                    fileLocation = fileLocation.Contains("%") ? ReplaceToken(fileLocation) : fileLocation;
                }

                if (!filePrefix.IsNullOrEmpty())
                {
                    filePrefix = filePrefix.Contains("%") ? ReplaceToken(filePrefix) : filePrefix;
                    fileCriteria = $"{filePrefix}*.xml";
                }
                else
                {
                    fileCriteria = "*.xml";
                }

                var fileList = System.IO.Directory.GetFiles(fileLocation, fileCriteria);
                foreach (var file in fileList)
                {
                    System.IO.File.Delete(file);
                }

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Given xml file location delete",
                    _scenarioContext.ScenarioInfo.Title + "Error - deleting the contents of directory", e);
            }
        }

        #endregion

        #region Then Steps

        [Then(@"assert json response (.*) is equal to (.*)")]
        public void Then_AssertJsonResponseValue(string responseValue, string expectedValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                expectedValue = expectedValue.Contains("%") ? ReplaceToken(expectedValue) : expectedValue;
                assertValue = ReplaceWithJsonResponseData($"response.{responseValue}");
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response is equal to ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting json response", e);

#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
                Debug.WriteLine("ExpectedValue equals " + expectedValue);
#endif
            }
            assertValue.Should().BeEquivalentTo(expectedValue);
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert json response (.*) contains (.*)")]
        public void Then_AssertJsonResponseContains(string responseValue, string expectedValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                expectedValue = expectedValue.Contains("%") ? ReplaceToken(expectedValue) : expectedValue;
                assertValue = ReplaceWithJsonResponseData($"response.{responseValue}");
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response contains ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting json response", e);


#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
                Debug.WriteLine("ExpectedValue equals " + expectedValue);
#endif
            }
            assertValue.Should().Contain(expectedValue);
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert json response (.*) does not contain (.*)")]
        public void Then_AssertJsonResponsetDoesNotContain(string responseValue, string value)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                value = value.Contains("%") ? ReplaceToken(value) : value;
                assertValue = ReplaceWithJsonResponseData($"response.{responseValue}");
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response contains ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting json response", e);

#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
                Debug.WriteLine("Value equals " + value);
#endif
            }
            value.Should().NotBeEquivalentTo(assertValue);
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert json response (.*) should be null")]
        public void Then_AssertJsonResponseIsNull(string responseValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                assertValue = ReplaceWithJsonResponseData($"response.{responseValue}");
            }
            catch (NullReferenceException e)
            {
                //TODO: Need to refactor this and Then_AssertJsonResponseShouldBeEmpty
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response does not contain",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting json response", e);
            }
            assertValue.Should().BeNull();
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert json response (.*) should be empty")]
        public void Then_AssertJsonResponseShouldBeEmpty(string responseValue)
        {
            var response = (APIResponse)_scenarioContext["response"];

            try
            {
                var assertValue = "";
                if (responseValue.Contains("%"))
                {
                    responseValue = ReplaceToken(responseValue);
                }
                JToken jt = JToken.Parse(response.ResponseBody);
                if (jt.Count() > 1)
                {
                    if (jt is JObject)
                    {
                        JObject jObject = JObject.Parse(response.ResponseBody);
                        assertValue = JsonXPathQuery(responseValue, jObject);
                    }
                    else
                    {
                        assertValue = JsonXPathQuery(responseValue, jt);
                    }
                }
                else
                {
                    assertValue = "";
                }

                assertValue.Should().BeEquivalentTo("");
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response should be empty",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting json response", e);
            }

            _scenarioContext["_response"] = response;
        }

        [Then(@"assert api response status is equal to (.*)")]
        public void Then_AssertApiResponseStatusIsEqualTo(string statusValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var actual = "";
            var expected = "";
            try
            {
                expected = statusValue;
                actual = Helpers.Joystick.HttpRequests.ConvertHttpStatusCode(response.ResponseCode);

                #if DEBUG
                Then_PrintResponseDebug(); 
                Debug.WriteLine("");
                #endif

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then api response status is equal to",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting api response", e);
            }
            actual.Should().BeEquivalentTo(expected);
        }

        [Then(@"print var (.*) debug")]
        public void Then_PrintVarDebug(string variableName)
        {
            try
            {
                var variable = _scenarioContext.Get<object>(variableName);
                Debug.WriteLine("var " + variableName.ToString() + " : " + variable.ToString());
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then print var debug ",
                    _scenarioContext.ScenarioInfo.Title + "Error - printing debug", e);
            }
        }

        [Then(@"print response debug")]
        public void Then_PrintResponseDebug()
        {
            var response = (APIResponse)_scenarioContext["response"];
            try
            {
                Debug.WriteLine("============================================");
                Debug.WriteLine("DEBUG: [Response Test Name] current value is: " + response.TestName);
                Debug.WriteLine("DEBUG: [Response Environment] current value is: " + response.Environment);
                Debug.WriteLine("DEBUG: [Response Request Type] current value is: " + response.RequestType);
                Debug.WriteLine("DEBUG: [Response Request Body] current value is: " + response.RequestBody);
                Debug.WriteLine("DEBUG: [Response Response Code] current value is: " + response.ResponseCode);
                Debug.WriteLine("DEBUG: [Response Response Duration] current value is: " + response.RequestDurationMilliseconds);
                Debug.WriteLine("DEBUG: [Response Response Body] current value is: " + response.ResponseBody);
                Debug.WriteLine("============================================");
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then print response debug",
                    _scenarioContext.ScenarioInfo.Title + "Error - printing api response", e);
            }
        }

        [Then(@"assert json response matches schema file (.*)")]
        public void Then_AssertJsonResponseMatchsSchemaFile(string jsonSchemaFile)
        {
            var response = (APIResponse)_scenarioContext["response"];
            JSchema schema = null;
            IList<string> validationErrors = new List<string>();
            try
            {
                using (StreamReader r = new StreamReader(jsonSchemaFile))
                {
                    schema = JSchema.Parse(r.ReadToEnd());
                    JObject json = JObject.Parse(response.ResponseBody);
                    var expected = json.IsValid(schema, out validationErrors);
                    foreach (string str in validationErrors) { Debug.WriteLine(str); }
                    Assert.True(json.IsValid(schema), validationErrors.ToString());

                }
            }
            catch (Newtonsoft.Json.JsonReaderException jre)
            {
                JToken json = JToken.Parse(response.ResponseBody);
                var expected = json.IsValid(schema, out validationErrors);
                foreach (string str in validationErrors) { Debug.WriteLine(str); }
                Assert.True(json.IsValid(schema), validationErrors.ToString());
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then json response matches schema file",
                    _scenarioContext.ScenarioInfo.Title + "Error - matching schema file", e);
            }
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert json response matches json file (.*)")]
        public void Then_AssertJsonResponseMatchesFile(string jsonFileName)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var json = "";

            try
            {
                using (StreamReader r = new StreamReader(jsonFileName))
                {
                    json = r.ReadToEnd();
                }
                JObject expected = JObject.Parse(json);
                JObject actual = JObject.Parse(response.ResponseBody);
                expected.Should().BeEquivalentTo(actual);
            }
            catch (Newtonsoft.Json.JsonReaderException jre)
            {
                JToken expected = JToken.Parse(json);
                JToken actual = JToken.Parse(response.ResponseBody);
                expected.Should().BeEquivalentTo(actual);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response matches json file",
                    _scenarioContext.ScenarioInfo.Title + "Error - matching json file", e);
            }
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert hl7 file location is empty")]
        public void ThenAssertHlFileLocationIsEmpty()
        {
            var fileLocation = _scenarioContext.Get<object>("hl7FileLocation").ToString();
            var filePrefix = _scenarioContext.Get<object>("hl7FilePrefix").ToString();
            var fileCriteria = string.Empty;

            try
            {
                if (fileLocation.IsNullOrEmpty())
                {
                    throw new Exception("hl7 file location is null or empty");
                }
                else
                {
                    fileLocation = fileLocation.Contains("%") ? ReplaceToken(fileLocation) : fileLocation;
                }

                if (!filePrefix.IsNullOrEmpty())
                {
                    filePrefix = filePrefix.Contains("%") ? ReplaceToken(filePrefix) : filePrefix;
                    fileCriteria = $"{filePrefix}*.hl7";
                }
                else
                {
                    fileCriteria = "*.hl7";
                }

                var fileList = System.IO.Directory.GetFiles(fileLocation, fileCriteria);
                Assert.True(fileList.Length == 0);

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - When assert hl7 file location is empty",
                    _scenarioContext.ScenarioInfo.Title + "Error - verifying that hl7 location is empty", e);
            }
        }

        [Then(@"assert hl7 response (.*) should be empty")]
        public void ThenAssertHlResponseMSH_ShouldBeEmpty(string expectedValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                assertValue = HL7Message.ParseOnly(response.ResponseBody, expectedValue);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert hl7 should be empty ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting hl7 response", e);

#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
#endif
            }

            assertValue.Should().BeNullOrEmpty();
            _scenarioContext["_response"] = response;
        }



        [Then(@"assert hl7 response (.*) is equal to (.*)")]
        public void ThenAssertHlResponseIsEqualTo(string responseValue, string expectedValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                expectedValue = expectedValue.Contains("%") ? ReplaceToken(expectedValue) : expectedValue;
                assertValue = HL7Message.ParseOnly(response.ResponseBody, responseValue);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response is equal to ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting json response", e);

#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
                Debug.WriteLine("ExpectedValue equals " + expectedValue);
#endif
            }
            assertValue.Should().BeEquivalentTo(expectedValue);
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert hl7 response (.*) contains (.*)")]
        public void ThenAssertHlResponseContains(string responseValue, string expectedValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                expectedValue = expectedValue.Contains("%") ? ReplaceToken(expectedValue) : expectedValue;
                assertValue = HL7Message.ParseOnly(response.ResponseBody, responseValue);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert json response contains ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting json response", e);


#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
                Debug.WriteLine("ExpectedValue equals " + expectedValue);
#endif
            }
            assertValue.Should().Contain(expectedValue);
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert xml file location is empty")]
        public void ThenAssertXmlFileLocationIsEmpty()
        {
            var fileLocation = _scenarioContext.Get<object>("hl7FileLocation").ToString();
            var filePrefix = _scenarioContext.Get<object>("hl7FilePrefix").ToString();
            var fileCriteria = string.Empty;

            try
            {
                if (fileLocation.IsNullOrEmpty())
                {
                    throw new Exception("xml file location is null or invalid");
                }
                else
                {
                    fileLocation = fileLocation.Contains("%") ? ReplaceToken(fileLocation) : fileLocation;
                }

                if (!filePrefix.IsNullOrEmpty())
                {
                    filePrefix = filePrefix.Contains("%") ? ReplaceToken(filePrefix) : filePrefix;
                    fileCriteria = $"{filePrefix}*.xml";
                }
                else
                {
                    fileCriteria = "*.hl7";
                }

                var fileList = System.IO.Directory.GetFiles(fileLocation, fileCriteria);
                Assert.True(fileList.Length == 0);

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - When assert xml file location is empty",
                    _scenarioContext.ScenarioInfo.Title + "Error - verifying that xml location is empty", e);
            }
        }

        //TODO: Marked for Refactor on second parse
        [Then(@"assert xml response (.*) is equal to (.*)")]
        public void ThenAssertXmlResponseIsEqualTo(string responseValue, string expectedValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                expectedValue = expectedValue.Contains("%") ? ReplaceToken(expectedValue) : expectedValue;
                var xml = new XmlDocument();
                xml.LoadXml(response.ResponseBody);
                var json = JsonConvert.SerializeXmlNode(xml);
                JObject jo = JObject.Parse(json);
                assertValue = JsonXPathQuery(responseValue, jo);

            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert xml response is equal to ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting xml response", e);

#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
                Debug.WriteLine("ExpectedValue equals " + expectedValue);
#endif
            }
            assertValue.Should().BeEquivalentTo(expectedValue);
            _scenarioContext["_response"] = response;
        }

        [Then(@"assert xml response (.*) contains (.*)")]
        public void ThenAssertXmlResponseContains(string responseValue, string expectedValue)
        {
            var response = (APIResponse)_scenarioContext["response"];
            var assertValue = "";
            try
            {
                expectedValue = expectedValue.Contains("%") ? ReplaceToken(expectedValue) : expectedValue;
                var xml = new XmlDocument();
                xml.LoadXml(response.ResponseBody);
                var json = JsonConvert.SerializeXmlNode(xml);
                JObject jo = JObject.Parse(json);
                assertValue = JsonXPathQuery(responseValue, jo);
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Then assert xml response contains ",
                    _scenarioContext.ScenarioInfo.Title + "Error - asserting xml response", e);


#if DEBUG
                Debug.WriteLine("AssertValue equals " + assertValue);
                Debug.WriteLine("ExpectedValue equals " + expectedValue);
#endif
            }
            assertValue.Should().Contain(expectedValue);
            _scenarioContext["_response"] = response;
        }
        #endregion


        public string JsonXPathQuery(string query, JObject jObject)
        {
            var value = jObject.SelectToken(query);
            return (string)jObject.SelectToken(query);
        }

        public string JsonXPathQuery(string query, JToken jObject)
        {
            return (string)jObject.SelectToken(query);
        }

        public JObject JsonJObjectQuery(string query, JObject jObject)
        {
            return (JObject)jObject.SelectToken(query);
        }
        public JObject JsonJObjectQuery(string query, JToken jObject)
        {
            return (JObject)jObject.SelectToken(query);
        }

        public string ReplaceToken(string value)
        {
            foreach (var key in _scenarioContext.Keys)
            {
                if (value.Contains(key))
                {
                    value = value.Replace(key, _scenarioContext[key].ToString());
                }
            }
            return value;
        }

        public string ReplaceWithJsonResponseData(string variableValue)
        {
            JToken jt = null;
            try
            {
                if (variableValue.Contains("response."))
                {
                    var response = (APIResponse)_scenarioContext["response"];
                    var queryString = variableValue.Split("response.").Last();
                    jt = JToken.Parse(response.ResponseBody);
                    if (jt is JObject)
                    {
                        JObject jObject = JObject.Parse(response.ResponseBody);
                        variableValue = JsonXPathQuery(queryString, jObject);
                    }
                    else
                    {
                        jt = JToken.Parse(response.ResponseBody);
                        variableValue = JsonXPathQuery(queryString, jt);
                    }
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Error - ReplaceWithJsonResponseData",
                    _scenarioContext.ScenarioInfo.Title + $"Error - variableName: {variableValue} - ", e);
            }
            return variableValue;
        }

        public object ReplaceWithJsonResponseJSONObject(string variableValue)
        {
            JToken jt = null;
            try
            {
                if (variableValue.Contains("response."))
                {
                    var response = (APIResponse)_scenarioContext["response"];
                    var queryString = variableValue.Split("response.").Last();
                    jt = JToken.Parse(response.ResponseBody);
                    if (jt is JObject)
                    {
                        JObject jObject = JObject.Parse(response.ResponseBody);
                        variableValue = JsonJObjectQuery(queryString, jObject).ToString();
                    }
                    else
                    {
                        jt = JToken.Parse(response.ResponseBody);
                        variableValue = JsonJObjectQuery(queryString, jt).ToString();
                    }
                }
            }
            catch (Exception e)
            {
                TestRun_Resources.DebugReportError("Joystick API - Error - ReplaceWithJsonResponseData",
                    _scenarioContext.ScenarioInfo.Title + $"Error - variableName: {variableValue} - ", e);
            }
            return variableValue;
        }
    }
}

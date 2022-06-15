using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Automation.Domain.Models.Joystick;
using Automation.Domain.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.Helpers.Joystick
{
    public class HttpRequests
    {
        #region Get Requests
        public static async Task<APIResponse> GetRequest<T>(string uri, string scenarioTitle, string bearer = null, string basic = null)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(bearer))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                    }

                    if (!string.IsNullOrEmpty(basic))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
                    }

                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    using (HttpResponseMessage response = await client.GetAsync(uri))
                    {
                        apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                        stopWatch.Stop();
                        apiResponse.TestName = scenarioTitle;
                        apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        apiResponse.RequestType = "HttpRequest-GET";
                        apiResponse.RequestBody = uri;
                        apiResponse.ResponseCode = response.StatusCode.ToString();
                        apiResponse.ResponseBody = responseBody;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return apiResponse;

        }
        #endregion

        #region Post Requests
        public static async Task<APIResponse> PostRequest<TIn, TOut>(string uri, string scenarioTitle, TIn content, string bearer = null,
            string basic = null, string headerValue = null)
        {
            var apiResponse = new APIResponse();
            HttpContent postContent = null;
            JObject jo = null;
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    if (!string.IsNullOrEmpty(bearer))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                        basic = null;
                    }

                    if (!string.IsNullOrEmpty(basic))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
                    }

                    if (headerValue != null)
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerValue));
                        switch (headerValue)
                        {
                            case "application/json":
                                jo = JObject.Parse(content.ToString());
                                postContent = new StringContent(JsonConvert.SerializeObject(jo), Encoding.UTF8, "application/json");
                                break;
                            case "application/x-www-form-urlencoded":
                                List<KeyValuePair<string, string>> bodyProperties = new List<KeyValuePair<string, string>>();
                                foreach (var key in TestRun_Resources.FormContent)
                                {
                                    bodyProperties.Add(new KeyValuePair<string, string>(key.Key, key.Value));
                                }
                                postContent = new FormUrlEncodedContent(bodyProperties.ToArray());
                                break;
                        }
                    }


                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    using (HttpResponseMessage response = await client.PostAsync(uri, postContent))
                    {
                        apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                        stopWatch.Stop();
                        apiResponse.TestName = scenarioTitle;
                        apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        apiResponse.RequestType = "HttpRequest-POST";
                        apiResponse.ResponseCode = response.StatusCode.ToString();
                        if (headerValue == "application/json")
                        {
                            apiResponse.RequestBody = JsonConvert.SerializeObject(jo).Replace("\"", "'");
                        }
                        else
                        {
                            apiResponse.RequestBody = JsonConvert.SerializeObject(TestRun_Resources.FormContent.ToString());
                        }
                        apiResponse.ResponseBody = responseBody.Replace("\"", "'");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return apiResponse;
        }
        #endregion

        #region Put Requests
        public static async Task<APIResponse> PutRequest<TIn, TOut>(string uri, string scenarioTitle, TIn content, string bearer = null,
            string basic = null,
            string headerValue = null)
        {
            APIResponse apiResponse = new APIResponse();
            HttpContent postContent = null;
            JObject jo = null;
            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    if (!string.IsNullOrEmpty(bearer))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                    }

                    if (!string.IsNullOrEmpty(basic))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
                    }

                    if (headerValue != null)
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerValue));
                        switch (headerValue)
                        {
                            case "application/json":
                                jo = JObject.Parse(content.ToString());
                                postContent = new StringContent(JsonConvert.SerializeObject(jo), Encoding.UTF8, "application/json");
                                break;
                            case "application/x-www-form-urlencoded":
                                List<KeyValuePair<string, string>> bodyProperties = new List<KeyValuePair<string, string>>();
                                foreach (var key in TestRun_Resources.FormContent)
                                {
                                    bodyProperties.Add(new KeyValuePair<string, string>(key.Key, key.Value));
                                }
                                postContent = new FormUrlEncodedContent(bodyProperties.ToArray());
                                break;
                        }
                    }

                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    using (HttpResponseMessage response = await client.PutAsync(uri, postContent))
                    {
                        stopWatch.Stop();
                        apiResponse.TestName = scenarioTitle;
                        apiResponse.RequestType = "HttpRequest-PUT";
                        apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        apiResponse.ResponseCode = response.StatusCode.ToString();
                        if (headerValue == "application/json")
                        {
                            apiResponse.RequestBody = JsonConvert.SerializeObject(jo).Replace("\"", "'");
                        }
                        else
                        {
                            apiResponse.RequestBody = JsonConvert.SerializeObject(TestRun_Resources.FormContent.ToString());
                        }
                        apiResponse.ResponseBody = responseBody.Replace("\"", "'");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return apiResponse;
        }

        public static async Task<APIResponse> PutRequestNullContent<TOut>(string uri, string scenarioTitle, string bearer = null,
            string basic = null,
            string headerValue = null)
        {
            APIResponse apiResponse = new APIResponse();
            HttpContent postContent = null;

            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    if (!string.IsNullOrEmpty(bearer))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                    }

                    if (!string.IsNullOrEmpty(basic))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
                    }

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    using (HttpResponseMessage response = await client.PutAsync(uri, postContent))
                    {
                        stopWatch.Stop();
                        apiResponse.TestName = scenarioTitle;
                        apiResponse.RequestType = "HttpRequest-PUT";
                        apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        apiResponse.ResponseCode = response.StatusCode.ToString();
                        apiResponse.RequestBody = uri.ToString();
                        apiResponse.ResponseBody = responseBody.Replace("\"", "'");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return apiResponse;
        }
        #endregion

        #region Delete Requests
        public static async Task<APIResponse> DeleteRequest<T>(string uri, string scenarioTitle, string bearer = null, string basic = null)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(bearer))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
                    }

                    if (!string.IsNullOrEmpty(basic))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);
                    }

                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    using (HttpResponseMessage response = await client.DeleteAsync(uri))
                    {
                        apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                        stopWatch.Stop();
                        apiResponse.TestName = scenarioTitle;
                        apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        apiResponse.RequestType = "HttpRequest-DELETE";
                        apiResponse.ResponseCode = response.StatusCode.ToString();
                        apiResponse.ResponseBody = responseBody;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return apiResponse;

        }
        #endregion

        public static string ConvertHttpStatusCode(string value)
        {
            var _return = "";
            switch (value)
            {
                case "OK":
                    _return = "200";
                    break;
                case "Created":
                    _return = "201";
                    break;
                case "NoContent":
                    _return = "204";
                    break;
                case "BadRequest":
                    _return = "400";
                    break;
                case "NotFound":
                    _return = "404";
                    break;
                case "MethodNotAllowed":
                    _return = "405";
                    break;

            }
            return _return;
        }
    }
}

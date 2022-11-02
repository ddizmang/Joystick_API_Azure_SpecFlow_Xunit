using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Domain.Models.Joystick;
using Automation.Domain.Resources;

namespace Automation.Helpers.Joystick
{
    public static class SQLRequests
    {
        public static APIResponse SqlServerRequest(string scenarioTitle, string sqlConnection, string sqlQuery)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = sqlConnection;
                    conn.Open();
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, conn);
                    using (SqlDataReader sdr = sqlCommand.ExecuteReader())
                    {
                        apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                        stopWatch.Stop();
                        sdr.Read();
                        apiResponse.TestName = scenarioTitle;
                        apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                        apiResponse.RequestType = "SqlServerRequest-JSON";
                        apiResponse.RequestBody = sqlQuery;
                        apiResponse.ResponseCode = "Success";
                        apiResponse.ResponseBody = sdr.GetValue(0).ToString().Replace("\"", "'");
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return apiResponse;
        }

        public static APIResponse SqlServerUpdateRequest(string scenarioTitle, string sqlConnection, string sqlQuery)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = sqlConnection;
                    conn.Open();
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, conn);
                    var resultCount = sqlCommand.ExecuteNonQuery();
                    apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                    stopWatch.Stop();
                    apiResponse.TestName = scenarioTitle;
                    apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                    apiResponse.RequestType = "SqlServerRequest-Update";
                    apiResponse.RequestBody = sqlQuery;
                    apiResponse.ResponseCode = "Success";
                    apiResponse.ResponseBody = $"{resultCount} : rows updated";
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return apiResponse;
        }

        public static APIResponse SqlServerInsertRequest(string scenarioTitle, string sqlConnection, string sqlQuery)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = sqlConnection;
                    conn.Open();
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, conn);
                    var resultCount = sqlCommand.ExecuteNonQuery();
                    apiResponse.RequestDurationMilliseconds = stopWatch.ElapsedMilliseconds.ToString();
                    stopWatch.Stop();
                    apiResponse.TestName = scenarioTitle;
                    apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                    apiResponse.RequestType = "SqlServerRequest-Insert";
                    apiResponse.RequestBody = sqlQuery;
                    apiResponse.ResponseCode = "Success";
                    apiResponse.ResponseBody = $"{resultCount} : rows updated";
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return apiResponse;
        }
    }
}

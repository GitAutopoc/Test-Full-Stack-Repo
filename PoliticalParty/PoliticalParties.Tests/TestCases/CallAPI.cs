using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoliticalParties.Tests.TestCases
{
    public class CallAPI
    {
        public static string UniqueGuid = "18f69543-da90-412c-8a01-4825f31340bb";


        public static async Task<string> saveTestResult(string testName, string status, string type)
        {
            TestResults testResults = new TestResults();
            Dictionary<string, TestCaseResultDto> testCaseResults = new Dictionary<string, TestCaseResultDto>();
            string customValue = System.IO.File.ReadAllText("../../../../../custom.ih");
            testResults.CustomData = customValue;
            int actualScore = 0;
            String testStatus = "Failed";
            if (status.Equals("True"))
            {
                actualScore = 1;
                testStatus = "Passed";
            }

            testCaseResults.Add(UniqueGuid, new TestCaseResultDto
            {
                MethodName = testName,
                MethodType = type,
                EarnedScore = 1,
                ActualScore = actualScore,
                Status = testStatus,
                IsMandatory = true
            });

            using (HttpClient _httpClient = new HttpClient())
            {
                testResults.TestCaseResults = JsonConvert.SerializeObject(testCaseResults);
                var testResultsJson = JsonConvert.SerializeObject(testResults);
                await _httpClient.PostAsync("https://yaksha-stage-sbfn-s2-vm.azurewebsites.net/api/YakshaMFAEnqueue?code=MLo1s4eRhiP_Z2EJr3Y38EDFPQ5NUfOu4snUcWTC5tU3AzFuQZCG2Q==", new StringContent(testResultsJson, Encoding.UTF8, "application/json"));
            }
            return status;
        }

        public static string GetCurrentMethodName([System.Runtime.CompilerServices.CallerMemberName] string name = "")
        {
            return name;
        }

    }
}

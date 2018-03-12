using System.Collections.Generic;
using System.Threading.Tasks;
using EnvBuild.Config;
using EnvBuild.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace EnvBuild.Controllers
{
    [Route("api/[controller]")]
    public class GitController : Controller
    {
        private AuthConfig _authConfig { get; set; }

        private GitConfig _gitConfig { get; set; }
        public GitController(AuthConfig authConfig, GitConfig gitConfig)
        {
            _authConfig = authConfig;
            _gitConfig = gitConfig;
        }

        [HttpGet("repos")]
        public async Task<GitResponse> GetRepository()
        {
            List<GitRepositoryDto> repositories = new List<GitRepositoryDto>();
            RestClient restClient = new RestClient(_gitConfig.RepositoryUrl);
            RestRequest request = new RestRequest(Method.GET);
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = restClient.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            RestResponse response = (RestResponse)(await taskCompletion.Task);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                repositories = JsonConvert.DeserializeObject<List<GitRepositoryDto>>(response.Content);
            GitResponse resp = new GitResponse()
            {
                Repositories = repositories,
                User = new UserInfo()
                {
                    Email = _authConfig.UserName,
                    UserName = _authConfig.Email
                }
            };
            return resp;
        }

    }
}

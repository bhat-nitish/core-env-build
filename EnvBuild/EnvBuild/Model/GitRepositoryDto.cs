using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvBuild.Model
{
    public class GitResponse
    {
        public IList<GitRepositoryDto> Repositories { get; set; }

        public UserInfo User { get; set; }

        public GitResponse()
        {
            Repositories = new List<GitRepositoryDto>();
            User = new UserInfo();
        }
    }
    public class GitRepositoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public Owner Owner { get; set; }

        public bool Private { get; set; }

        public GitRepositoryDto()
        {
            Owner = new Owner();
        }
    }

    public class UserInfo
    {
        public string UserName { get; set; }

        public string Email { get; set; }
    }

    public class Owner
    {
        public string Login { get; set; }

        public long Id { get; set; }
    }
}

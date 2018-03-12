# core-env-build
Maintain a single appsettings.json and configure it dynamically with build params.

1. Clone the repository and navigate to core-env-build/EnvBuild folder which has the EnvBuild.csprog file. Run dotnet restore to install all dependencies.

2. The RepositoryUrl , UserName and Email parameters are configurable. The default values are in appsettings.json.

3. Only a single appsettings.json file is maintained and the same gets updated with build params.

## Dependencies :

Dotnet Core Framework [2.0]

## Example :
  
Pass params with dotnet run

* View Repositories of Nitish Bhat with username and email of Nitish Bhat : [navigate to folder containing .csproj file]
     1. dotnet build
     2. dotnet run AuthConfig:UserName=Peter AuthConfig:Email=peter@mail.com GitConfig:RepositoryUrl=https://api.github.com/users/bhat-nitish/repos

* View Repositories of any other public repository with a username and email : [navigate to folder containing .csproj file] :  
     1. dotnet build
     2. dotnet run AuthConfig:UserName=Nitish AuthConfig:Email=nitish@mail.com GitConfig:RepositoryUrl=https://api.github.com/users/angular/repos
 
## Verifying that the config has changed

 1. Navigate to localhost:5000/api/git/repos on either postman or chrome after dotnet build and dotnet run. The results of the repository url and the username/email details change.

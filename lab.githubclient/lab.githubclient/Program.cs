// https://www.nuget.org/packages/Octokit.Reactive/4.0.2/ReportAbuse

using Octokit;
using System.Text;

Console.WriteLine("----------get repo using c# octokit----------");

try
{
    Console.WriteLine("----------start----------");

    string gitHubRepoName = "githubclient-store";

    //get repo: githubclient-store
    var gitHubClient = new GitHubClient(new ProductHeaderValue(gitHubRepoName));

    //Not working
    //var basicAuth = new Credentials("raselahmmed@hotmail.com", "RaB!@#9885"); // NOTE: not real credentials
    //gitHubClient.Credentials = basicAuth;

    var tokenAuth = new Credentials("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"); // NOTE: not real token
    gitHubClient.Credentials = tokenAuth;

    var currentUser = await gitHubClient.User.Current();

    var (owner, repoName, filePath, branch) = (currentUser.Login, gitHubRepoName,
            "employee.txt", "main");

    ////create
    //var createTxt = new StringBuilder("----------");
    //createTxt.AppendLine();
    //createTxt.AppendLine($"date: {DateTime.Now.ToShortDateString()}");
    //createTxt.AppendLine($"name: rasel");
    //createTxt.AppendLine($"email: rasel@gmail.com");
    //createTxt.AppendLine("----------");
    //createTxt.AppendLine();

    //var createResult = await gitHubClient.Repository.Content.CreateFile(
    //     owner, repoName, filePath,
    //     new CreateFileRequest($"create txt file: {filePath}", createTxt.ToString(), branch));
    ////create

    //update
    //var updateCommit = createResult.Commit.Sha;

    var updateTxt = new StringBuilder("----------");
    updateTxt.AppendLine();
    updateTxt.AppendLine($"date: {DateTime.Now.ToShortDateString()}");
    updateTxt.AppendLine($"name: bappi");
    updateTxt.AppendLine($"email: bappi@gmail.com");
    updateTxt.AppendLine("----------");
    updateTxt.AppendLine();

    //await gitHubClient.Repository.Content.UpdateFile(owner, repoName, filePath,
    //    new UpdateFileRequest($"updated txt file: {filePath}", updateTxt.ToString(), updateCommit));

    var fileRawContent = await gitHubClient.Repository.Content.GetRawContentByRef(owner, repoName,
    filePath, branch);

    if (fileRawContent != null)
    {
        
    }

    var fileContents = await gitHubClient.Repository.Content.GetAllContentsByRef(owner, repoName,
    filePath, branch);

    var updateResult = await gitHubClient.Repository.Content.UpdateFile(owner, repoName, filePath,
        new UpdateFileRequest("updated txt file: {filePath}", updateTxt.ToString(), fileContents.First().Sha));

    //update

    Console.WriteLine("----------end----------");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message.ToString());
}

Console.ReadLine();
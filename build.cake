var target = Argument<string>("target", "build");

Task("clean")
    .Does(() =>
{
    CleanDirectories("./**/^{bin,obj}");
});

Task("restore")
    .IsDependentOn("clean")
    .Does(() =>
{
    DotNetCoreRestore("./ConsoleApp/ConsoleApp.sln", new DotNetCoreRestoreSettings
    {
        LockedMode = true,
    });
});

Task("build")
    .IsDependentOn("restore")
    .DoesForEach(new[] { "Debug", "Release" }, (configuration) =>
{
    DotNetCoreBuild("./ConsoleApp/ConsoleApp.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        NoRestore = true,
        NoIncremental = false,
    });
});

RunTarget(target);

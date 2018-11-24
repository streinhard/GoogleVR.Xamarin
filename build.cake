#addin nuget:?package=SharpZipLib
#addin nuget:?package=Cake.Compression

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solution = "./src/GoogleVR.Xamarin.sln";

var ANDROID_URL = "https://github.com/googlevr/gvr-android-sdk/archive/v1.170.tar.gz";
var ANDROID_ARCHIVE = "./externals/gvr-android-sdk-1.170.tar.gz";

var PROTOBUF_URL = "https://repo1.maven.org/maven2/com/google/protobuf/nano/protobuf-javanano/3.1.0/protobuf-javanano-3.1.0.jar";
var PROTOBUF_ARCHIVE = "./externals/protobuf-javanano-3.1.0.jar";

var IOS_URL = "https://dl.google.com/dl/cpdc/dc01e783d2390012/GVRSDK-1.170.0.tar.gz";
var IOS_ARCHIVE = "./externals/GVRSDK-1.170.0.tar.gz";



Task("clean")
    .Does(() =>
{
  CleanDirectories("./src/**/obj");
  CleanDirectories("./src/**/bin");
});

Task("nugets")
  .IsDependentOn("clean")
  .Does(() =>
{
    NuGetRestore(solution);
});

Task("externals")
  .Does(() =>
{
  if (!FileExists(ANDROID_ARCHIVE))
  {
    DownloadFile(ANDROID_URL, ANDROID_ARCHIVE);
    GZipUncompress(ANDROID_ARCHIVE, "./externals");
  }

  if (!FileExists(PROTOBUF_ARCHIVE))
  {
    DownloadFile(PROTOBUF_URL, PROTOBUF_ARCHIVE);
  }

  if (!FileExists(IOS_ARCHIVE))
  {
    DownloadFile(IOS_URL, IOS_ARCHIVE);
    GZipUncompress(IOS_ARCHIVE, "./externals/gvr-ios-sdk-1.170.0");
  }
});


Task("build")
  .IsDependentOn("nugets")
  .Does(() =>
{
  MSBuild(solution, settings =>
    settings.SetConfiguration(configuration));
});

Task("Default").IsDependentOn("build");

RunTarget(target);

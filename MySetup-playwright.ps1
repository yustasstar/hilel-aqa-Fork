param (
    [Parameter(Mandatory=$true)]
    [string]$ProjectName
)

if (-not $ProjectName) {
    Write-Host "Usage: .\setup-playwright.ps1 -ProjectName <YourProjectName>"
    exit
}

Write-Host "Step 1: Create a new NUnit project with the specified name"
dotnet new nunit -n $ProjectName

Write-Host "Step 2: Change directory to the newly created project folder"
cd $ProjectName

Write-Host "Step 3: Add Microsoft.Playwright.NUnit package"
dotnet add package Microsoft.Playwright.NUnit

Write-Host "Step 4: Build the project"
dotnet build

Write-Host "Step 5: Install Playwright"
./bin/Debug/net8.0/playwright.ps1 install

Write-Host "Step 6: Replace content of UnitTest1.cs with the specified text"
$unitTestPath = "FirstTest.cs"
$fixturePath = "UITestFixture.cs"

$unitTestContent = @"
using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace $ProjectName
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class FirstTest : UITestFixture
    {
        [Test]
        public async Task HasTitle()
        {
            await Page.GotoAsync(baseUrl);
            await Assertions.Expect(Page).ToHaveTitleAsync(new Regex("Automation Exercise"));
        }

        [Test]
        public async Task GotoLogIn()
        {
            await Page.GotoAsync(baseUrl);
            await Page.GetByRole(AriaRole.Link, new() { Name = "Signup / Login" }).ClickAsync();
            await Assertions.Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Login to your account" })).ToBeVisibleAsync();
        } 
    }
}
"@

$fixtureContent = @"
using Microsoft.Playwright;

namespace $ProjectName
{
    public class UITestFixture
    {
        private static IBrowser? Browser;
        public static IBrowserContext? Context { get; private set; }
        public static IPage? Page { get; private set; }
        internal static string baseUrl = "https://automationexercise.com/";

        [SetUp]
        public async Task Setup()
        {
            var playwrightDriver = await Playwright.CreateAsync();
            Browser = await playwrightDriver.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1885, Height = 945 },
            });

            await Context.Tracing.StartAsync(new()
            {
                Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            Page = await Context.NewPageAsync();
            Page.SetDefaultTimeout(15000);
            //Page.PauseAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
			var failed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;
           
            if (Context != null)
            {
                await Context.Tracing.StopAsync(new()
                {
                    Path = failed ? Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "playwright-traces",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                ) : null,
                });
            }
            if (Page != null) { await Page.CloseAsync(); }
            if (Browser != null) { await Browser.CloseAsync(); }
        }
    }
}
"@

Set-Content -Path $unitTestPath -Value $unitTestContent
Set-Content -Path $fixturePath -Value $fixtureContent

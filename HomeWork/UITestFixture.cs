using Microsoft.Playwright;

namespace HomeWork
{
    public class UITestFixture
    {
        public IPage? Page { get; private set; }
        private IBrowser? Browser;
        public IBrowserContext? Context { get; private set; }

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
            Page.SetDefaultTimeout(10000);
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

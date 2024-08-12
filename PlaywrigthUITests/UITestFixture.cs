using Microsoft.Playwright;

namespace PlaywrigthUITests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class UITestFixture
    {
        public IPage Page { get; private set; }
        private IBrowser Browser;

        [SetUp]
        public async Task Setup()
        {
            var playwrightDriver = await Playwright.CreateAsync();
            Browser = await playwrightDriver.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1890, Height = 945 },
            });

            Page = await context.NewPageAsync();
        }

        [OneTimeSetUp]

        [TearDown]
        public async Task Teardown()
        {
            if (Page != null) { await Page.CloseAsync(); }
            if (Browser != null) { await Browser.CloseAsync(); }
        }
    }
}

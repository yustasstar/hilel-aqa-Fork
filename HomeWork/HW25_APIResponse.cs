using Microsoft.Playwright;
using System.Text.Json.Nodes;

namespace HomeWork
{
    internal class HW25_APIResponse : UITestFixture
    {
        [Test]
        public async Task ReplaceResponse()
        {
            await Page.RouteAsync("*/**/api/v1/fruits", async route =>
            {
                var response = await route.FetchAsync();
                var body = await response.BodyAsync();
                var jn = JsonNode.Parse(body);
                JsonArray ja = jn.AsArray();
                ja[1]["name"] = "MY NEW NAME";

                await route.FulfillAsync(new() { Response = response, Json = ja });
            });

            await Page.GotoAsync("https://demo.playwright.dev/api-mocking");
            await Assertions.Expect(Page.GetByText("MY NEW NAME")).ToBeVisibleAsync();
        }

        [Test]
        public async Task RemoveAfterLastFruit()
        {
            await Page.RouteAsync("*/**/api/v1/fruits", async route =>
            {
                var response = await route.FetchAsync();
                var body = await response.BodyAsync();
                var jn = JsonNode.Parse(body);
                JsonArray ja = jn.AsArray();

                int index = -1;
                for (int i = 0; i < ja.Count; i++)
                {
                    if (ja[i]?["name"]?.ToString() == "Orange")
                    {
                        index = i;
                        break;
                    }
                }

                if (index != -1)
                {
                    ja[index]["name"] = "LAST FRUIT";
                    while (ja.Count > index + 1)
                    {
                        ja.RemoveAt(index + 1);
                    }
                }
                await route.FulfillAsync(new() { Response = response, Json = ja });
            });

            await Page.GotoAsync("https://demo.playwright.dev/api-mocking");

            await Assertions.Expect(Page.GetByText("LAST FRUIT")).ToBeVisibleAsync();
            var allFruits = await Page.Locator("li").AllTextContentsAsync();
            Assert.That(allFruits.Last(), Is.EqualTo("LAST FRUIT"), "The last fruit is not 'LAST FRUIT'");
        }

        [Test]
        public async Task RemoveAfterLastFruitList()
        {
            await Page.RouteAsync("*/**/api/v1/fruits", async route =>
            {
                var response = await route.FetchAsync();
                var body = await response.BodyAsync();
                var jn = JsonNode.Parse(body);
                JsonArray ja = jn.AsArray();

                var result = new List<JsonNode>();
                for (int i = 0; i < ja.Count; i++)
                {
                    if (ja[i]?["name"]?.ToString() == "Orange")
                    {
                        ja[i]["name"] = "LAST FRUIT";
                        result.Add(ja[i]);
                        break;
                    }
                    result.Add(ja[i]);
                }

                await route.FulfillAsync(new() { Response = response, Json = result });
            });

            await Page.GotoAsync("https://demo.playwright.dev/api-mocking");

            await Assertions.Expect(Page.GetByText("LAST FRUIT")).ToBeVisibleAsync();
            var allFruits = await Page.Locator("li").AllTextContentsAsync();
            Assert.That(allFruits.Last(), Is.EqualTo("LAST FRUIT"), "The last fruit is not 'LAST FRUIT'");
        }
    }
}

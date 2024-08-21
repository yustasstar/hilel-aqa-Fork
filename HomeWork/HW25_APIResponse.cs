using HW24_Evershop;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HomeWork
{
    internal class HW25_APIResponse : UITestFixture
    {
        [Test]
        public async Task ReplaceResponse()
        {
            await Page.RouteAsync("*/**/api/v1/fruits", async route => {
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

    }
}

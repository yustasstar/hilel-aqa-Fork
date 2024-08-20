using Microsoft.Playwright;
using System;

namespace HW24_Evershop
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ExampleTest : UITestFixture
    {

        [Test]
        public async Task CheckoutWorkflow()
        {
            await Page.APIRequest.PostAsync("https://demo.evershop.io//customer/login", new()
            {
                DataObject = new Dictionary<string, object>
                {
                    {"email", "TestEmail@mail.test" },
                    {"password", "P@ssword123" }
                }
            });

            await Page.GotoAsync("https://demo.evershop.io/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Men", Exact = true }).ClickAsync();
            await Page.GetByRole(AriaRole.Link, new() { Name = "Nike air zoom pegasus" }).Nth(3).ClickAsync();
            await Page.WaitForTimeoutAsync(1000);
            await Page.GetByRole(AriaRole.Link, new() { Name = "M", Exact = true }).ClickAsync();
            await Page.WaitForSelectorAsync(".selected");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Red" }).ClickAsync();
            await Page.WaitForTimeoutAsync(500);
            await Page.GetByRole(AriaRole.Button, new() { Name = "ADD TO CART" }).ClickAsync();
            await Page.GetByRole(AriaRole.Link, new() { Name = "VIEW CART (1)" }).ClickAsync();
            await Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" }).ClickAsync();
        }
    }
}

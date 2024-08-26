﻿using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrigthUITests.PageObjects
{
    internal class WebTablesPage(IPage page)
    {
        private readonly IPage page = page;

        public async Task GoToTestPageURL()
        {
            await page.GotoAsync("https://demoqa.com/webtables");
            await page.WaitForURLAsync("https://demoqa.com/webtables");
        }

        public async Task IsPageH1Visible(string pageH1)
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = pageH1 })).ToBeVisibleAsync();
        }

        #region Table:
        public async Task IsTableVisible()
        {
            await Assertions.Expect(page.Locator(".ReactTable")).ToBeVisibleAsync();
        }
        public async Task IsTableRowVisible()
        {
            await Assertions.Expect(page.Locator(".rt-tr-group").First).ToBeVisibleAsync();
        }
        public async Task VerifyTableHeadersContent(string headerName)
        {
            var headers = await page.Locator(".rt-th").AllInnerTextsAsync();
            var headerList = headers.ToList();
            Assert.That(headerList, Does.Contain(headerName), $"The header '{headerName}' was not found in the table headers.");
        }
        public async Task VerifyFirstRowContentIsPresent(string contentValue)
        {
            var row = page.Locator(".rt-tr-group").First;
            var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
            var cellList = cells.ToList();
            Assert.That(cellList, Does.Contain(contentValue), $"The search value '{contentValue}' was not found in the table.");
        }
        public async Task VerifyFirstRowContentIsNotPresent(string contentValue)
        {
            var row = page.Locator(".rt-tr-group").First;
            var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
            var cellList = cells.ToList();
            Assert.That(cellList, Does.Not.Contain(contentValue), $"The search value '{contentValue}' was not found in the table.");
        }
        public async Task VerifyTableContent(string headerName, string cellValue)
        {
            var table = page.Locator(".ReactTable");
            var headers = await table.Locator(".rt-th").AllInnerTextsAsync();
            var headerList = headers.ToList();
            int headerIndex = headerList.IndexOf(headerName);

            if (headerIndex == -1)
            {
                throw new Exception($"Header '{headerName}' not found.");
            }

            var rows = table.Locator(".rt-tr-group");
            var rowCount = await rows.CountAsync();

            bool isCellContentPresent = false;
            for (int i = 0; i < rowCount; i++)
            {
                var row = rows.Nth(i);
                var cells = await row.Locator(".rt-td").AllInnerTextsAsync();
                var cellList = cells.ToList();

                if (headerIndex < cellList.Count && cellList[headerIndex] == cellValue)
                {
                    isCellContentPresent = true;
                    break;
                }
            }
            Assert.That(isCellContentPresent, Is.True, $"The cell value '{cellValue}' is not present under the header '{headerName}'.");
        }
        #endregion

        #region Inputs:
        public async Task FillSearchValue(string searchValue)
        {
            var searchInput = page.GetByPlaceholder("Type to search");

            await searchInput.ClickAsync();
            await searchInput.FillAsync(searchValue);
            await searchInput.FocusAsync();
            //await searchInput.PressAsync("Enter");
        }
        public async Task FillFirstName(string fillText)
        {
            await page.GetByPlaceholder("First Name").FillAsync(fillText);
        }
        public async Task FillLastName(string fillText)
        {
            await page.GetByPlaceholder("Last Name").FillAsync(fillText);
        }
        public async Task FillEmail(string fillText)
        {
            await page.GetByPlaceholder("name@example.com").FillAsync(fillText);
        }
        public async Task FillAge(string fillText)
        {
            await page.GetByPlaceholder("Age").FillAsync(fillText);
        }
        public async Task FillSalary(string fillText)
        {
            await page.GetByPlaceholder("Salary").FillAsync(fillText);
        }
        public async Task FillDepartment(string fillText)
        {
            await page.GetByPlaceholder("Department").FillAsync(fillText);
        }
        #endregion

        #region Buttons:
        public async Task AddButtonClick()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Add" }).ClickAsync();
        }
        public async Task SubmitButtonCLick()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
        }
        public async Task ClickEdit()
        {
            await page.Locator("#edit-record-2").GetByRole(AriaRole.Img).ClickAsync();
        }
        public async Task ClickDelete()
        {
            await page.Locator("#delete-record-2").GetByRole(AriaRole.Img).ClickAsync();
        }
        #endregion

        #region AddPopupVerifications:
        public async Task VerifyAddPopupOpened()
        {
            await Assertions.Expect(page.Locator(".modal-content")).ToBeVisibleAsync();
        }
        public async Task VerifyFirstNameCssOption(string cssOption, string cssValue)
        {
            await Assertions.Expect(page.GetByPlaceholder("First Name")).ToHaveCSSAsync(cssOption, cssValue);
        }
        public async Task VerifyLastNameCssOption(string cssOption, string cssValue)
        {
            await Assertions.Expect(page.GetByPlaceholder("Last Name")).ToHaveCSSAsync(cssOption, cssValue);
        }
        public async Task VerifyEmailCssOption(string cssOption, string cssValue)
        {
            await Assertions.Expect(page.GetByPlaceholder("name@example.com")).ToHaveCSSAsync(cssOption, cssValue);
        }
        public async Task VerifyAgeCssOption(string cssOption, string cssValue)
        {
            await Assertions.Expect(page.GetByPlaceholder("Age")).ToHaveCSSAsync(cssOption, cssValue);
        }
        public async Task VerifySalaryCssOption(string cssOption, string cssValue)
        {
            await Assertions.Expect(page.GetByPlaceholder("Salary")).ToHaveCSSAsync(cssOption, cssValue);
        }
        public async Task VerifyDepartmentCssOption(string cssOption, string cssValue)
        {
            await Assertions.Expect(page.GetByPlaceholder("Department")).ToHaveCSSAsync(cssOption, cssValue);
        }
        #endregion
    }
}
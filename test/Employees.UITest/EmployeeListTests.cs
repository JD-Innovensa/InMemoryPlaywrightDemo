using Employees.UITests.Helper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Employees.UITests
{
    public class EmployeeListTests : BaseTestClass<UI.Startup>
    {
        [Test]
        public async Task When_ClickEmployeeMenuOption_Then_EmployeesPageDisplayed_Chrome()
        {
            RootUri.Should().Be("https://localhost:5001");

            using var playwright = await Playwright.CreateAsync();

            var browser = await TestHelper.GetChromeBrowser(playwright, headless: false);

            var page = await TestHelper.GetPageInNewBrowserContext(browser);

            // Navigate to the home page
            await page.GotoAsync(RootUri);

            await TestHelper.TakeAndLogScreenshot(page, $"ClickEmployeeMenuOption_Chrome", "Homepage");

            var title = await page.TitleAsync();
            var pageSource = await page.ContentAsync();

            using (new AssertionScope())
            {
                title.Should().Be("Home Page - Employees System");
                pageSource.Should().Contain("Welcome to the Employees System!");
            }

            // Click on menu
            await page.ClickAsync("//a[contains(@href,'/Employee')]");

            await TestHelper.TakeAndLogScreenshot(page, $"ClickEmployeeMenuOption_Chrome", "Employees List");

            var pageTitle = await page.TitleAsync();

            pageSource = await page.ContentAsync();
            using (new AssertionScope())
            {
                pageTitle.Should().Be("Employees - Employees System");
                pageSource.Should().Contain("101").And.Contain("CEO Test");
            }
        }

        [Test]
        public async Task When_ClickEmployeeMenuOption_Then_EmployeesPageDisplayed_Edge()
        {
            RootUri.Should().Be("https://localhost:5001");

            using var playwright = await Playwright.CreateAsync();

            var browser = await TestHelper.GetEdgeBrowser(playwright, headless: false);

            var page = await TestHelper.GetPageInNewBrowserContext(browser);

            // Navigate to the home page
            await page.GotoAsync(RootUri);

            await TestHelper.TakeAndLogScreenshot(page, $"ClickEmployeeMenuOption_Edge", "Homepage");

            var title = await page.TitleAsync();
            var pageSource = await page.ContentAsync();

            using (new AssertionScope())
            {
                title.Should().Be("Home Page - Employees System");
                pageSource.Should().Contain("Welcome to the Employees System!");
            }

            // Click on menu
            await page.ClickAsync("//a[contains(@href,'/Employee')]");

            await TestHelper.TakeAndLogScreenshot(page, $"ClickEmployeeMenuOption_Edge", "Employees List");

            var pageTitle = await page.TitleAsync();

            pageSource = await page.ContentAsync();
            using (new AssertionScope())
            {
                pageTitle.Should().Be("Employees - Employees System");
                pageSource.Should().Contain("101").And.Contain("CEO Test");
            }
        }

        [Test]
        public async Task When_ClickEmployeeMenuOption_Then_EmployeesPageDisplayed_Firefox()
        {
            RootUri.Should().Be("https://localhost:5001");

            using var playwright = await Playwright.CreateAsync();

            var browser = await TestHelper.GetFirefoxBrowser(playwright, headless: false);

            var page = await TestHelper.GetPageInNewBrowserContext(browser);

            // Navigate to the home page
            await page.GotoAsync(RootUri);

            await TestHelper.TakeAndLogScreenshot(page, $"ClickEmployeeMenuOption_Edge", "Homepage");

            var title = await page.TitleAsync();
            var pageSource = await page.ContentAsync();

            using (new AssertionScope())
            {
                title.Should().Be("Home Page - Employees System");
                pageSource.Should().Contain("Welcome to the Employees System!");
            }

            // Click on menu
            await page.ClickAsync("//a[contains(@href,'/Employee')]");

            await TestHelper.TakeAndLogScreenshot(page, $"ClickEmployeeMenuOption_Edge", "Employees List");

            var pageTitle = await page.TitleAsync();

            pageSource = await page.ContentAsync();
            using (new AssertionScope())
            {
                pageTitle.Should().Be("Employees - Employees System");
                pageSource.Should().Contain("101").And.Contain("CEO Test");
            }
        }

    }
}
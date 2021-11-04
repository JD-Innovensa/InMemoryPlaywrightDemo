// -----------------------------------------------------------------------
// <copyright file="TestHelper.cs" company="MedLogic Ltd">
// Copyright (c) MedLogic Ltd. All rights reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// </copyright>
// -----------------------------------------------------------------------

namespace Employees.UITests.Helper
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::NUnit.Framework;
    using Microsoft.Playwright;

    /// <summary>
    /// Common Playwright/Unit functionality.
    /// </summary>
    public static class TestHelper
    {
        public const string BrowserChrome = "Chrome";

        public const string BrowserEdge = "Edge";

        public const string BrowserFirefox = "Firefox";

        public static async Task<IBrowser> GetChromeBrowser(IPlaywright playwright, bool headless = false)
        {
            TestContext.Out.WriteLine($"Getting Chrome browser. Headless mode: {headless}.");

            var chromium = playwright.Chromium;

            // Can be "msedge", "chrome-beta", "msedge-beta", "msedge-dev", etc.
            var browser = await chromium.LaunchAsync(new BrowserTypeLaunchOptions { Channel = "chrome", Headless = headless });

            return browser;
        }

        public static async Task<IBrowser> GetEdgeBrowser(IPlaywright playwright, bool headless = false)
        {
            TestContext.Out.WriteLine($"Getting Edge browser. Headless mode: {headless}.");

            var chromium = playwright.Chromium;

            // Can be "msedge", "chrome-beta", "msedge-beta", "msedge-dev", etc.
            var browser = await chromium.LaunchAsync(new BrowserTypeLaunchOptions { Channel = "msedge", Headless = headless });

            return browser;
        }

        public static async Task<IBrowser> GetFirefoxBrowser(IPlaywright playwright, bool headless = false)
        {
            TestContext.Out.WriteLine($"Getting Firefox browser. Headless mode: {headless}.");

            // Needs a download of Firefox (CLI and then install Firefox)
            var firefox = playwright.Firefox;

            Dictionary<string, object> caps = new Dictionary<string, object>();
            caps.Add("security.insecure_field_warning.contextual.enabled", false);
            caps.Add("security.certerrors.permanentOverride", false);
            caps.Add("network.stricttransportsecurity.preloadlist", false);
            caps.Add("security.enterprise_roots.enabled", true);

            var browser = await firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless, FirefoxUserPrefs = caps });

            return browser;
        }

        /// <summary>
        /// Gets IPage object from a new Browser Context.
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static async Task<IPage> GetPageInNewBrowserContext(IBrowser browser)
        {
            TestContext.Out.WriteLine("Creating new context.");

            // Running in Cloud hosted instance - we get an error: Microsoft.Playwright.PlaywrightException : net::ERR_CERT_AUTHORITY_INVALID at https://localhost:5001/
            var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true });

            TestContext.Out.WriteLine("Getting new page.");

            return await browserContext.NewPageAsync();
        }

        public static async Task TakeAndLogScreenshot(IPage page, string testName, string title)
        {
            var path = $"{testName}\\{title}.png";
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
            TestContext.AddTestAttachment(path, title);
        }
    }
}

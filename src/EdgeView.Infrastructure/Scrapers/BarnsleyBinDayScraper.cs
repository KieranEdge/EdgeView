using EdgeView.Application.Interfaces;
using EdgeView.Domain.Entities;
using Microsoft.Playwright;

namespace EdgeView.Infrastructure.Scrapers
{
    public class BarnsleyBinScraper : IBinScraper
    {
        public async Task<BinCollection> GetNextBinAsync(string houseNumber, string postcode)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
            var page = await browser.NewPageAsync();

            // 1. Visit the form page
            await page.GotoAsync("https://waste.barnsley.gov.uk/ViewCollection/SelectAddress");

            // 2. Fill in the form
            await page.FillAsync("input[name='personInfo.person1.HouseNumberOrName']", houseNumber);
            await page.FillAsync("input[name='personInfo.person1.Postcode']", postcode);

            // 3. Click 'Find Address'
            await page.ClickAsync("text=Find Address");

            // 4. Wait for something to happen
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // 5. Detect results page vs address-selection page
            if (!await page.Locator(".ui-bin-next-date").IsVisibleAsync())
            {
                // Select the first address if multiple
                var firstAddressRadio = page.Locator("input[type='radio']");
                if (await firstAddressRadio.CountAsync() > 0)
                {
                    await firstAddressRadio.First.ClickAsync();
                    await page.ClickAsync("text=Next");
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                }
            }

            // 6. Extract the data
            await page.WaitForSelectorAsync(".ui-bin-next-date");

            var dateText = await page.InnerTextAsync(".ui-bin-next-date");
            var binTypeText = await page.InnerTextAsync(".ui-bin-next-type");

            return new BinCollection
            {
                Date = DateTime.Parse(dateText),
                BinType = binTypeText.Trim()
            };
        }
    }
}

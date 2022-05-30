using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogViewModelService _catalogViewModelService;
    private TelemetryClient _telemetry;

    public IndexModel(ICatalogViewModelService catalogViewModelService,
        TelemetryClient telemetry)
    {
        _catalogViewModelService = catalogViewModelService;
        _telemetry = telemetry;
    }

    public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

    public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
    {

        CatalogModel = await _catalogViewModelService.GetCatalogItems(pageId ?? 0, Constants.ITEMS_PER_PAGE, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied);
        //var dictionary = new Dictionary<string, string>();
        //dictionary.Add("Count", $"CatalogModel {CatalogModel.CatalogItems.Count()}");

        //_telemetry.TrackEvent("CatalogItems", dictionary);
    }
}

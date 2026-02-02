using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbraTV.Controllers;

/// <summary>
/// Route hijacking controller for TV Channel document type.
/// This controller intercepts requests for tvChannel content and renders the TvChannel view.
/// </summary>
public class TvChannelController : RenderController
{
    public TvChannelController(
        ILogger<TvChannelController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    public override IActionResult Index()
    {
        return View("TvChannel", CurrentPage);
    }
}

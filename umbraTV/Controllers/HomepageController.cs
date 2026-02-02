using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbraTV.Controllers;

/// <summary>
/// Route hijacking controller for Homepage document type.
/// This controller intercepts requests for homepage content and renders the Homepage view.
/// </summary>
public class HomepageController : RenderController
{
    public HomepageController(
        ILogger<HomepageController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    public override IActionResult Index()
    {
        return View("Homepage", CurrentPage);
    }
}

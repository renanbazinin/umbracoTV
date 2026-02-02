using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbraTV.Controllers;

/// <summary>
/// Route hijacking controller for Channel Category document type.
/// This controller intercepts requests for channelCategory content and renders the ChannelCategory view.
/// </summary>
public class ChannelCategoryController : RenderController
{
    public ChannelCategoryController(
        ILogger<ChannelCategoryController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    public override IActionResult Index()
    {
        return View("ChannelCategory", CurrentPage);
    }
}

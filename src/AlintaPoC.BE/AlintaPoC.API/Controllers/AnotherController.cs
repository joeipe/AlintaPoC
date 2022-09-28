using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlintaPoC.API.Controllers
{
    public class AnotherController : CommonController
    {
        private readonly ILogger<AnotherController> _logger;
        private readonly IFeatureManager _featureManager;

        public AnotherController(
            ILogger<AnotherController> logger,
            IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetTest()
        {
            return Response(
                new List<string> 
                { 
                    await _featureManager.IsEnabledAsync("beta") ? "Is Beta" : "Is not Beta"
                });
        }
    }
}

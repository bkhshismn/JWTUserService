using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserAction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        [Authorize]
        [HttpPost("run-action")]
        public async Task<IActionResult> RunAction()
        {
            await _semaphore.WaitAsync();
            try
            {
                await Task.Delay(10000);
                return Ok("انجام شد");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}

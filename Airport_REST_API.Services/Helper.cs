using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Airport_REST_API.Services.Interfaces;
using Airport_REST_API.Shared.DTO;

namespace Airport_REST_API.Services
{
    public class Helper
    {
        private Timer timer;
        private IFlightService _service;

        public Helper(IFlightService service, int timespan )
        {
            timer = new Timer(timespan);
            timer.AutoReset = false;
            _service = service;
        }

        public async Task<IEnumerable<FlightDTO>> GetFlightsDelay()
        {
            var tcs = new TaskCompletionSource<IEnumerable<FlightDTO>>();

            timer.Enabled = true;

            ElapsedEventHandler callback =
                async (obj, args) =>
                {
                    tcs.SetResult(await _service.GetCollectionAsync());
                    timer.Enabled = false;
                };

            timer.Elapsed += callback;

            return await tcs.Task;
        }
    }
}

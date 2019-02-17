using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wings.worker.Framework.Common.DTO;

namespace Wings.worker.Framework.MH.Controller {

    [Route ("api/Worker/MH/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase {
        [HttpGet ("[action]")]
        public WorkerInformation information () {
            var information = new WorkerInformation ();
            information.isActive = true;
            return information;
        }
    }
}
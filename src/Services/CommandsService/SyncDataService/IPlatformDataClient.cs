using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsService.Models;

namespace CommandsService.SyncDataService
{
    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnallPlatforms();
    }
}
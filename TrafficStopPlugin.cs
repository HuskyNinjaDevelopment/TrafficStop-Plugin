using CitizenFX.Core;
using FivePD.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficStopPlugin
{
    internal class TrafficStopPlugin : Plugin
    {
        private bool offeredCallout = false;

        internal TrafficStopPlugin()
        {
            Tick += CheckForTrafficStop;
        }

        public async Task CheckForTrafficStop()
        {
            //Check to see if player is on a traffic stop
            if(Utilities.IsPlayerPerformingTrafficStop())
            {
                //On a traffic stop
                //Wait till player steps out of the vehicle to show they are about to do the interaction
                if(!Game.PlayerPed.IsInVehicle())
                {
                    //If the player has not been offered the callout give it to them
                    if (!offeredCallout)
                    {
                        //Force Callout
                        offeredCallout = true;
                        Utilities.ForceCallout("TrafficStop");
                    }
                }
            }
            else
            {
                //If player is not on a traffic stop and offered callout is true they just got off of one.
                //offered callout needs to be reset
                if (offeredCallout)
                {
                    offeredCallout = false;
                }
            }

            await Task.FromResult(0);
        }
    }
}

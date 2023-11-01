using System.Collections.Generic;
using RimWorld;

namespace TRC
{
    public class TiberiumIncidentDef : IncidentDef
    {
        public List<EventDef> eventsToTrigger;
        //public PositionFilter positionFilter;
        public List<SkyfallerValue> skyfallers;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Structures
{
    public static class BuildingInfo
    {
        #region Enums
        public enum Type
        {
            STOCKPILE,
            WOODSHOP,
            TAVERN,
            ARCHERY,
            BLACKSMITH,
            BOOTH_PRODUCE,
            BOOTH_FISH,
            BOOTH_GEMS,
            BOOTH_SEEDS,
            BUTCHER,
            STABLES,
            TANNER,
            SCRIBE,
            CHAPEL,
            INN,
            BATH,
            CLOTH,
            ROAD,

            TOWER
        }
        #endregion

        #region Interface
        public static List<Tuple<int, int>> RetrieveRelativeCoordinates(Type t)
        {
            List<Tuple<int, int>> relativeCoordinates = new List<Tuple<int, int>>();
            relativeCoordinates.Add(Tuple.Create(0, 0)); // (x, y)

            switch (t)
            {
                case Type.STOCKPILE:
                    break;

                case Type.ARCHERY:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));

                    relativeCoordinates.Add(Tuple.Create(2, 0));
                    relativeCoordinates.Add(Tuple.Create(2, 1));
                    break;

                case Type.BLACKSMITH:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));
                    break;

                case Type.BOOTH_PRODUCE:
                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    break;

                case Type.BOOTH_FISH:
                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    break;

                case Type.BOOTH_GEMS:
                    break;

                case Type.BOOTH_SEEDS:
                    break;

                case Type.BUTCHER:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));
                    break;

                case Type.CHAPEL:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));

                    relativeCoordinates.Add(Tuple.Create(2, 0));
                    relativeCoordinates.Add(Tuple.Create(2, 1));
                    break;

                case Type.CLOTH:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));


                    break;

                case Type.INN:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));

                    relativeCoordinates.Add(Tuple.Create(2, 0));
                    relativeCoordinates.Add(Tuple.Create(2, 1));
                    break;

                case Type.ROAD:
                    break;

                case Type.SCRIBE:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));
                    break;

                case Type.STABLES:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));

                    relativeCoordinates.Add(Tuple.Create(2, 0));
                    relativeCoordinates.Add(Tuple.Create(2, 1));
                    break;

                case Type.TANNER:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));
                    break;

                case Type.TAVERN:
                    relativeCoordinates.Add(Tuple.Create(0, 1));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));
                    break;

                case Type.BATH:
                    relativeCoordinates.Add(Tuple.Create(0, 1));
                    relativeCoordinates.Add(Tuple.Create(0, 2));

                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));
                    relativeCoordinates.Add(Tuple.Create(1, 2));

                    relativeCoordinates.Add(Tuple.Create(2, 0));
                    relativeCoordinates.Add(Tuple.Create(2, 1));
                    relativeCoordinates.Add(Tuple.Create(2, 2));
                    break;

                case Type.TOWER:
                    relativeCoordinates.Add(Tuple.Create(0, 1));
                    break;

                default:
                    throw new Exception("Building coordinates not defined!");
            }

            return relativeCoordinates;
        }
        public static int RetrieveWidth(Type t)
        {
            switch (t)
            {
                case Type.STOCKPILE:
                case Type.WOODSHOP:
                case Type.TAVERN:
                case Type.ARCHERY:
                case Type.BLACKSMITH:
                case Type.BOOTH_PRODUCE:
                case Type.BOOTH_FISH:
                case Type.BOOTH_GEMS:
                case Type.BOOTH_SEEDS:
                case Type.BUTCHER:
                case Type.STABLES:
                case Type.TANNER:
                case Type.SCRIBE:
                case Type.CHAPEL:
                case Type.INN:
                case Type.BATH:
                case Type.CLOTH:
                case Type.ROAD:
                case Type.TOWER:
                    return 1;

                default:
                    return 1;
            }
        }
        public static int RetrieveHeight(Type t)
        {
            switch (t)
            {
                case Type.STOCKPILE:
                case Type.WOODSHOP:
                case Type.TAVERN:
                case Type.ARCHERY:
                case Type.BLACKSMITH:
                case Type.BOOTH_PRODUCE:
                case Type.BOOTH_FISH:
                case Type.BOOTH_GEMS:
                case Type.BOOTH_SEEDS:
                case Type.BUTCHER:
                case Type.STABLES:
                case Type.TANNER:
                case Type.SCRIBE:
                case Type.CHAPEL:
                case Type.INN:
                case Type.BATH:
                case Type.CLOTH:
                case Type.ROAD:
                    return 1;

                case Type.TOWER:
                    return 2;

                default:
                    return 1;
            }
        }
        public static string RetrieveName(Type t)
        {
            switch (t)
            {
                case Type.STOCKPILE: return "Stockpile"; 
                case Type.WOODSHOP: return "Woodshop";
                case Type.TAVERN: return "Tavern";
                case Type.ARCHERY: return "Archery";
                case Type.BLACKSMITH: return "Blacksmith";
                case Type.BOOTH_PRODUCE: return "Produce Booth";
                case Type.BOOTH_FISH: return "Fish Booth";
                case Type.BOOTH_GEMS: return "Gem Booth";
                case Type.BOOTH_SEEDS: return "Seed Booth";
                case Type.BUTCHER: return "Butcher";
                case Type.STABLES: return "Stables";
                case Type.TANNER: return "Tanner";
                case Type.SCRIBE: return "Scribe";
                case Type.CHAPEL: return "Chapel";
                case Type.INN: return "Inn";
                case Type.BATH: return "Bath";
                case Type.CLOTH: return "Clothery";
                case Type.ROAD: return "Road";
                case Type.TOWER: return "Wood Tower";

                default:
                    return "Undefined";
            }
        }
        public static string RetrieveSprite(Type t)
        {
            switch (t)
            {
                
                case Type.BOOTH_PRODUCE: return "building-booth-produce";
                case Type.BOOTH_FISH: return "building-booth-fish";
                case Type.BOOTH_GEMS: return "building-booth-jewels";
                case Type.BOOTH_SEEDS: return "building-booth-seeds";
                case Type.TOWER: return "debug-tower";
                case Type.STOCKPILE: return "building-stockpile";

                case Type.WOODSHOP:
                case Type.TAVERN:
                case Type.ARCHERY:
                case Type.BLACKSMITH:
                case Type.BUTCHER:
                case Type.STABLES:
                case Type.TANNER:
                case Type.SCRIBE:
                case Type.CHAPEL:
                case Type.INN:
                case Type.BATH:
                case Type.CLOTH:
                case Type.ROAD:
                    return "building-temp1x1";

                default:
                    return "Error";
            }
        }
        public static int RetrieveCapacity(Type t)
        {
            switch (t)
            {
                case Type.STOCKPILE:
                case Type.WOODSHOP:
                case Type.TAVERN:
                case Type.ARCHERY:
                case Type.BLACKSMITH:
                case Type.BOOTH_PRODUCE:
                case Type.BOOTH_FISH:
                case Type.BOOTH_GEMS:
                case Type.BOOTH_SEEDS:
                case Type.BUTCHER:
                case Type.STABLES:
                case Type.TANNER:
                case Type.SCRIBE:
                case Type.CHAPEL:
                case Type.INN:
                case Type.BATH:
                case Type.CLOTH:
                case Type.ROAD:
                    return 0;

                case Type.TOWER:
                    return 1;

                default:
                    return 0;
            }
        }
        #endregion


    }
}
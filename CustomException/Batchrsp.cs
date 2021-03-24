using System;
using System.Collections.Generic;

namespace SpotlightWallpaper.CustomException
{
    public class Batchrsp
    {
        public string ver { get; set; }
        public List<Error> errors { get; set; }
        public DateTime refreshtime { get; set; }
    }
}
﻿using System.Collections.Generic;

namespace Loaders.Data.Raw
{
    public struct Step
    {
        public string Type { get; set; }
        public SoundExtension? SoundExtension { get; set; }
        public TextDialogExtension? TextDialogExtension { get; set; }
        public TimerExtension? TimerExtension { get; set; }
        public ChangeStateObjectExtension? ChangeStateObjectExtension { get; set; }
        public OpenDoorExtension? OpenDoorExtension { get; set; }
        public CreateItemExtension? CreateItemExtension { get; set; }
    }

    public struct Plot
    {
        public int Id;
        public List<Step> Steps; 
    }
    
    public struct PlotRaw
    {
        public List<Plot> Plot;
    }
}
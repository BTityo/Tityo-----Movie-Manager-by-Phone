using MobileMovieManager.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PhoneClient.ViewModels
{
    public class PcOptionViewModel
    {
        [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        public enum PCOption
        {
            [Description("Speciális műveletek")]
            PcOptions,
            [Description("Leállítás")]
            Shutdown,
            [Description("Újraindítás")]
            Restart,
            [Description("Alvás")]
            Sleep,
            [Description("Hibernálás")]
            Hibernate,
            [Description("Kijelentkezés")]
            LogOff,
            [Description("Zárolás")]
            Lock
        }

        public IList<PCOption> PCOptions
        {
            get
            {
                // Will result in a list like {"Tester", "Engineer"}
                return Enum.GetValues(typeof(PCOption)).Cast<PCOption>().ToList<PCOption>();
            }
        }

        public PCOption PCOptionProp
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NewsApp
{
    public enum State
    {
        Normal,
        Loading,
        Error,
        NoInternet
    }

    [ContentProperty("Content")]
    public class StateCondition : View
    {
        public object Is { get; set; }
        public View Content { get; set; }

    }
}

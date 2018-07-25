using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.Helpers
{
    public static class ActionHelper
    {
        public static Exception DoAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                return e;
            }
            return null;
        }
    }
}

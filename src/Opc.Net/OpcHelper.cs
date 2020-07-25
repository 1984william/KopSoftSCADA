using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opc.Net
{
    public class OpcHelper
    {
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static object ShowValue(OpcDaCustomAsyncEventArgs e, int index)
        {
            object returnValue = null;
            if (e.ClientItemsHandle != null)
            {
                returnValue = e.Values[e.ClientItemsHandle.First(s=>s== index)-1];
            }
            return returnValue;
        }
    }
}

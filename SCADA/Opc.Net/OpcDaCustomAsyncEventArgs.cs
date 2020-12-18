using System;

namespace Opc.Net
{
    public class OpcDaCustomAsyncEventArgs:EventArgs
    {
        /// <summary>
        /// Opc组句柄
        /// </summary>
        public int GroupHandle { get; set; }
        /// <summary>
        /// 项组长度
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 项组值
        /// </summary>
        public object[] Values { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public int[] Errors { get; set; }
        /// <summary>
        /// 项组句柄
        /// </summary>
        public int[] ClientItemsHandle { get; set; }
    }
}

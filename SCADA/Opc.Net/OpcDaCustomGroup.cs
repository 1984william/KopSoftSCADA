using System;
using System.Runtime.InteropServices;
using OpcRcw.Da;

namespace Opc.Net
{
    /// <summary>
    /// 自定义接口OPC组对象
    /// </summary>
    public class OpcDaCustomGroup
    {
        private string groupName;
        private int isActive=1;
        private int requestedUpdateRate;
        private int clientGroupHandle=1;
        private GCHandle timeBias = GCHandle.Alloc(0, GCHandleType.Pinned);
        private GCHandle percendDeadBand = GCHandle.Alloc(0, GCHandleType.Pinned);
        private int lcid = 0x409;
        private int itemCount;
        private bool onRead;

        /// <summary>
        /// 输出参数,服务器为新创建的组对象产生的句柄
        /// </summary>
        public int ServerGroupHandle;

        /// <summary>
        /// 输出参数，服务器返回给客户端的实际使用的数据更新率
        /// </summary>
        public int RevisedUpdateRate;

        /// <summary>
        /// 引用参数，客户端想要的组对象的接口类型(如 IIDIOPCItemMgt)
        /// </summary>
        public Guid Riid = typeof(IOPCItemMgt).GUID;

        /// <summary>
        /// 输出参数，用来存储返回的接口指针。如果函数操作出现任务失败，此参数将返回NULL。
        /// </summary>
        public object Group;
        private OpcDaCustomItem[] opcDataCustomItems;

        public int[] PErrors { get; set; }

        /// <summary>
        /// 组对象是否激活
        /// 1为激活，0为未激活,默认激活
        /// </summary>
        public int IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                if (isActive == value)
                    return;
                isActive = value;
            }
        }
        /// <summary>
        /// 组是否采用异步读方式
        /// </summary>
        public bool OnRead
        {
            get
            {
                return onRead;
            }
            set
            {
                if (onRead == value)
                    return;
                onRead = value;
            }
        }
        /// <summary>
        /// 项的个数
        /// </summary>
        public int ItemCount
        {
            get { return itemCount; }
            set 
            {
                if(itemCount == value)
                    return;
                itemCount=value;
            }
        }
        /// <summary>
        /// 客户端指定的数据变化率
        /// </summary>
        public int RequestedUpdateRate
        {
            get
            {
                return requestedUpdateRate;
            }
            set
            {
                if (requestedUpdateRate == value)
                    return;
                requestedUpdateRate = value;
            }
        }

        /// <summary>
        /// OPC组名称
        /// </summary>
        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                if (groupName == value)
                    return;
                groupName = value;
            }
        }

        /// <summary>
        /// 客户端程序为组对象提供的句柄
        /// </summary>
        public int ClientGroupHandle
        {
            get
            {
                return clientGroupHandle;
            }
            set
            {
                if (clientGroupHandle == value)
                    return;
                clientGroupHandle = value;
            }
        }

        /// <summary>
        /// 指向Long类型的指针
        /// </summary>
        public GCHandle TimeBias
        {
            get
            {
                return timeBias;
            }
            set
            {
                if (timeBias == value)
                    return;
                timeBias = value;
            }
        }

        /// <summary>
        /// 一个项对象的值变化的百分比，可能引发客户端程序的订阅回调。
        /// 此参数只应用于组对象中有模拟dwEUType(工程单位)类型的项对象。指针为NULL表示0.0
        /// </summary>
        public GCHandle PercendDeadBand
        {
            get
            {
                return percendDeadBand;
            }
            set
            {
                if (percendDeadBand == value)
                    return;
                percendDeadBand = value;
            }
        }

        /// <summary>
        /// 当用于组对象上的操作的返回值为文本类型时，服务器使用的语言
        /// </summary>
        public int LCID
        {
            get
            {
                return lcid;
            }
            set
            {
                if (lcid == value)
                    return;
                lcid = value;
            }
        }

        /// <summary>
        /// OPC项数组
        /// </summary>
        public OpcDaCustomItem[] OpcDataCustomItems
        {
            get
            {
                return opcDataCustomItems;
            }
            set
            {
                if (opcDataCustomItems != null && opcDataCustomItems == value)
                    return;
                opcDataCustomItems = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OpcRcw.Da;

namespace Opc.Net
{
    /// <summary>
    /// 自定义接口Opc项
    /// </summary>
    public class OpcDaCustomItem
    {
        private string name;
        private string accessPath="";
        private string itemID;
        private int isActive = 1;
        private int clientHandle = 0;
        private int blobSize = 0;
        private IntPtr blob = IntPtr.Zero;
        private short requestedDataType = 0;
        private object itemValue;
        private int serverHandle;

        /// <summary>
        /// 项名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                    return;
                name = value;
            }
        }
        /// <summary>
        /// 项对象的访问路径
        /// </summary>
        public string AccessPath
        {
            get
            {
                return accessPath;
            }
            set
            {
                if (accessPath == value)
                    return;
                accessPath = value;
            }
        }

        /// <summary>
        /// 项对象的ItemIDea，唯一标识该数据项
        /// </summary>
        public string ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                if (itemID == value)
                    return;
                itemID = value;
            }
        }

        /// <summary>
        /// 项对象的激活状态
        /// 1为激活，0为未激活，默认激活
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
        /// 项对象的客户端句柄
        /// </summary>
        public int ClientHandle
        {
            get
            {
                return clientHandle;
            }
            set
            {
                if (clientHandle == value)
                    return;
                clientHandle = value;
            }
        }
        public int BlobSize
        {
            get
            {
                return blobSize;
            }
            set
            {
                if (blobSize == value)
                    return;
                blobSize = value;
            }
        }
        public IntPtr Blob
        {
            get
            {
                return blob;
            }
            set
            {
                if (blob == value)
                    return;
                blob = value;
            }
        }

        /// <summary>
        /// OPC项的数据类型
        /// VbBoolean:11，VbByte:17,VbDecimal:14,VbDouble:5,Vbinteger:2,VbLong:3,VbSingle:4,VbString:8
        /// </summary>
        public short RequestedDataType
        {
            get
            {
                return requestedDataType;
            }
            set
            {
                if (requestedDataType == value)
                    return;
                requestedDataType = value;
            }
        }

       /// <summary>
       /// OPC项的值
       /// </summary>
        public object Value
        {
            get
            {
                return itemValue;
            }
            set
            {
                if (itemValue == value)
                    return;
                itemValue = value;
            }
        }

        /// <summary>
        /// OPC项的服务器句柄
        /// </summary>
        public int ServerHandle
        {
            get
            {
                return serverHandle;
            }
            set
            {
                if (serverHandle == value)
                    return;
                serverHandle = value;
            }
        }
    }
}

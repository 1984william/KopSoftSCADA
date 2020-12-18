using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Opc.Net
{
    public class OpcManager
    {
        /// <summary>
        /// Opc异步接口类
        /// </summary>
        OpcDaCustomAsync _opcDaCustomAsync;
        /// <summary>
        /// 异步读取数据完成事件
        /// </summary>
        public event EventHandler<OpcDaCustomAsyncEventArgs> OnReadCompleted;
        /// <summary>
        /// Opc组列表
        /// </summary>
        List<OpcDaCustomGroup> _opcGroups;
        /// <summary>
        /// OPC服务器名称
        /// </summary>
        string _strRemoteServerName;
        /// <summary>
        /// OPC服务器IP地址
        /// </summary>
        string _strRemoteServerIpAddress;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strConfigFilePath">配置文件路径</param>
        public OpcManager(string strConfigFilePath)
        {
            LoadOpcGroupConfig(strConfigFilePath);
        }
        /// <summary>
        /// 加载Opc组配置
        /// </summary>
        /// <param name="strConfigFilePath">配置文件路径</param>
        public void LoadOpcGroupConfig(string strConfigFilePath)
        {
            try
            {
                if (!File.Exists(strConfigFilePath)) return;
                XDocument xDoc = XDocument.Load(strConfigFilePath);
                XElement xElement = xDoc.Element("System").Element("OpcServer");
                _strRemoteServerName = xElement.Attribute("ServerName").Value;
                _strRemoteServerIpAddress = xElement.Attribute("IPAddress").Value;
                _opcGroups = new List<OpcDaCustomGroup>();
                foreach (XElement xElementItem in xElement.Elements())
                {
                    var opcDaCustomGroupService = new OpcDaCustomGroup
                    {
                        GroupName = xElementItem.Attribute("GroupName").Value,
                        ClientGroupHandle = Convert.ToInt32(xElementItem.Attribute("ClientHandle").Value),
                        RequestedUpdateRate = Convert.ToInt32(xElementItem.Attribute("UpdateRate").Value),
                        OpcDataCustomItems = LoadOpcItemConfig(xElementItem)
                    };
                    _opcGroups.Add(opcDaCustomGroupService);
                }
                _opcDaCustomAsync = new OpcDaCustomAsync(_opcGroups, _strRemoteServerName, _strRemoteServerIpAddress);
                _opcDaCustomAsync.OnReadCompleted += ReadCompleted;
            }
            catch(COMException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 连接Opc服务器
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            return _opcDaCustomAsync.Connect();
        }
        /// <summary>
        /// 连接Opc服务器
        /// </summary>
        /// <returns></returns>
        public bool Connect(string remoteOpcServerName,string remoteOpcServerIpAddress)
        {
            return _opcDaCustomAsync.Connect(remoteOpcServerName, remoteOpcServerIpAddress);
        }
        /// <summary>
        /// 加载Opc项配置
        /// </summary>
        /// <param name="xElement">Opc组Xml节点</param>
        /// <returns></returns>
        public OpcDaCustomItem[] LoadOpcItemConfig(XElement xElement)
        {
            int itemCount = xElement.Elements().Count();
            var opcDaCustomItems = new OpcDaCustomItem[itemCount];
            int i = 0;
            foreach (var xElementItem in xElement.Elements())
            {
                var opcDaCustomItemService = new OpcDaCustomItem
                {
                    ClientHandle = Convert.ToInt32(xElementItem.Attribute("ClientHandle").Value),
                    ItemID = xElementItem.Attribute("ItemID").Value,
                    RequestedDataType = short.Parse(xElementItem.Attribute("RequestedDataType").Value)
                };
                opcDaCustomItems[i] = opcDaCustomItemService;
                i++;
            }
            return opcDaCustomItems;
        }
        public bool WriteForReturn(int itemClientHandle, int value, int clientHandle)
        {
            bool returnValue;
            var itemDictionary = new Dictionary<int, object>
            {
                {itemClientHandle, value}
            };
            try
            {
                int[] pErrors;
                Write(itemDictionary, clientHandle, out pErrors);
                returnValue = (pErrors[0] == 0);
            }
            catch (COMException ex)
            {
                throw ex;
            }
            return returnValue;
        }
        public void Write(Dictionary<int, object> itemDictionary, int groupHandle, out int[] pErrors)
        {
            var count = itemDictionary.Count();
            var values = new object[count];
            var serverHandle = new int[count];
            pErrors = null;
            OpcDaCustomGroup group = _opcGroups.First(p => p.ServerGroupHandle == groupHandle);
            int index = 0;
            foreach (KeyValuePair<int, object> itemId in itemDictionary)
            {
                foreach (var item in group.OpcDataCustomItems)
                {
                    if (item.ClientHandle == itemId.Key)
                    {
                        values[index] = itemId.Value;
                        serverHandle[index] = item.ServerHandle;
                        index++;
                    }
                }
            }
            try
            {
                _opcDaCustomAsync.Write(values, serverHandle, out pErrors, group);
            }
            catch (COMException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 写单个数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="groupHandle">组ID</param>
        /// <param name="clientHandle">项ID</param>
        public void Write(int value, int groupHandle, int clientHandle)
        {
            OpcDaCustomGroup group = GetOpcGroup(groupHandle);
            if (group != null)
            {
                int[] pErrors;
                var serverHanlde = new int[1];
                serverHanlde[0] = group.OpcDataCustomItems.First(c => c.ClientHandle == clientHandle).ServerHandle;
                var values = new object[1];
                values[0] = value;

                _opcDaCustomAsync.Write(values, serverHanlde, out pErrors, group);

            }
        }
        /// <summary>
        /// 异步读取数据完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReadCompleted(object sender, OpcDaCustomAsyncEventArgs e)
        {
            if (OnReadCompleted != null)
            {
                OnReadCompleted(this, e);
            }
        }
        /// <summary>
        /// 异步读取控制模式数据
        /// </summary>
        public void Read()
        {
            if (_opcDaCustomAsync != null)
            {
                _opcDaCustomAsync.Read();
            }

        }
        /// <summary>
        /// 根据OPC句柄获取OPC组对象
        /// </summary>
        /// <param name="groupHandle">OPC组对象</param>
        /// <returns></returns>
        public OpcDaCustomGroup GetOpcGroup(int groupHandle)
        {
            return _opcGroups.First(e => e.ClientGroupHandle == groupHandle);
        }
    }
}

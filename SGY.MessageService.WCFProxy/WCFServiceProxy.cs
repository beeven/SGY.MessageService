using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography;
using GZCustoms.Application.SGY.MessageService.Interface;

namespace GZCustoms.Application.SGY.MessageService.WCFProxy
{
    public class WCFServiceProxy
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        private IMessageServiceWCF Instance { get; set; }

        /// <summary>
        /// WCF代理类构造方法
        /// </summary>
        /// <param name="url">服务地址</param>
        public WCFServiceProxy(string url)
        {
            this.Url = url;      
            WSHttpBinding httpBinding = new WSHttpBinding();
            httpBinding.Security.Mode = SecurityMode.None;      
            httpBinding.MaxReceivedMessageSize = 2147483647;
            httpBinding.MaxBufferPoolSize = 2147483647;       
            EndpointAddress httpEndpointAddress = new EndpointAddress(url);
            XmlDictionaryReaderQuotas myReaderQuotas = new XmlDictionaryReaderQuotas() { MaxStringContentLength = 2147483647, MaxArrayLength = 16384, MaxDepth = 32, MaxBytesPerRead = 4096, MaxNameTableCharCount = 16384 };       
            httpBinding.ReaderQuotas = myReaderQuotas;
            ChannelFactory<IMessageServiceWCF> wsHttpFactory = new ChannelFactory<IMessageServiceWCF>(httpBinding, httpEndpointAddress);
            Instance = wsHttpFactory.CreateChannel();
            
        }

        /// <summary>
        /// WCF代理类构造方法
        /// </summary>
        /// <param name="url">服务地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public WCFServiceProxy(string url, string userName, string password)
        {
            this.Url = url;
            WSHttpBinding httpBinding = new WSHttpBinding();
            httpBinding.Security.Mode = SecurityMode.Message;
            httpBinding.MaxReceivedMessageSize = 2147483647;
            httpBinding.MaxBufferPoolSize = 2147483647;
            XmlDictionaryReaderQuotas myReaderQuotas = new XmlDictionaryReaderQuotas() { MaxStringContentLength = 2147483647, MaxArrayLength = 16384, MaxDepth = 32, MaxBytesPerRead = 4096, MaxNameTableCharCount = 16384 };
            httpBinding.ReaderQuotas = myReaderQuotas;
            EndpointAddress httpEndpointAddress = new EndpointAddress(url);         
            ChannelFactory<IMessageServiceWCF> wsHttpFactory = new ChannelFactory<IMessageServiceWCF>(httpBinding, httpEndpointAddress);
            wsHttpFactory.Credentials.Windows.ClientCredential.UserName = userName;
            wsHttpFactory.Credentials.Windows.ClientCredential.Password = password;
            Instance = wsHttpFactory.CreateChannel();
        }


        #region 上载报关单数据
        /// <summary>
        /// 上载报文（到QP）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号（可选），如果没有填空字符</param>
        /// <param name="msgXml">消息</param>
        /// <returns>报文回执实体</returns>  
        public MesReceipt SendMessage(string keyValue, string machineCode, string cusCiqNo, string msgXml)
        {
            return Instance.SendMessage(keyValue, machineCode, cusCiqNo, msgXml);
        }
        #endregion

        #region 暂存相关
        /// <summary>
        /// 获取关检关联号
        /// </summary>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <returns>关检关联号</returns>
        public string GetCusCiqNo(string ieFlag, string locationCode)
        {
            return Instance.GetCusCiqNo(ieFlag, locationCode);
        }

        /// <summary>
        /// 下载报关数据
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>      
        public string DownloadCustomsData(string keyValue, string machineCode, string cusCiqNo, string password)
        {
            return Instance.DownloadCustomsData(keyValue, machineCode, cusCiqNo, password);
        }

        /// <summary>
        /// 下载报检数据
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>     
        public string DownloadCiqData(string keyValue, string machineCode, string cusCiqNo, string password)
        {
            return Instance.DownloadCiqData(keyValue, machineCode, cusCiqNo, password);
        }

        /// <summary>
        /// 上传报关和报检数据（暂存数据）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="status">数据状态（0，暂存；1，报检；2，上载QP；3，申报）</param>
        /// <param name="cusMsgXml">报关数据报文</param>      
        /// <param name="ciqMsgXml">报检数据报文</param>
        /// <returns>返回实体消息</returns>        
        public SaveModel UploadAllData(string keyValue, string machineCode, string ieFlag, string locationCode, string cusCiqNo, int status, string cusMsgXml, string ciqMsgXml)
        {
            return Instance.UploadAllData(keyValue, machineCode, ieFlag, locationCode, cusCiqNo, status, cusMsgXml, ciqMsgXml);
        }

        /// <summary>
        /// 上传报关和报检数据（暂存数据）（普通暂存）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <param name="cusCiqNo">关检关联号</param>       
        /// <param name="cusMsgXml">报关数据报文</param>      
        /// <param name="ciqMsgXml">报检数据报文</param>
        /// <returns>返回实体消息</returns>        
        public SaveModel UploadAllData(string keyValue, string machineCode, string ieFlag, string locationCode, string cusCiqNo, string cusMsgXml, string ciqMsgXml)
        {
            return Instance.UploadAllData(keyValue, machineCode, ieFlag, locationCode, cusCiqNo, 0, cusMsgXml, ciqMsgXml);
        }
        /// <summary>
        /// 上传报关数据（暂存数据）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <param name="status">数据状态（0，暂存；1，报检；2，上载QP；3，申报）</param>
        /// <param name="msgXml">消息</param>       
        /// <returns>返回实体消息</returns>
        public SaveModel UploadCustomsData(string keyValue, string machineCode, string ieFlag, string locationCode, string cusCiqNo, int status, string msgXml)
        {
            return Instance.UploadCustomsData(keyValue, machineCode, ieFlag, locationCode, cusCiqNo, status, msgXml);
        }
        /// <summary>
        /// 上传报关数据（暂存数据）（普通暂存）
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="ieFlag">进出口标识，1为进口，0为出口</param>
        /// <param name="locationCode">现场代码（4位）</param>
        /// <param name="cusCiqNo">关检关联号</param>      
        /// <param name="msgXml">消息</param>       
        /// <returns>返回实体消息</returns>
        public SaveModel UploadCustomsDataByDefault(string keyValue, string machineCode, string ieFlag, string locationCode, string cusCiqNo, string msgXml)
        {
            return Instance.UploadCustomsData(keyValue, machineCode, ieFlag, locationCode, cusCiqNo, 0, msgXml);
        }
        /// <summary>
        /// 获取服务器上单据的保存时间
        /// </summary>
        /// <param name="cusCiqNo">关检关联号</param>
        /// <returns>返回暂存时间</returns>
        public DateTime GetSaveTime(string cusCiqNo)
        {
            return Instance.GetSaveTime(cusCiqNo);
        }

        #endregion

        #region 用户管理

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="password">用户密码（明文）</param>
        /// <returns>用户信息实体</returns>        
        public UserInfo Login(string userName, string password)
        {
            return Instance.Login(userName, Md5Encrypt(password));
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="pwdOld">旧密码（明文）</param>
        /// <param name="pwdNew">新密码（明文）</param>
        /// <returns>修改结果 0 不成功，1 成功</returns>
        public int UpdatePassword(string userName, string pwdOld, string pwdNew)
        {
            return Instance.UpdatePassword(userName, this.Md5Encrypt(pwdOld), Md5Encrypt(pwdNew));
        }

        /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="userGuid">用户ID</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功,2 该激活码已成功激活过</returns>
        public int ActiveKey(string userGuid, string keyValue, string machineCode)
        {
            return Instance.ActiveKey(userGuid, keyValue, machineCode);
        }

        /// <summary>
        /// 激活码激活
        /// </summary>
        /// <param name="loginName">登陆用户名</param>
        /// <param name="password">密码（明文）</param>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <returns>激活结果 0 不成功，1 成功</returns>
        public int ActiveKeyByLoginName(string loginName, string password, string keyValue, string machineCode)
        {
            return Instance.ActiveKeyByLoginName(loginName, this.Md5Encrypt(password), keyValue, machineCode);
        }

        #endregion

        #region 回执处理

        /// <summary>
        /// 下载报关回执
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        public IEnumerable<CusReturnInfo> ReceiveMsgRep(string keyValue, string machineCode, string taskId)
        {
            return Instance.ReceiveMsgRep(keyValue, machineCode, taskId);
        }

        /// <summary>
        /// 下载报关回执2
        /// </summary>
        /// <param name="keyValue">激活码</param>
        /// <param name="machineCode">机器代码</param>
        /// <param name="taskId">任务ID</param>
        /// <returns>回执内容</returns>
        public IEnumerable<CusReturnInfo2> ReceiveMsgRep2(string keyValue, string machineCode, string taskId)
        {
            return Instance.ReceiveMsgRep2(keyValue, machineCode, taskId);
        }
        #endregion

        #region 下载已报关数据
        /// <summary>
        /// 下载已申报报关数据
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <returns>报关数据</returns>        
        public CusDeclDataMsg GetDeclCusData(string taskId)
        {
            return Instance.GetDeclCusData(taskId);
        }
        /// <summary>
        /// 下载已申报报关数据
        /// </summary>
        /// <param name="idList">任务编号列表</param>
        /// <returns>报关数据列表</returns>       
        public IEnumerable<CusDeclDataMsg> GetDeclCusDataList(IEnumerable<string> taskIdList)
        {
            return Instance.GetDeclCusDataList(taskIdList);
        }
        #endregion


        internal string Md5Encrypt(string originalStr)
        {
            byte[] b = Encoding.UTF8.GetBytes(originalStr);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret.ToUpper();
        }     
    }
}

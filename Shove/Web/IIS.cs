using System;
using System.DirectoryServices;
using System.Collections;
using System.Text.RegularExpressions;

namespace Shove._Web
{
    /// <summary>
    /// �� IIS ���в���
    /// </summary>
    public class IIS
    {
        #region UserName,Password,HostName�Ķ���

        private string hostName = "localhost";
        private string userName;
        private string password;

        /// <summary>
        /// ������
        /// </summary>
        public string HostName
        {
            get
            {
                return hostName;
            }

            set
            {
                hostName = value;
            }
        }

        /// <summary>
        /// Զ�̲��������ṩ���û���
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        /// <summary>
        /// Զ�̲��������ṩ���û�����
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                if (UserName.Length <= 1)
                {
                    throw new ArgumentException("��û��ָ�����û���������ָ���û���");
                }
                password = value;
            }
        }

        /// <summary>
        /// Զ�̲������û�����
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void RemoteConfig(string hostName, string userName, string password)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
        }

        #endregion

        #region ����·������Entry�ķ���

        /// <summary>
        ///  �����Ƿ����û������ж��Ƿ���Զ�̷�������
        ///  Ȼ���ٹ������ͬ��DirectoryEntry����
        /// </summary>
        /// <param name="EntryPath">DirectoryEntry��·��</param>
        /// <returns>���ص���DirectoryEntryʵ��</returns>
        public DirectoryEntry GetDirectoryEntry(string EntryPath)
        {
            DirectoryEntry Entry;

            if (String.IsNullOrEmpty(UserName))
            {
                Entry = new DirectoryEntry(EntryPath);
            }
            else
            {
                Entry = new DirectoryEntry(EntryPath, UserName, Password, AuthenticationTypes.Secure);
            }

            return Entry;
        }

        #endregion

        #region ��ӣ�ɾ����վ�ķ���

        /// <summary>
        ///  ����һ���µ���վ�����ݴ���������Ϣ��������
        /// </summary>
        /// <param name="siteInfo">�洢��������վ����Ϣ</param>
        public void CreateWebSite(SiteInfo siteInfo)
        {
            if (!EnsureNewSiteEnavaible(siteInfo.BindString))
            {
                throw new Exception("վ���Ѿ����ڡ�" + Environment.NewLine + siteInfo.BindString);
            }

            string entPath = String.Format("IIS://{0}/w3svc", HostName);
            DirectoryEntry rootEntry = GetDirectoryEntry(entPath);

            if (rootEntry == null)
            {
                throw new Exception("IIS Ŀ¼��ʧ�ܡ�");
            }

            long newSiteNum = GetNewWebSiteID();

            DirectoryEntry newSiteEntry = rootEntry.Children.Add(newSiteNum.ToString(), "IIsWebServer");

            newSiteEntry.CommitChanges();
            newSiteEntry.Properties["ServerBindings"].Value = siteInfo.BindString;
            newSiteEntry.Properties["ServerComment"].Value = siteInfo.CommentOfWebSite;
            newSiteEntry.CommitChanges();

            DirectoryEntry vdEntry = newSiteEntry.Children.Add("root", "IIsWebVirtualDir");

            vdEntry.CommitChanges();
            vdEntry.Properties["Path"].Value = siteInfo.WebPath;
            vdEntry.CommitChanges();
        }

        /// <summary>
        ///  ɾ��һ����վ��������վ����ɾ����
        /// </summary>
        /// <param name="SiteName">��վ����</param>
        public void DeleteWebSiteByName(string SiteName)
        {
            long siteNum = GetWebSiteNum(SiteName);

            if (siteNum < 0)
            {
                throw new Exception("IIS վ�㡰" + SiteName + "����ʧ�ܡ�");
            }

            string siteEntPath = String.Format("IIS://{0}/w3svc/{1}", HostName, siteNum);
            DirectoryEntry siteEntry = GetDirectoryEntry(siteEntPath);

            if (siteEntry == null)
            {
                throw new Exception("IIS վ�㡰" + SiteName + "����ʧ�ܡ�");
            }

            string rootPath = String.Format("IIS://{0}/w3svc", HostName);
            DirectoryEntry rootEntry = GetDirectoryEntry(rootPath);

            if (rootEntry == null)
            {
                throw new Exception("IIS Ŀ¼��ʧ�ܡ�");
            }

            rootEntry.Children.Remove(siteEntry);
            rootEntry.CommitChanges();
        }

        #endregion

        #region Start��Stop��վ�ķ���

        /// <summary>
        /// ����һ����վ
        /// </summary>
        /// <param name="SiteName"></param>
        public void StartWebSite(string SiteName)
        {
            long siteNum = GetWebSiteNum(SiteName);

            if (siteNum < 0)
            {
                throw new Exception("IIS վ�㡰" + SiteName + "����ʧ�ܡ�");
            }

            string siteEntPath = String.Format("IIS://{0}/w3svc/{1}", HostName, siteNum);
            DirectoryEntry siteEntry = GetDirectoryEntry(siteEntPath);

            if (siteEntry == null)
            {
                throw new Exception("IIS վ�㡰" + SiteName + "����ʧ�ܡ�");
            }

            siteEntry.Invoke("Start", new object[] { });
        }

        /// <summary>
        /// ֹͣһ����վ
        /// </summary>
        /// <param name="SiteName"></param>
        public void StopWebSite(string SiteName)
        {
            long siteNum = GetWebSiteNum(SiteName);

            if (siteNum < 0)
            {
                throw new Exception("IIS վ�㡰" + SiteName + "����ʧ�ܡ�");
            }

            string siteEntPath = String.Format("IIS://{0}/w3svc/{1}", HostName, siteNum);
            DirectoryEntry siteEntry = GetDirectoryEntry(siteEntPath);

            if (siteEntry == null)
            {
                throw new Exception("IIS վ�㡰" + SiteName + "����ʧ�ܡ�");
            }

            siteEntry.Invoke("Stop", new object[] { });
        }

        #endregion

        #region ȷ����վ�Ƿ���ͬ

        /// <summary>
        ///  ȷ��һ���µ���վ�����е���վû����ͬ�ġ�������ֹ���Ƿ������ݴ�ŵ�IIS����ȥ
        /// </summary>
        /// <param name="bindStr">��վ���Ϣ</param>
        /// <returns>��Ϊ���Դ�������Ϊ�����Դ���</returns>
        public bool EnsureNewSiteEnavaible(string bindStr)
        {
            string entPath = String.Format("IIS://{0}/w3svc", HostName);
            DirectoryEntry ent = GetDirectoryEntry(entPath);

            if (ent == null)
            {
                throw new Exception("IIS Ŀ¼��ʧ�ܡ�");
            }

            foreach (DirectoryEntry child in ent.Children)
            {
                if (child.SchemaClassName == "IIsWebServer")
                {
                    if (child.Properties["ServerBindings"].Value != null)
                    {
                        if (child.Properties["ServerBindings"].Value.ToString() == bindStr)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        #region ��ȡһ����վ��ŵķ���

        /// <summary>
        ///  ��ȡһ����վ�ı�š�������վ��ServerBindings����ServerComment��ȷ����վ���
        /// </summary>
        /// <param name="SiteName"></param>
        /// <returns>������վ�ı��</returns>
        public long GetWebSiteNum(string SiteName)
        {
            Regex regex = new Regex(SiteName);
            string tmpStr;

            string entPath = String.Format("IIS://{0}/w3svc", HostName);
            DirectoryEntry ent = GetDirectoryEntry(entPath);

            foreach (DirectoryEntry child in ent.Children)
            {
                if (child.SchemaClassName == "IIsWebServer")
                {
                    if (child.Properties["ServerBindings"].Value != null)
                    {
                        tmpStr = child.Properties["ServerBindings"].Value.ToString();

                        if (regex.Match(tmpStr).Success)
                        {
                            return _Convert.StrToLong(child.Name, -1);
                        }
                    }

                    if (child.Properties["ServerComment"].Value != null)
                    {
                        tmpStr = child.Properties["ServerComment"].Value.ToString();

                        if (regex.Match(tmpStr).Success)
                        {
                            return _Convert.StrToLong(child.Name, -2);
                        }
                    }
                }
            }

            // վ�㲻����
            return -3;
        }

        #endregion

        #region ��ȡ����վid�ķ���

        /// <summary>
        ///  ��ȡ��վϵͳ�������ʹ�õ���С��ID��
        ///  ������Ϊÿ����վ����Ҫ��һ��Ψһ�ı�ţ�����������ԽСԽ�á�
        ///  ��������㷨�����˲�����û������ġ�
        /// </summary>
        /// <returns>��С��id</returns>
        public long GetNewWebSiteID()
        {
            ArrayList list = new ArrayList();
            string tmpStr;

            string entPath = String.Format("IIS://{0}/w3svc", HostName);
            DirectoryEntry ent = GetDirectoryEntry(entPath);

            if (ent == null)
            {
                throw new Exception("IIS Ŀ¼��ʧ�ܡ�");
            }

            foreach (DirectoryEntry child in ent.Children)
            {
                if (child.SchemaClassName == "IIsWebServer")
                {
                    tmpStr = child.Name.ToString();
                    list.Add(Convert.ToInt32(tmpStr));
                }
            }

            list.Sort();

            long i = 1;
            foreach (long j in list)
            {
                if (i == j)
                {
                    i++;
                }
            }

            return i;
        }

        #endregion

        #region ����վ��Ϣ�ṹ��

        /// <summary>
        /// ����վ��Ϣ�ṹ��
        /// </summary>
        public struct SiteInfo
        {
            private string hostIP;   // The Hosts IP Address
            private string portNum;   // The New Web Sites Port.generally is "80"
            private string descOfWebSite; // ��վ��ʾ��һ��Ϊ��վ����վ��������"www.dns.com.cn"
            private string commentOfWebSite;// ��վע�͡�һ��ҲΪ��վ����վ����
            private string webPath;   // ��վ����Ŀ¼������"e:\tmp"

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hostIP"></param>
            /// <param name="portNum"></param>
            /// <param name="descOfWebSite"></param>
            /// <param name="commentOfWebSite"></param>
            /// <param name="webPath"></param>
            public SiteInfo(string hostIP, string portNum, string descOfWebSite, string commentOfWebSite, string webPath)
            {
                this.hostIP = hostIP;
                this.portNum = portNum;
                this.descOfWebSite = descOfWebSite;
                this.commentOfWebSite = commentOfWebSite;
                this.webPath = webPath;
            }

            /// <summary>
            /// 
            /// </summary>
            public string BindString
            {
                get
                {
                    return String.Format("{0}:{1}:{2}", hostIP, portNum, descOfWebSite);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public string CommentOfWebSite
            {
                get
                {
                    return commentOfWebSite;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public string WebPath
            {
                get
                {
                    return webPath;
                }
            }
        }

        #endregion

        #region ����ͷ����

        /// <summary>
        /// ����վ������ͷ(����վ������)
        /// </summary>
        /// <param name="SiteName"></param>
        /// <param name="IPAddress"></param>
        /// <param name="Port"></param>
        /// <param name="Url"></param>
        public void AddHostHeader(string SiteName, string IPAddress, string Port, string Url)
        {
            AddHostHeader(GetWebSiteNum(SiteName), IPAddress, Port, Url);
        }

        /// <summary>
        /// ����վ������ͷ(����վ��ţ�Ҳ����վ���ʶ)
        /// </summary>
        /// <param name="SiteNum"></param>
        /// <param name="IPAddress"></param>
        /// <param name="Port"></param>
        /// <param name="Url"></param>
        public void AddHostHeader(long SiteNum, string IPAddress, string Port, string Url)
        {
            if (SiteNum < 0)
            {
                throw new Exception("IIS վ��š�" + SiteNum.ToString() + "����ʧ�ܡ�");
            }

            string entry = String.Format("IIS://{0}/w3svc/{1}", HostName, SiteNum);
            DirectoryEntry site = GetDirectoryEntry(entry);

            if (site == null)
            {
                throw new Exception("IIS վ��š�" + SiteNum.ToString() + "����ʧ�ܡ�");
            }

            PropertyValueCollection serverBindings = site.Properties["ServerBindings"];

            string headerStr = string.Format("{0}:{1}:{2}", IPAddress, Port, Url);

            if (!serverBindings.Contains(headerStr))
            {
                serverBindings.Add(headerStr);
                site.CommitChanges();
            }
        }

        /// <summary>
        /// ɾ��վ������ͷ(����վ������)
        /// </summary>
        /// <param name="SiteName"></param>
        /// <param name="IPAddress"></param>
        /// <param name="Port"></param>
        /// <param name="Url"></param>
        public void DeleteHostHeader(string SiteName, string IPAddress, string Port, string Url)
        {
            DeleteHostHeader(GetWebSiteNum(SiteName), IPAddress, Port, Url);
        }

        /// <summary>
        /// ɾ��վ������ͷ(����վ��ţ�Ҳ����վ���ʶ)
        /// </summary>
        /// <param name="SiteNum"></param>
        /// <param name="IPAddress"></param>
        /// <param name="Port"></param>
        /// <param name="Url"></param>
        public void DeleteHostHeader(long SiteNum, string IPAddress, string Port, string Url)
        {
            if (SiteNum < 0)
            {
                throw new Exception("IIS վ��š�" + SiteNum.ToString() + "����ʧ�ܡ�");
            }

            string entry = String.Format("IIS://{0}/w3svc/{1}", HostName, SiteNum);
            DirectoryEntry site = GetDirectoryEntry(entry);

            if (site == null)
            {
                throw new Exception("IIS վ��š�" + SiteNum.ToString() + "����ʧ�ܡ�");
            }

            PropertyValueCollection serverBindings = site.Properties["ServerBindings"];

            string headerStr = string.Format("{0}:{1}:{2}", IPAddress, Port, Url);

            if (serverBindings.Contains(headerStr))
            {
                serverBindings.Remove(headerStr);
                site.CommitChanges();
            }
        }

        #endregion

        /// <summary>
        /// ���̳صĲ�����
        /// </summary>
        public enum ApplicationPoolOperateType
        {
            /// <summary>
            /// �������̳�
            /// </summary>
            Start,
            /// <summary>
            /// ֹͣ���̳�
            /// </summary>
            Stop,
            /// <summary>
            /// ���ս��̳�
            /// </summary>
            Recycle
        }

        /// <summary>
        /// ��λ IIS Ӧ�ó����, Operator = "Stop"��"Recycle"�ȡ�//"Stop" ΪֹͣӦ�ó����,"Recycle"Ϊ��λIIS�����
        /// </summary>
        public void RestartApplicationPool(string applicationPoolName, ApplicationPoolOperateType OperateType)
        {
            DirectoryEntry applicationPool = new DirectoryEntry("IIS://localhost/W3SVC/AppPools/" + applicationPoolName);
            applicationPool.Invoke(OperateType.ToString());
        }

        /// <summary>
        /// ��ȡ���еĽ��̳��б�
        /// </summary>
        /// <returns></returns>
        public string[] GetApplicationPools()
        {
            DirectoryEntry ApplicationPools = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            DirectoryEntries Pools = ApplicationPools.Children;

            ArrayList al = new ArrayList();

            foreach (DirectoryEntry pool in Pools)
            {
                al.Add(pool.Name);
            }

            if (al.Count < 1)
            {
                return null;
            }

            string[] Result = new string[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                Result[i] = al[i].ToString();
            }

            return Result;
        }

        /// <summary>
        /// ����һ���µĽ��̳�
        /// </summary>
        /// <param name="PoolName">���̳�����</param>
        /// <param name="PeriodicRestartTime">��ʱ������Դ������</param>
        public void CreateApplicationPool(string PoolName, int PeriodicRestartTime)
        {
            DirectoryEntry ApplicationPools = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            System.DirectoryServices.DirectoryEntry NewPool = ApplicationPools.Children.Add(PoolName, "IISApplicationPool");

            NewPool.Properties["PeriodicRestartTime"][0] = PeriodicRestartTime;
            NewPool.CommitChanges();
        }
    }
}

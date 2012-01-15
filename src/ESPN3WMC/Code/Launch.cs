using System.Collections.Generic;
using Microsoft.MediaCenter.Hosting;

namespace ESPN3WMC
{
    public class MyAddIn : IAddInModule, IAddInEntryPoint
    {
        private static HistoryAddInPageSession s_session;

        public void Initialize(Dictionary<string, object> appInfo, Dictionary<string, object> entryPointInfo)
        {
        }

        public void Uninitialize()
        {
        }

        public void Launch(AddInHost host)
        {
            if (host != null && host.ApplicationContext != null)
            {
                host.ApplicationContext.SingleInstance = true;
            }

            s_session = new HistoryAddInPageSession();
            s_session.GoToPageWithoutHistory("resx://ESPN3WMC/ESPN3WMC.Resources/Loading", null, null);
        }

        /// <summary>
        /// In addition to the back page stack supported by HistoryOrientedPageSession, supports
        /// going to a page without that page being added to the back stack.
        /// </summary>
        private class HistoryAddInPageSession : HistoryOrientedPageSession
        {
            public void GoToPageWithoutHistory(
                string source, IDictionary<string, object> sourceData, IDictionary<string, object> uiProperties)
            {
                LoadPage(source, sourceData, uiProperties);
            }
        }
    }
}
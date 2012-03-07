﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
// 
#pragma warning disable 1591

namespace ESPN3Web.wsESPN3 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Service", Namespace="http://amarkota.com/webservices/espn3")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private UserCredentials userCredentialsValueField;
        
        private System.Threading.SendOrPostCallback GetListingsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::ESPN3Web.Properties.Settings.Default.ESPN3Web_wsESPN3_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public UserCredentials UserCredentialsValue {
            get {
                return this.userCredentialsValueField;
            }
            set {
                this.userCredentialsValueField = value;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetListingsCompletedEventHandler GetListingsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("UserCredentialsValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://amarkota.com/webservices/espn3/GetListings", RequestNamespace="http://amarkota.com/webservices/espn3", ResponseNamespace="http://amarkota.com/webservices/espn3", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ListingsResponse GetListings() {
            object[] results = this.Invoke("GetListings", new object[0]);
            return ((ListingsResponse)(results[0]));
        }
        
        /// <remarks/>
        public void GetListingsAsync() {
            this.GetListingsAsync(null);
        }
        
        /// <remarks/>
        public void GetListingsAsync(object userState) {
            if ((this.GetListingsOperationCompleted == null)) {
                this.GetListingsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListingsOperationCompleted);
            }
            this.InvokeAsync("GetListings", new object[0], this.GetListingsOperationCompleted, userState);
        }
        
        private void OnGetListingsOperationCompleted(object arg) {
            if ((this.GetListingsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListingsCompleted(this, new GetListingsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://amarkota.com/webservices/espn3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://amarkota.com/webservices/espn3", IsNullable=false)]
    public partial class UserCredentials : System.Web.Services.Protocols.SoapHeader {
        
        private string authenticationKeyField;
        
        private string usernameField;
        
        private string passwordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string AuthenticationKey {
            get {
                return this.authenticationKeyField;
            }
            set {
                this.authenticationKeyField = value;
            }
        }
        
        /// <remarks/>
        public string Username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <remarks/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://amarkota.com/webservices/espn3")]
    public partial class Match {
        
        private System.DateTime occurrenceField;
        
        private ListingType occurrenceTypeField;
        
        private string leagueField;
        
        private string categoryField;
        
        private string nameField;
        
        private string descriptionField;
        
        private string videoUrlField;
        
        /// <remarks/>
        public System.DateTime Occurrence {
            get {
                return this.occurrenceField;
            }
            set {
                this.occurrenceField = value;
            }
        }
        
        /// <remarks/>
        public ListingType OccurrenceType {
            get {
                return this.occurrenceTypeField;
            }
            set {
                this.occurrenceTypeField = value;
            }
        }
        
        /// <remarks/>
        public string League {
            get {
                return this.leagueField;
            }
            set {
                this.leagueField = value;
            }
        }
        
        /// <remarks/>
        public string Category {
            get {
                return this.categoryField;
            }
            set {
                this.categoryField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        public string VideoUrl {
            get {
                return this.videoUrlField;
            }
            set {
                this.videoUrlField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://amarkota.com/webservices/espn3")]
    public enum ListingType {
        
        /// <remarks/>
        LIVE,
        
        /// <remarks/>
        REPLAY,
        
        /// <remarks/>
        UPCOMING,
        
        /// <remarks/>
        UNKNOWN,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://amarkota.com/webservices/espn3")]
    public partial class League {
        
        private int idField;
        
        private string nameField;
        
        private string pictureField;
        
        private Match[] matchesField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string Picture {
            get {
                return this.pictureField;
            }
            set {
                this.pictureField = value;
            }
        }
        
        /// <remarks/>
        public Match[] Matches {
            get {
                return this.matchesField;
            }
            set {
                this.matchesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://amarkota.com/webservices/espn3")]
    public partial class Sport {
        
        private int idField;
        
        private string nameField;
        
        private string pictureField;
        
        private string keywordField;
        
        private League[] leaguesField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string Picture {
            get {
                return this.pictureField;
            }
            set {
                this.pictureField = value;
            }
        }
        
        /// <remarks/>
        public string Keyword {
            get {
                return this.keywordField;
            }
            set {
                this.keywordField = value;
            }
        }
        
        /// <remarks/>
        public League[] Leagues {
            get {
                return this.leaguesField;
            }
            set {
                this.leaguesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://amarkota.com/webservices/espn3")]
    public partial class ListingsResponse {
        
        private ListingsStatus statusField;
        
        private string messageField;
        
        private Sport[] sportsField;
        
        /// <remarks/>
        public ListingsStatus Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public Sport[] Sports {
            get {
                return this.sportsField;
            }
            set {
                this.sportsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://amarkota.com/webservices/espn3")]
    public enum ListingsStatus {
        
        /// <remarks/>
        SUCCESS,
        
        /// <remarks/>
        FAILURE,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetListingsCompletedEventHandler(object sender, GetListingsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetListingsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetListingsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ListingsResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ListingsResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
﻿#pragma checksum "D:\Chinedu\code camp - 2012\CityTourApp\CityTourApp\Page_signIn.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D7ADF697D5627F8DDEC86F4463C88C5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CityTourApp {
    
    
    public partial class Page_signIn : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.PivotItem sign_in;
        
        internal System.Windows.Controls.TextBox txtUsername;
        
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        internal System.Windows.Controls.Button btnSign_in;
        
        internal Microsoft.Phone.Controls.PivotItem sign_up;
        
        internal System.Windows.Controls.TextBox txtSUusername;
        
        internal System.Windows.Controls.PasswordBox txtSUpassword;
        
        internal System.Windows.Controls.Button btnSign_up;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/CityTourApp;component/Page_signIn.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.sign_in = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("sign_in")));
            this.txtUsername = ((System.Windows.Controls.TextBox)(this.FindName("txtUsername")));
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("txtPassword")));
            this.btnSign_in = ((System.Windows.Controls.Button)(this.FindName("btnSign_in")));
            this.sign_up = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("sign_up")));
            this.txtSUusername = ((System.Windows.Controls.TextBox)(this.FindName("txtSUusername")));
            this.txtSUpassword = ((System.Windows.Controls.PasswordBox)(this.FindName("txtSUpassword")));
            this.btnSign_up = ((System.Windows.Controls.Button)(this.FindName("btnSign_up")));
        }
    }
}


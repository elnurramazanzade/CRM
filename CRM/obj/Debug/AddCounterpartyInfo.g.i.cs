﻿#pragma checksum "..\..\AddCounterpartyInfo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7F75A5533B497CCD992230DC74B4B28ED3F15D75"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CRM;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CRM {
    
    
    /// <summary>
    /// AddCounterparty
    /// </summary>
    public partial class AddCounterparty : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtCounterpartyName;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtResponsiblePerson;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbPosition;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtPhone;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtMobile;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtAddress;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxtBlcAttention;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Save;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\AddCounterpartyInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Clear;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CRM;component/addcounterpartyinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddCounterpartyInfo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TxtCounterpartyName = ((System.Windows.Controls.TextBox)(target));
            
            #line 11 "..\..\AddCounterpartyInfo.xaml"
            this.TxtCounterpartyName.GotFocus += new System.Windows.RoutedEventHandler(this.TxtCounterpartyName_GotFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TxtResponsiblePerson = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\AddCounterpartyInfo.xaml"
            this.TxtResponsiblePerson.GotFocus += new System.Windows.RoutedEventHandler(this.TxtResponsiblePerson_GotFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CmbPosition = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.TxtPhone = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\AddCounterpartyInfo.xaml"
            this.TxtPhone.GotFocus += new System.Windows.RoutedEventHandler(this.TxtPhone_GotFocus);
            
            #line default
            #line hidden
            
            #line 18 "..\..\AddCounterpartyInfo.xaml"
            this.TxtPhone.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtPhone_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TxtMobile = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\AddCounterpartyInfo.xaml"
            this.TxtMobile.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtMobile_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TxtAddress = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\AddCounterpartyInfo.xaml"
            this.TxtAddress.GotFocus += new System.Windows.RoutedEventHandler(this.TxtAddress_GotFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TxtBlcAttention = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.Save = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\AddCounterpartyInfo.xaml"
            this.Save.Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Clear = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\AddCounterpartyInfo.xaml"
            this.Clear.Click += new System.Windows.RoutedEventHandler(this.Clear_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


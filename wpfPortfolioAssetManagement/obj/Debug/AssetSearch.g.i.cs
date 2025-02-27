﻿#pragma checksum "..\..\AssetSearch.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3E2F2D94BF7FBAB942E97BCC4CF150FCFBD83EA1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using wpfPortfolioAssetManagement;


namespace wpfPortfolioAssetManagement {
    
    
    /// <summary>
    /// AssetSearch
    /// </summary>
    public partial class AssetSearch : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvAssets;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Menu_WatchlistAdd;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSearch;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbLang;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblRegion;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbRegion;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblRegion_Copy;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\AssetSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox;
        
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
            System.Uri resourceLocater = new System.Uri("/wpfPortfolioAssetManagement;component/assetsearch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AssetSearch.xaml"
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
            this.lvAssets = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.Menu_WatchlistAdd = ((System.Windows.Controls.MenuItem)(target));
            
            #line 16 "..\..\AssetSearch.xaml"
            this.Menu_WatchlistAdd.Click += new System.Windows.RoutedEventHandler(this.Menu_WatchlistAdd_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\AssetSearch.xaml"
            this.tbSearch.KeyUp += new System.Windows.Input.KeyEventHandler(this.Search_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.cmbLang = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.lblRegion = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.cmbRegion = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.lblRegion_Copy = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.comboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


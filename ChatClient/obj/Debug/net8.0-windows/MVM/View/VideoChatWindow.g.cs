﻿#pragma checksum "..\..\..\..\..\MVM\View\VideoChatWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85EF3C4D6F80B4EE4C03513B16021ED1FCABF646"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SimpleChatAppWithoutDesign.MVM.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace SimpleChatAppWithoutDesign.MVM.View {
    
    
    /// <summary>
    /// VideoChatWindow
    /// </summary>
    public partial class VideoChatWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_Send;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_Recieve;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_StartCall;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Video;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_ButtonVideo;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Audio;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_ButtonAudio;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ChatClient;component/mvm/view/videochatwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Image_Send = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.Image_Recieve = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.Button_StartCall = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
            this.Button_StartCall.Click += new System.Windows.RoutedEventHandler(this.Button_StartCall_OnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Button_Video = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
            this.Button_Video.Click += new System.Windows.RoutedEventHandler(this.Button_Video_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Image_ButtonVideo = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.Button_Audio = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\..\..\MVM\View\VideoChatWindow.xaml"
            this.Button_Audio.Click += new System.Windows.RoutedEventHandler(this.Button_Audio_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Image_ButtonAudio = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

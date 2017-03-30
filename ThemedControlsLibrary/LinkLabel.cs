using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Documents;

namespace ThemedControlsLibrary
{
    [TemplatePart(Name = "PART_InnerHyperlink", Type = typeof(Hyperlink))]
    public class LinkLabel : Label
    {
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(Uri), typeof(LinkLabel));

        [Category("Common Properties"), Bindable(true)]
        public Uri Url
        {
            get { return GetValue(UrlProperty) as Uri; }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly DependencyProperty HyperlinkStyleProperty =
            DependencyProperty.Register("HyperlinkStyle", typeof(Style),
                typeof(LinkLabel));

        public Style HyperlinkStyle
        {
            get { return GetValue(HyperlinkStyleProperty) as Style; }
            set { SetValue(HyperlinkStyleProperty, value); }
        }

        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register("HoverForeground", typeof(Brush),
                typeof(LinkLabel));

        [Category("Brushes"), Bindable(true)]
        public Brush HoverForeground
        {
            get { return GetValue(HoverForegroundProperty) as Brush; }
            set { SetValue(HoverForegroundProperty, value); }
        }

        public static readonly DependencyProperty LinkLabelBehaviorProperty =
            DependencyProperty.Register("LinkLabelBehavior",
                typeof(LinkLabelBehavior),
                typeof(LinkLabel));

        [Category("Common Properties"), Bindable(true)]
        public LinkLabelBehavior LinkLabelBehavior
        {
            get { return (LinkLabelBehavior)GetValue(LinkLabelBehaviorProperty); }
            set { SetValue(LinkLabelBehaviorProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(LinkLabel));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(LinkLabel));

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(LinkLabel));

        [Localizability(LocalizationCategory.NeverLocalize), Bindable(true), Category("Action")]
        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        [Localizability(LocalizationCategory.NeverLocalize), Bindable(true), Category("Action")]
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        [Bindable(true), Category("Action")]
        public IInputElement CommandTarget
        {
            get { return (IInputElement)this.GetValue(CommandTargetProperty); }
            set { this.SetValue(CommandTargetProperty, value); }
        }

        [Category("Behavior")]
        public static readonly RoutedEvent ClickEvent;

        [Category("Behavior")]
        public static readonly RoutedEvent RequestNavigateEvent;

        static LinkLabel()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LinkLabel),
                new FrameworkPropertyMetadata(typeof(LinkLabel)));

            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LinkLabel));
            RequestNavigateEvent = EventManager.RegisterRoutedEvent("RequestNavigate", RoutingStrategy.Bubble, typeof(RequestNavigateEventHandler), typeof(LinkLabel));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Hyperlink innerHyperlink = GetTemplateChild("PART_InnerHyperlink") as Hyperlink;
            if (innerHyperlink != null)
            {
                innerHyperlink.Click += new RoutedEventHandler(InnerHyperlink_Click);
                innerHyperlink.RequestNavigate += new RequestNavigateEventHandler(InnerHyperlink_RequestNavigate);
            }
        }

        void InnerHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            RequestNavigateEventArgs args = new RequestNavigateEventArgs(e.Uri, String.Empty);
            args.Source = this;
            args.RoutedEvent = LinkLabel.RequestNavigateEvent;
            RaiseEvent(args);
        }

        
        void InnerHyperlink_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(LinkLabel.ClickEvent, this));
        }

        public event RoutedEventHandler Click
        {
            add
            {
                base.AddHandler(ClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(ClickEvent, value);
            }
        }

        public event RequestNavigateEventHandler RequestNavigate
        {
            add
            {
                base.AddHandler(RequestNavigateEvent, value);
            }
            remove
            {
                base.RemoveHandler(RequestNavigateEvent, value);
            }
        }
    }
}

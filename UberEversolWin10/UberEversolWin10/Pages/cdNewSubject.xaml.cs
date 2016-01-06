﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UberEversol.Model;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UberEversol.Pages
{
    public enum cdResult
    {
        AddSuccess,
        AddFail,
        AddCancel
    }

    public sealed partial class cdNewSubject : ContentDialog
    {

        protected Subject newSub;
        public cdResult result { get; set; }

        public cdNewSubject()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Content Dialog Save Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            newSub = new Subject(txtFirstName.Text, txtLastName.Text);

            using (var db = new UberEversolContext())
            {
                db.Subjects.Add(newSub);
                db.SaveChanges();
            }

            result = cdResult.AddSuccess;

            FlyoutBase.SetAttachedFlyout(this, (FlyoutBase)this.Resources["notifyFlyout"]);
            FlyoutBase.ShowAttachedFlyout(this);

            FlyoutBase.GetAttachedFlyout(this).Hide();
        }

        /// <summary>
        /// Content Dialog Cancel Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            result = cdResult.AddCancel;
            this.Hide();
            //FlyoutBase.GetAttachedFlyout(this).Hide();
        }

        // When the flyout closes, hide the sign in dialog, too.
        private void Flyout_Closed(object sender, object e)
        {
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
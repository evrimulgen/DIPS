﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DIPS.UI.Pages;
using DIPS.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using DIPS.ViewModel.Unity;

namespace DIPS.UI.Unity.Implementations
{
    public class QueueDialog : IQueueDialog
    {
        public void ShowDialog( IJobTracker jobs )
        {
            QueueWindow w = new QueueWindow();
            w.DataContext = jobs;
            w.ShowInTaskbar = true;
            w.ShowDialog();
        }
    }
}

﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DIPS.Database.Objects;
using DIPS.ViewModel.Commands;
using System.Collections.Generic;
using DIPS.Processor.Client;
using System.Collections.Specialized;
using DIPS.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System.Windows.Data;
using DIPS.Util.Commanding;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep3ViewModel : BaseViewModel, IJobSource
    {
        public ObservableCollection<FileInfo> ListOfFiles
        {
            get;
            private set;
        }

        public string PipelineName
        {
            get
            {
                return _pipelineName;
            }
            set
            {
                _pipelineName = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private string _pipelineName;

        public string Identifier
        {
            get
            {
                return _identifier;
            }
            set
            {
                _identifier = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private string _identifier;

        public ObservableCollection<AlgorithmViewModel> PipelineAlgorithms
        {
            get;
            private set;
        }


        public ComboBoxItem PostProcessAction { get; set; }

        public UnityCommand ProcessFilesCommand { get; private set; }
        public ICommand GoBackCommand { get; set; }

        public LoadNewDsStep3ViewModel()
        {
            PipelineAlgorithms = new ObservableCollection<AlgorithmViewModel>();
            ListOfFiles = new ObservableCollection<FileInfo>();
            PipelineAlgorithms.CollectionChanged += _jobDetailsChanged;
            ListOfFiles.CollectionChanged += _jobDetailsChanged;
            SetupCommands();
            Handler = new SaveToDatabaseHandler();
        }

        

        private void ProcessFiles(object obj)
        {

            if (PostProcessAction.Content.ToString() == "Shut down")
            {
                MessageBoxResult result = MessageBox.Show("You have chosen to turn off the computer after processing - are you sure?", "Shut down computer after processing?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Images processed.", "Processing Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    Process.Start("shutdown", "/s /t 0");
                }
            }
            else if (PostProcessAction.Content.ToString() == "Sleep")
            {
                MessageBoxResult resultSleep = MessageBox.Show("You have chosen to put the computer into hibernate mode after processing - are you sure?", "Hibernate computer after processing?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultSleep == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Images processed.", "Processing Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Hibernate
                    Process.Start("shutdown", "/h /f");
                }

            }
            else
            {
                MessageBox.Show("Images processed", "Processing complete.", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            OverallFrame.Content = BaseViewModel._MainViewModel;
        }

        private void SetupCommands()
        {
            ProcessFilesCommand = new EnqueueJobCommand( this );
            ProcessFilesCommand.Container = GlobalContainer.Instance.Container;
            GoBackCommand = new RelayCommand(new Action<object>(_goBack));
        }

        private void _goBack(object obj)
        {
            OverallFrame.Content = _PostProcessingViewModel;
        }

        private void _jobDetailsChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            ProcessFilesCommand.ExecutableStateChanged();
        }


        IEnumerable<FileInfo> IJobSource.Files
        {
            get { return ListOfFiles; }
        }

        PipelineDefinition IJobSource.Pipeline
        {
            get
            {
                return new PipelineDefinition(
                    from process in PipelineAlgorithms select process.Definition );
            }
        }

        public IJobResultsHandler Handler
        {
            get
            {
                return _handler;
            }
            set
            {
                _handler = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IJobResultsHandler _handler;
    }
}

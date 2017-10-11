using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
namespace LearnThreadingWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker bakCodebreaker = new BackgroundWorker();
        private readonly BackgroundWorker bakCodebreaker1 = new BackgroundWorker();
        private readonly BackgroundWorker bakCodebreaker2 = new BackgroundWorker();
        private readonly BackgroundWorker bakCodebreaker3 = new BackgroundWorker();

      

        // The simulated code to be broken
        private string Code;
        // The list of Labels of the characters to be broken.
        private List<TextBlock> OutputCharLabels;
        // The list of ProgressBar controls that show the
        // progress of the character being decoded
        private List<ProgressBar> prloProgressChar;
        public MainWindow()
        {
            bakCodebreaker.DoWork += BakCodebreaker_DoWork;
            bakCodebreaker1.DoWork += BakCodebreaker1_DoWork;
            bakCodebreaker2.DoWork += BakCodebreaker2_DoWork;
            bakCodebreaker3.DoWork += BakCodebreaker3_DoWork;

            bakCodebreaker.ProgressChanged += BakCodebreaker_ProgressChanged;
            bakCodebreaker1.ProgressChanged += BakCodebreaker1_ProgressChanged;
            bakCodebreaker2.ProgressChanged += BakCodebreaker2_ProgressChanged;
            bakCodebreaker3.ProgressChanged += BakCodebreaker3_ProgressChanged;

            bakCodebreaker.WorkerReportsProgress = true;
            bakCodebreaker1.WorkerReportsProgress = true;
            bakCodebreaker2.WorkerReportsProgress = true;
            bakCodebreaker3.WorkerReportsProgress = true;

            bakCodebreaker.WorkerSupportsCancellation = true;
            bakCodebreaker1.WorkerSupportsCancellation = true;
            bakCodebreaker2.WorkerSupportsCancellation = true;
            bakCodebreaker3.WorkerSupportsCancellation = true;

            bakCodebreaker.RunWorkerCompleted += BakCodebreaker_RunWorkerCompleted;
            bakCodebreaker1.RunWorkerCompleted += BakCodebreaker1_RunWorkerCompleted;
            bakCodebreaker2.RunWorkerCompleted += BakCodebreaker2_RunWorkerCompleted;
            bakCodebreaker3.RunWorkerCompleted += BakCodebreaker3_RunWorkerCompleted;

            InitializeComponent();
            btnStop.IsEnabled = false;
            // Create a new list of ProgressBar controls that show
            // the progress of each character of the code being
            // broken
            prloProgressChar = new List<ProgressBar>(4);
            // Add the ProgressBar controls to the list
            prloProgressChar.Add(pgb1);
            prloProgressChar.Add(pgb2);
            prloProgressChar.Add(pgb3);
            prloProgressChar.Add(pgb4);
            // Generate a random code to be broken
            SimulateCodeGeneration();
            // Create a new list of Label controls that show the characters of the code being broken.
            OutputCharLabels = new List<TextBlock>(4);
            // Add the Label controls to the List
            OutputCharLabels.Add(txtoutput1);
            OutputCharLabels.Add(txtoutput2);
            OutputCharLabels.Add(txtoutput3);
            OutputCharLabels.Add(txtoutput4);
            // Hide the fishes game and show the CodeBreaker
            showCodeBreaker();
        }

        private void BakCodebreaker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedProcedure(sender, e);
        }

        private void BakCodebreaker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedProcedure(sender, e);
        }

        private void BakCodebreaker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedProcedure(sender, e);
        }

        private void BakCodebreaker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedProcedure(sender, e);
        }

        private void ProgressChangedProcedure(object sender,
ProgressChangedEventArgs e)
        {
            // This variable will hold a CodeBreakerProgress
            // instance
            CodeBreakerProgress loCodeBreakerProgress =
            (CodeBreakerProgress)e.UserState;
            // Update the corresponding ProgressBar with the          percentage received as a parameter
            prloProgressChar[loCodeBreakerProgress.CharNumber].Value =
            loCodeBreakerProgress.PercentageCompleted;
            // Update the corresponding Label with the character being processed
            OutputCharLabels[loCodeBreakerProgress.CharNumber].Text =
            ((char)loCodeBreakerProgress.CharCode).ToString();
        }

        private void BakCodebreaker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChangedProcedure(sender, e);
        }

        private void BakCodebreaker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChangedProcedure(sender, e);
        }

        private void BakCodebreaker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChangedProcedure(sender, e);
        }

        private void BakCodebreaker3_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWorkProcedure(sender, e);
        }

        private void BakCodebreaker2_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWorkProcedure(sender, e);
        }

        private void BakCodebreaker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWorkProcedure(sender, e);
        }

        private void BakCodebreaker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChangedProcedure(sender, e);
        }

        private void DoWorkProcedure(object sender, DoWorkEventArgs e)
        {
            // This variable will hold the broken code
            string lsBrokenCode = "";
            CodeBreakerParameters loCodeBreakerParameters = (CodeBreakerParameters)e.Argument;
            int liTotal = loCodeBreakerParameters.MaxUnicodeCharCode;
            Thread.Sleep(10000);
            // This code will break the simulated code.
            // This variable will hold a number to iterate from 1 to 65,535 - Unicode character set.
            int i;
            // This variable will hold a number to iterate from 0 to 3(the characters positions in the code to be broken).
            int liCharNumber;
            // This variable will hold a char generated from the number in i
            char lcChar;
            // This variable will hold the current Label control that shows the char position being decoded.
            TextBlock loOutputCharCurrentLabel;
            // This variable will hold a CodeBreakerProgress
            // instance
            CodeBreakerProgress loCodeBreakerProgress = new
            CodeBreakerProgress();
            // This variable will hold the last percentage of the iteration completed
            int liOldPercentageCompleted;
            liOldPercentageCompleted = 0;
            //for (liCharNumber = 0; liCharNumber < 4;
            //liCharNumber++)
            for (liCharNumber = loCodeBreakerParameters.FirstCharNumber;
liCharNumber <= loCodeBreakerParameters.LastCharNumber;
liCharNumber++)
            {
                loOutputCharCurrentLabel =
                OutputCharLabels[liCharNumber];
                // This loop will run 65,536 times
                for (i = 0; i <= 65535; i++)
                {
                    //   if (bakCodebreaker.CancellationPending)
                    if (((BackgroundWorker)sender).CancellationPending)
                    {
                        // The user requested to cancel the process
                        e.Cancel = true;
                        return;
                    }
                    // myChar holds a Unicode char
                    lcChar = (char)(i);
                    //  loOutputCharCurrentLabel.Text = lcChar.ToString();
                    //Application.DoEvents();
                    // The percentage completed is calculated and stored in
                    // the PercentageCompleted property
                    loCodeBreakerProgress.PercentageCompleted = (int)((i * 100) /
                    65535);
                    loCodeBreakerProgress.CharNumber = liCharNumber;
                    loCodeBreakerProgress.CharCode = i;
                    if (loCodeBreakerProgress.PercentageCompleted >
                    liOldPercentageCompleted)
                    {
                        // The progress is reported only when it changes with regard to the last one(liOldPercentageCompleted)
                        // bakCodebreaker.ReportProgress(loCodeBreakerProgress.PercentageCompleted, loCodeBreakerProgress);
                        ((BackgroundWorker)sender).ReportProgress(loCodeBreakerProgress.
 PercentageCompleted, loCodeBreakerProgress);
                        // The old percentage completed is now the
                        // percentage reported
                        liOldPercentageCompleted = loCodeBreakerProgress.
                        PercentageCompleted;
                    }
                    if (checkCodeChar(lcChar, liCharNumber))
                    {
                        // The code position was found
                        loCodeBreakerProgress.PercentageCompleted = 100;
                        //    bakCodebreaker.ReportProgress(loCodeBreakerProgress.PercentageCompleted, loCodeBreakerProgress);
                        ((BackgroundWorker)sender).ReportProgress(loCodeBreakerProgress.PercentageCompleted, loCodeBreakerProgress);
                        // The code position was found
                        break;
                    }
                    // Create a new instance of the CodeBreakerResult class
                    // and set its properties' values
                    CodeBreakerResult loResult = new CodeBreakerResult();
                    loResult.FirstCharNumber = loCodeBreakerParameters.
                    FirstCharNumber;
                    loResult.LastCharNumber = loCodeBreakerParameters.
                    LastCharNumber;
                    loResult.BrokenCode = lsBrokenCode;
                    // Return a CodeBreakerResult instance in the Result
                    // property
                    e.Result = loResult;
                }
            }
            //  MessageBox.Show("The code has been decoded successfully.", this.Title);
        }

        private void RunWorkerCompletedProcedure(object sender,
RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                // Obtain the CodeBreakerResult instance
                // contained in the Result property of e
                // parameter
                CodeBreakerResult loResult = (CodeBreakerResult)
                e.Result;
                int i;
                // Iterate through the parts of the result// resolved by this BackgroundWorker
                for (i = loResult.FirstCharNumber; i <= loResult.
                LastCharNumber; i++)
                {
                    // The process has finishes, therefore the
                    // ProgressBar control must show a 100%
                    prloProgressChar[i].Value = 100;
                    // Show the part of the broken code in the
                    // label
                   // OutputCharLabels[i].Text = loResult.BrokenCode[i - loResult.FirstCharNumber].ToString();
                }
            }
        }
        private void BakCodebreaker_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWorkProcedure(sender, e);

        }

        private void SimulateCodeGeneration()
        {
            // A Random number generator.
            Random loRandom = new Random();
            // The char position being generated
            int i;
            Code = "";
            for (i = 0; i <= 4; i++)
            {
                // Generate a Random Unicode char for each of
                //the 4 positions
                Code += (char)(loRandom.Next(65535));
            }
        }

        private void setFishesVisibility(System.Windows.Visibility pbValue)
        {
            // Change the visibility of the controls
            //related to the fishes game.
            imgFish1.Visibility = pbValue;
            imgFish2.Visibility = pbValue;
            imgFish3.Visibility = pbValue;
            txtgame.Visibility = pbValue;
            btnGameover.Visibility = pbValue;
        }

        private void setCodeBreakerVisibility(System.Windows.Visibility pbValue)
        {
            // Change the visibility of the controls related to the CodeBreaking procedure.
            imgSkull.Visibility = pbValue;
            imgAgent.Visibility = pbValue;
            txtcodebreaker.Visibility = pbValue;
            txtnumber1.Visibility = pbValue;
            txtnumber2.Visibility = pbValue;
            txtnumber3.Visibility = pbValue;
            txtnumber4.Visibility = pbValue;
            txtoutput1.Visibility = pbValue;
            txtoutput2.Visibility = pbValue;
            txtoutput3.Visibility = pbValue;
            txtoutput4.Visibility = pbValue;
            btnStart.Visibility = pbValue;
            btnHide.Visibility = pbValue;
            // Change the visibility of the controls related to the
            // progress of the CodeBreaking procedure
            pgb1.Visibility = pbValue;
            pgb2.Visibility = pbValue;
            pgb3.Visibility = pbValue;
            pgb4.Visibility = pbValue;
            // Change the visibility of the new stop button
            btnStop.Visibility = pbValue;
        }

        private void showFishes()
        {
            // Hide all the controls related to the code
            // breaking procedure.
            setCodeBreakerVisibility(System.Windows.Visibility.Hidden);
            // Change the window title
            this.Title = "Fishing game for Windows 1.0";
            // Make the fishes visible
            setFishesVisibility(System.Windows.Visibility.Visible);
        }

        private void showCodeBreaker()
        {
            // Hide all the controls related to the fishes
            // game
            setFishesVisibility(System.Windows.Visibility.Hidden);
            // Change the window title
            this.Title = "CodeBreaker Application";
            // Make the code breaker controls visible
            setCodeBreakerVisibility(System.Windows.Visibility.
            Visible);
        }

        private bool checkCodeChar(char pcChar, int piCharNumber)
        {
            // Returns a bool value indicating whether the piCharNumber position of the code is the pcChar received.
            return (Code[piCharNumber] == pcChar);
        }

        private void btnGameover_Click(object sender, RoutedEventArgs e)
        {
            // Hide the fishes game and show the CodeBreaker
            showCodeBreaker();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            // Hide the CodeBreaker and show the fishes game
            showFishes();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Disable the Start button
            btnStart.IsEnabled = false;
            // Enable the Stop button
            btnStop.IsEnabled = true;
            // Start running the code programmed in
            // BackgroundWorker DoWork event handler
            // in a new independent thread and return control to
            // the application's main thread
            CodeBreakerParameters loParameters1 = new
CodeBreakerParameters();
            CodeBreakerParameters loParameters2 = new
            CodeBreakerParameters();
            CodeBreakerParameters loParameters3 = new
            CodeBreakerParameters();
            CodeBreakerParameters loParameters4 = new
            CodeBreakerParameters();
            loParameters1.MaxUnicodeCharCode = 32000;
            loParameters1.FirstCharNumber = 0;
            loParameters1.LastCharNumber = 0;
            loParameters2.MaxUnicodeCharCode = 32000;
            loParameters2.FirstCharNumber = 1;
            loParameters2.LastCharNumber = 1;
            loParameters3.MaxUnicodeCharCode = 32000;
            loParameters3.FirstCharNumber = 2;
            loParameters3.LastCharNumber = 2;
            loParameters4.MaxUnicodeCharCode = 32000;
            loParameters4.FirstCharNumber = 3;
            loParameters4.LastCharNumber = 3;
            bakCodebreaker.RunWorkerAsync(loParameters1);
            bakCodebreaker1.RunWorkerAsync(loParameters2);
            bakCodebreaker2.RunWorkerAsync(loParameters3);
            bakCodebreaker3.RunWorkerAsync(loParameters4);
            // bakCodebreaker.RunWorkerAsync();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            // Disable the Stop button
            btnStop.IsEnabled = false;
            // Enable the Start button
            btnStart.IsEnabled = true;
            //Call the CancelAsync method to cancel the
            // process.
            bakCodebreaker.CancelAsync();
            bakCodebreaker1.CancelAsync();
            bakCodebreaker2.CancelAsync();
            bakCodebreaker3.CancelAsync();
        }
    }
}

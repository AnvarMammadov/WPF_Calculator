using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace WPF_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double firstValue, secondValue;
        string operationName, instance = "";
        public MainWindow()
        {
            InitializeComponent();
            OtherHelperFunctions.CultureDot();
            txtResult.Text = "0";

        }
        
        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = "0";
                string first, second;
                first = Convert.ToString(firstValue);
                second = Convert.ToString(secondValue);
                instance = "";
                txtBlockInstance.Text = "";
                first = ""; second = "";
                EnableOperationPoint();
            }
            catch (Exception ex)
            {
               notifier.ShowError(ex.Message);
                ClearOrDefault();
                SystemSounds.Asterisk.Play();
                ErrorLog.LogError(ex);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = "0";
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
                ClearOrDefault();
                SystemSounds.Asterisk.Play();
                ErrorLog.LogError(ex);
            }
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtResult.Text.Length > 0)
                {
                    txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1, 1);
                }
                if (txtResult.Text.Length == 0) txtResult.Text = "0";
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
                ClearOrDefault();
                SystemSounds.Asterisk.Play();
                ErrorLog.LogError(ex);
            }
        }

        bool isSubt = false;
        private void btnOperator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btnOp = (Button)sender;
                firstValue = Convert.ToDouble(txtResult.Text);
                operationName = btnOp.Name;
                txtResult.Text = "0";
                instance += OtherHelperFunctions.FirstOperationInstanceText(firstValue, btnOp.Name);
                txtBlockInstance.Text = instance;

                if(btnOp.Name=="btnSubt") isSubt=true;
                else isSubt=false;  

                DisableOperationPoint();
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
                ClearOrDefault();
                SystemSounds.Asterisk.Play();
                ErrorLog.LogError(ex);
            }

        }

        private void EnableOperationPoint()
        {
            btnSum.IsEnabled = true;
            btnSubt.IsEnabled = true;
            btnMult.IsEnabled = true;
            btnDevide.IsEnabled = true;

        }
        private void DisableOperationPoint()
        {
            btnSum.IsEnabled = false;
            btnSubt.IsEnabled = false;
            btnMult.IsEnabled = false;
            btnDevide.IsEnabled = false;

        }

        private void btnNumber_Click(object sender, RoutedEventArgs e) {

            try
            {
                var btnNum = (Button)sender;
                if (txtResult.Text == "0") txtResult.Text = "";
                {

                    if (btnNum.Content.ToString() == ".")
                    {
                        if (!txtResult.Text.Contains(".")) txtResult.Text += btnNum.Content.ToString();
                    }
                    else txtResult.Text += btnNum.Content.ToString();
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
                ClearOrDefault();
                SystemSounds.Asterisk.Play();
                ErrorLog.LogError(ex);
            }
            
        }

        private void btnPosOrNeg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double numNeg = Convert.ToDouble(txtResult.Text);
                if(txtResult.Text != "0") { txtResult.Text = Convert.ToString(-1 * numNeg); }
                else txtResult.Text = "0";
               
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
                ClearOrDefault();
                SystemSounds.Asterisk.Play();
                ErrorLog.LogError(ex);
            }
        }

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                secondValue = Convert.ToDouble(txtResult.Text);
                switch (operationName)
                {
                    case "btnSum":
                        txtResult.Text = MathOperations.Sum(firstValue, secondValue).ToString();
                        break;
                    case "btnSubt":
                        txtResult.Text = MathOperations.Subt(firstValue, secondValue).ToString();
                        break;
                    case "btnMult":
                        txtResult.Text = MathOperations.Mult(firstValue, secondValue).ToString();
                        break;
                    case "btnDevide":
                        txtResult.Text = MathOperations.Devide(firstValue, secondValue).ToString(CultureInfo.InvariantCulture);
                        break;
                    default:
                        break;
                }
                if (isSubt && secondValue < 0) instance = OtherHelperFunctions.FirstOperationInstanceText(firstValue, btnSum.Name) +
                        OtherHelperFunctions.EqualOperationInstanceText(-1 * secondValue);
                else instance += OtherHelperFunctions.EqualOperationInstanceText(secondValue);
                txtBlockInstance.Text = instance;
                instance = "";
                EnableOperationPoint();
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
                ClearOrDefault();
                SystemSounds.Asterisk.Play();
                ErrorLog.LogError(ex);
            }
        }

        private void ClearOrDefault()
        {
            instance = "";
            txtBlockInstance.Text = "";
            firstValue = 0; secondValue = 0;
            txtResult.Text = "0";

        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }
}

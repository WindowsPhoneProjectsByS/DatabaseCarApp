using BazaDanych2.Common;
using BazaDanych2.Models;
using BazaDanych2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BazaDanych2.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddEditPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private int carId;
        private DatabaseService dbServiece;

        public AddEditPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            dbServiece = new DatabaseService();

            PrepareLayout();
            GetDataFromPrevious(e);
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public void PrepareLayout()
        {
            switch (Settings.operation)
            {
                case Settings.Operation.Add:
                    PrepareViewForAdd();
                    break;
                case Settings.Operation.Edit:
                    PrepareViewForEdit();
                    break;
                default:
                    ShowProperMessage();
                    break;
            }
        }


        private void GetDataFromPrevious(NavigationEventArgs e)
        {
            if (Settings.operation == Settings.Operation.Edit)
            {
                carId = (int)e.Parameter;
            }
        }


        public void PrepareViewForAdd()
        {
            TitleOperation.Text = "Dodaj";
            OperationButton.Content = "Dodaj";
        }

        public void PrepareViewForEdit()
        {
            TitleOperation.Text = "Edytuj";
            OperationButton.Content = "Edytuj";

            PrepareControls();


        }

        private void PrepareControls()
        {

        }

        public async void ShowProperMessage()
        {
            MessageDialog msg = new MessageDialog("Wystąpił nie znany bład.");
            await msg.ShowAsync();
            Frame.Navigate(typeof(MainPage));
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Settings.operation)
            {
                case Settings.Operation.Add:
                    AddCarToDB();
                    break;
                case Settings.Operation.Edit:
                    UpdateCar();
                    break;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void AddCarToDB()
        {
            Car car = new Car();
            car.Producer = Producer.Text;
            car.Model = Model.Text;
            car.ProductionYear = Int32.Parse(ProductionYear.Text);
            car.Capacity = Capacity.Text;
            car.FuelType = FuelType.Text;
            car.Power = Int32.Parse(Power.Text);

            dbServiece.Insert(car);

            Frame.Navigate(typeof(MainPage));
        }

        private void UpdateCar()
        {

        }
    }
}

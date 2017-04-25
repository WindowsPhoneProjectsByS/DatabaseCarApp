using BazaDanych2.Models;
using BazaDanych2.Pages;
using BazaDanych2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace BazaDanych2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DatabaseService dbService;
        private int actualChoosenId = -1;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            dbService = new DatabaseService();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            actualChoosenId = -1;
            PrepareCarList();
        }

        private void PrepareCarList()
        {
            Debug.WriteLine("MainPage: OnNavigateTo");
            ObservableCollection<Car> carList = dbService.ReadCars();
            Debug.WriteLine("List samochodów: " + carList.Count);
            CarListBox.ItemsSource = carList.OrderByDescending(i => i.Id).ToList();
        }

        private void CarListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarListBox.SelectedIndex != -1)
            {
                Car car = CarListBox.SelectedItem as Car;
                actualChoosenId = car.Id;
                Debug.WriteLine("Wybrane Id: " + actualChoosenId);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.operation = Settings.Operation.Add;
            Frame.Navigate(typeof(AddEditPage));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (actualChoosenId != -1)
            {
                Settings.operation = Settings.Operation.Edit;
                Frame.Navigate(typeof(AddEditPage), actualChoosenId);
            }
            
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (actualChoosenId != -1)
            {
                Frame.Navigate(typeof(DisplayInfoPage), actualChoosenId);
            }
        }
    }
}

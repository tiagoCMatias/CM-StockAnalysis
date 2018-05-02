using OxyPlot.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;

namespace Stock
{
	public partial class MainPage : ContentPage
	{

	    public PlotModel AreaModel { get; set; }

        public MainPage()
		{

			InitializeComponent();
		    createPicker();

		}

	    public PlotModel CreateAreaChart()
	    {
	        var plotModel1 = new PlotModel { Title = "AreaSeries with crossing lines" };
	        var areaSeries1 = new AreaSeries();
	        areaSeries1.Points.Add(new DataPoint(0, 50));
	        areaSeries1.Points.Add(new DataPoint(10, 140));
	        areaSeries1.Points.Add(new DataPoint(20, 60));
	        areaSeries1.Points2.Add(new DataPoint(0, 60));
	        areaSeries1.Points2.Add(new DataPoint(5, 80));
	        areaSeries1.Points2.Add(new DataPoint(20, 70));
	        plotModel1.Series.Add(areaSeries1);
	        return plotModel1;
	    }

        public void createPicker()
	    {
            // Dictionary to get Color from color name.
            Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
            {
                { "Aqua", Color.Aqua }, { "Black", Color.Black },
                { "Blue", Color.Blue }, { "Fucshia", Color.Fuchsia },
                { "Gray", Color.Gray }, { "Green", Color.Green },
                { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
                { "Navy", Color.Navy }, { "Olive", Color.Olive },
                { "Purple", Color.Purple }, { "Red", Color.Red },
                { "Silver", Color.Silver }, { "Teal", Color.Teal },
                { "White", Color.White }, { "Yellow", Color.Yellow }
            };

            Label header = new Label
            {
                Text = "Picker",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Picker picker1 = new Picker
            {
                Title = "Color",
                WidthRequest = 60
            };

	        Picker picker2 = new Picker
	        {
	            Title = "Color",
	            VerticalOptions = LayoutOptions.CenterAndExpand,
	            
            };

	        

            foreach (string colorName in nameToColor.Keys)
            {
                picker1.Items.Add(colorName);
                picker2.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            BoxView boxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker1.SelectedIndexChanged += (sender, args) =>
            {
                if (picker1.SelectedIndex == -1)
                {
                    boxView.Color = Color.Default;
                }
                else
                {
                    string colorName = picker1.Items[picker1.SelectedIndex];
                    boxView.Color = nameToColor[colorName]; ;
                }
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

	        AreaModel = CreateAreaChart();

            var pltview = new PlotView()
            {
                WidthRequest = 300,
                HeightRequest = 300,
            };

            pltview.Model = AreaModel;

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    picker1,
                    picker2,
                    //boxView,
                    pltview
                }
            };

        }
        
	}
}

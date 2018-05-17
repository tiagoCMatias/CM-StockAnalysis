using OxyPlot.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;
using Xamarin.Forms;
using Stock.ViewModel;
using OxyPlot.Axes;
using Stock.Services.Api;

namespace Stock
{
	public partial class MainPage : ContentPage
	{
	    public ApiRequest MyRequest = new ApiRequest();

        public GraphViewModel myViewModel;

	    public int Dias = -7;

        private LineSeries LineSeriesOne = new LineSeries
        {
            StrokeThickness = 2.0,
            Color = OxyColors.Green
            
        };

	    private LineSeries LineSeriesTwo = new LineSeries
	    {
	        StrokeThickness = 2.0,
	        Color = OxyColors.Red
	    };

        public MainPage()
		{
			InitializeComponent();

            BindingContext = App.Locator.GraphViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            myViewModel = BindingContext as GraphViewModel;
            if (myViewModel != null)
            {
                var startDate = DateTime.Now.AddDays(-10);
                var endDate = DateTime.Now;

                var minValue = DateTimeAxis.ToDouble(startDate);
                var maxValue = DateTimeAxis.ToDouble(endDate);

                myViewModel.PlotModel = new PlotModel
                {
                    Title = "Line"
                };
                CreatePlot();
                myViewModel.PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, StringFormat = "M/d" });
                
                //myViewModel.OnAppearing();
            }

 
        }

        public void CreatePlot()
	    {
	        //PlotModel = new PlotModel { Title = "AreaSeries with crossing lines" };
	        LineSeriesOne.Points.Clear();
            LineSeriesOne.Points.Add(new DataPoint(0, 0));
            LineSeriesOne.Points.Add(new DataPoint(10, 20));
            LineSeriesOne.Points.Add(new DataPoint(30, 1));
            LineSeriesOne.Points.Add(new DataPoint(40, 12));

            myViewModel.AddToPlot(LineSeriesOne);
	    }

	    private void LineOnePicker_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        UpdatePlot(1);
        }

	    private void LineTwoPicker_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        UpdatePlot(2);
	    }

	    private async void UpdatePlot(int plotLine)
	    {
	        string date = DateTime.Today.AddDays(Dias).ToString("yyyyMMdd");

            if (plotLine == 1)
	        {
	            var symbol = myViewModel.GetLineOnePickerValue();

	            var requestResponse = await MyRequest.SendRequest(date, symbol);

	            LineSeriesOne.Points.Clear();
	            myViewModel.EmptyPlot(LineSeriesOne);

	            AddSeries(LineSeriesOne, requestResponse.ToString());
            }

	        if (plotLine == 2)
	        {
	            var symbol = myViewModel.GetLineTwoPickerValue();

	            var requestResponse = await MyRequest.SendRequest(date, symbol);

	            LineSeriesTwo.Points.Clear();
	            myViewModel.EmptyPlot(LineSeriesTwo);

	            AddSeries(LineSeriesTwo, requestResponse.ToString());
	        }
	        else return;

	        
        }

	    private void AddSeries(LineSeries newLine, string request)
	    {
	        dynamic magic = JsonConvert.DeserializeObject(request);

	        if (magic.results == null) return;

	        var teste = magic.results;

	        foreach (var item in teste)
	        {
	            foreach (var field in item)
	            {
	                Debug.WriteLine((string)field);
	            }

	            var newDate = DateTime.ParseExact((string)item.tradingDay, "yyyy-MM-dd", CultureInfo.InvariantCulture);
	            var open = item.close;
	            Debug.WriteLine("Close: " + (string)open);
	            newLine.Points.Add(new DataPoint(DateTimeAxis.ToDouble(newDate), (double)open));
	        }
	        Debug.WriteLine("Go");
	        myViewModel.AddToPlot(newLine);
	        myViewModel.PlotModel.InvalidatePlot(true);

        }


	    private void Btn7Dias_OnClicked(object sender, EventArgs e)
	    {
	        Dias = -7;
            UpdatePlot(1);
	        UpdatePlot(2);
        }

	    private void Btn30Dias_OnClicked(object sender, EventArgs e)
	    {
	        Dias = -30;
	        UpdatePlot(1);
	        UpdatePlot(2);
        }
	}

}

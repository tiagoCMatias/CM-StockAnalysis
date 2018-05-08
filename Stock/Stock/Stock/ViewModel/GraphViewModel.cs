using System;
using System.Collections.Generic;
using System.Diagnostics;
using OxyPlot;
using OxyPlot.Series;
using System.Net.Http;
using Stock.ViewModel.Base;

namespace Stock.ViewModel
{

    public class GraphViewModel : ViewModelBase
    {
        private PlotModel _plotModel;

        public class CompanyNames
        {
            public string Name { get; set; }
            public string Symbol { get; set; }
        }

        public List<CompanyNames> Companies { get; set; } = new List<CompanyNames>()
        {
            new CompanyNames(){Name = "Apple",Symbol = "AAPL"},
            new CompanyNames(){Name = "IBM",Symbol = "IBM"},
            new CompanyNames(){Name = "Hewlett",Symbol = "HPE"},
            new CompanyNames(){Name = "Microsoft",Symbol = "MSFT"},
            new CompanyNames(){Name = "Oracle",Symbol = "ORCL"},
            new CompanyNames(){Name = "Google",Symbol = "GOOG"},
            new CompanyNames(){Name = "Facebook",Symbol = "FB"},
            new CompanyNames(){Name = "Netflix",Symbol = "NFLX"},
            new CompanyNames(){Name = "Intel",Symbol = "INTC"},
            new CompanyNames(){Name = "Google",Symbol = "GOOG"}
        };

        private CompanyNames _lineOne;

        public CompanyNames LineOne
        {
            get
            {
                return _lineOne;
            }
            set
            {
                _lineOne = value;
                RaisePropertyChanged();
            }
        }

        private CompanyNames _lineTwo;

        public CompanyNames LineTwo
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                _lineTwo = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _dateTime;

        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set
            {
                _plotModel = value;
                RaisePropertyChanged();
            }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        public string GetLineOnePickerValue()
        {
            return LineOne.Symbol;
        }

        public string GetLineTwoPickerValue()
        {
            return LineTwo.Symbol;
        }

        public void EmptyPlot(Series lineSeries)
        {
            PlotModel.Series.Remove(lineSeries);
        }

        public void AddToPlot(Series lineSeries)
        {
            PlotModel.Series.Add(lineSeries);
        }

    }
}
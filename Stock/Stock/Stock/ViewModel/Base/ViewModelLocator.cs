using Stock.Services.Navigation;
using Unity;

namespace Stock.ViewModel.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // ViewModels
            _container.RegisterType<GraphViewModel>();

            // Services     
            _container.RegisterType<INavigationService, NavigationService>();
        }
        public GraphViewModel GraphViewModel
        {
            get { return _container.Resolve<GraphViewModel>(); }
        }

        
    }
}

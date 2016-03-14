using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiciMAD_Map.ViewModels
{
    class VMLocator
    {
        private Lazy<MainMapViewModel> mainMapViewModel;

        public VMLocator()
        {
            mainMapViewModel = new Lazy<MainMapViewModel>(() => new MainMapViewModel());
        }

        public MainMapViewModel MainMapViewModel
        {
            get
            {
                return mainMapViewModel.Value;
            }
        }
    }
}

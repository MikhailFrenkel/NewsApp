using MvvmCross.ViewModels;
using NewsAppWithMvvmCross.ViewModels;

namespace NewsAppWithMvvmCross
{
    public class Setup : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<FirstViewModel>();
        }
    }
}
